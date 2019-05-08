using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using TemplatorEngine.Core.Abstract;
using TemplatorEngine.Core.Model;

namespace TemplatorEngine.Core.Element
{
    public class Value : TemplateElementBase
    {
        [XmlAttribute] public string DataField { get; set; }

        [XmlAttribute] public int Lines { get; set; }

        [XmlIgnore] public double? FontSize { get; set; }

        public string FontSizeStr
        {
            get => (this.FontSize.HasValue) ? this.FontSize.ToString() : null;
            set => this.FontSize = !string.IsNullOrEmpty(value) ? double.Parse(value) : default(double?);
        }
        
        [XmlAttribute] public string Align { get; set; }
        
        [XmlAttribute]
        public int Precision { get; set; }

        public override void Initialize(double? maxWidth, double? maxHeight, RenderContext context,
            IList<PropertyData> data)
        {
            var dataItem = data.SingleOrDefault(x => x.Name == this.DataField);

            if (dataItem == null)
            {
                throw new Exception($"Cannot find data for property: {this.DataField}");
            }
            
            var valueToDisplay = dataItem.Value;

            var pe = new PrintableElement
            {
                ElementType = ElementType.Text,
                X = context.CurrentX,
                Y = context.CurrentY,
                Value = valueToDisplay,
            };

            if (this.FontSize == null)
            {
                this.FontSize = context.PageSettings.FontSize;
            }
            else
            {
                pe.AddProperty(PrintableElementProperty.FontSize,this.FontSize);
            }

            if (this.Width == null)
            {
                this.Width = maxWidth ?? context.MaxPageWidth;
            }

            if (this.Height == null)
            {
                this.Height = this.FontSize * context.FontSizeHeightRatio;
            }

            if (this.Align == null)
            {
                this.Align = "Left";
            }
            else
            {
                pe.AddProperty(PrintableElementProperty.Align, this.Align);
            }

            if (this.Precision > 0)
            {
                pe.AddProperty(PrintableElementProperty.Precision, this.Precision);
            }

            pe.Height = this.Height.Value;
            pe.Width = this.Width.Value;

            if (valueToDisplay != null)
            {
                context.AddElement(pe);
            }
        }
    }
}