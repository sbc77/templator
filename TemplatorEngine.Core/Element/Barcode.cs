using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using BarcodeCore;
using TemplatorEngine.Core.Abstract;
using TemplatorEngine.Core.Model;

namespace TemplatorEngine.Core.Element
{
    public class Barcode : TemplateElementBase
    {
        private const int BarcodeStringFontSize = 14;
        
        [XmlAttribute]
        public string DataField { get; set; }

        [XmlAttribute]
        public string Type { get; set; }
        
        [XmlAttribute]
        public string Label { get; set; }
        
        [XmlAttribute]
        public double LabelFontSize { get; set; }
        
        [XmlAttribute]
        public double Scale { get; set; }
        
        [XmlAttribute]
        public double Thickness { get; set; }

        private double BarcodeStringHeight => BarcodeStringFontSize / 3.0;

        public override void Initialize(double? maxWidth, double? maxHeight, RenderContext context,
            IList<PropertyData> data)
        {
            var barcodeStr = data.SingleOrDefault(x => x.Name == this.DataField)?.Value?.ToString();

            if (this.Height == null)
            {
                this.Height = 20; 
            }
            else if (this.Height < this.BarcodeStringHeight )
            {
                throw new Exception($"Height of barcode cannot be smaller than {this.BarcodeStringHeight} mm");
            }

            if (this.Width != null)
            {
                throw new Exception("Width of barcode cannot be set - is calculated.");
            }

            if (string.IsNullOrWhiteSpace(barcodeStr))
            {
                return;
            }

            if (this.LabelFontSize <= 0)
            {
                this.LabelFontSize = 16;
            }

            if (this.Thickness <= 0)
            {
                this.Thickness = 1;
            }

            var barcode = this.GetBarcode();
            
            this.Width = 0;
            var posX = 0.0;

            if (this.Label != null)
            {
                posX = this.LabelFontSize;
            }
            
            var scale = this.Scale > 0 ? this.Scale : 1;
            var barLength = this.Height.Value - this.BarcodeStringHeight;

            if (this.Label != null)
            {
                this.PrintBarcodeLabel(context, barLength);
            }
            
            barcode.OnRenderBar = bar =>
            {
                var barWidth = bar.Width * this.Thickness * scale;
                
                var curX = context.CurrentX + posX + bar.X * scale;

                this.Width = curX > this.Width ? curX : this.Width;
                
                context.AddElement(new PrintableElement
                {
                    ElementType = ElementType.Rectangle,
                    Width = barWidth,
                    Height = barLength,
                    X =  curX,
                    Y = context.CurrentY
                });
            };

            barcode.Render(barcodeStr);

            PrintBarcodeText(context, barcodeStr,this.BarcodeStringHeight, this.Width.Value, posX, barLength);
        }

        private void PrintBarcodeLabel(RenderContext context, double barcodeHeight)
        {
            var label = new PrintableElement
            {
                ElementType = ElementType.Text,
                Height = this.LabelFontSize,
                Width = barcodeHeight,
                X = context.CurrentX,
                Y = context.CurrentY + barcodeHeight,
                Value = this.Label
            };

            label.AddProperty(PrintableElementProperty.Rotate, -90);
            label.AddProperty(PrintableElementProperty.Align, "Center");
            label.AddProperty(PrintableElementProperty.FontSize, this.LabelFontSize);

            context.AddElement(label);
        }

        private static void PrintBarcodeText(RenderContext context, string barcodeStr,double barcodeStringHeight, double barcodeWidth, double offsetLeft, double barHeight)
        {
            var barStr = new PrintableElement
            {
                ElementType = ElementType.Text,
                Height = barcodeStringHeight,
                Width = barcodeWidth -offsetLeft,
                X = context.CurrentX + offsetLeft,
                Y = context.CurrentY + barHeight,
                Value = barcodeStr
            };

            barStr.AddProperty(PrintableElementProperty.FontFamily, "Courier New");
            barStr.AddProperty(PrintableElementProperty.FontSize, BarcodeStringFontSize);
            barStr.AddProperty(PrintableElementProperty.Align, "Center");

            context.AddElement(barStr);
        }
        
        private IBarcode GetBarcode()
        {
            BarcodeTypes bt;

            switch (this.Type.ToUpper())
            {
                case "EAN128":
                    bt = BarcodeTypes.Ean_128;
                    break;
                case "GS1-128":
                    bt = BarcodeTypes.Gs1_128;
                    break;
                default:
                    throw new NotSupportedException($"Barcode type [{this.Type}] - not supported");
            }

            return BarcodeFactory.Create(bt);
            
        }
    }
}
