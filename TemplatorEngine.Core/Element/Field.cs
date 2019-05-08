using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using TemplatorEngine.Core.Abstract;
using TemplatorEngine.Core.Model;

namespace TemplatorEngine.Core.Element
{

    public class Field : TemplateElementBase
    {
        [XmlAttribute] public string DataField { get; set; }

        [XmlAttribute] public string Label { get; set; }

        [XmlAttribute] public int Lines { get; set; }

        [XmlIgnore] public double? FontSize { get; set; }

        public string FontSizeStr
        {
            get => (this.FontSize.HasValue) ? this.FontSize.ToString() : null;
            set => this.FontSize = !string.IsNullOrEmpty(value) ? double.Parse(value) : default(double?);
        }

        [XmlIgnore] public double? LabelWidth { get; set; }

        [XmlAttribute(AttributeName = "LabelWidth")]
        public string LabelWidthStr
        {
            get => (this.LabelWidth.HasValue) ? this.LabelWidth.ToString() : null;
            set => this.LabelWidth = !string.IsNullOrEmpty(value) ? double.Parse(value) : default(double?);
        }

        [XmlAttribute] public string LabelAlign { get; set; }
        
        [XmlAttribute]
        public int Precision { get; set; }

        [XmlAttribute] public string ValueAlign { get; set; }
        

        public override void Initialize(double? maxWidth, double? maxHeight, RenderContext context,
            IList<PropertyData> data)
        {
            var dataItem = data.SingleOrDefault(x => x.Name == this.DataField);

            if (dataItem == null)
            {
                throw new Exception($"Cannot find data for property: {this.DataField}");
            }

            var labelToDisplay = this.Label ?? dataItem.Label;
            var valueToDisplay = dataItem.Value;

            if (this.FontSize == null)
            {
                this.FontSize = context.PageSettings.FontSize;
            }

            if (this.Width == null)
            {
                this.Width = maxWidth ?? context.MaxPageWidth;
            }

            if (this.Height == null)
            {
                this.Height = this.FontSize * context.FontSizeHeightRatio;
            }

            if (this.ValueAlign == null)
            {
                this.ValueAlign = "Left";
            }

            if (this.LabelAlign == null)
            {
                this.LabelAlign = "Left";
            }

            if (this.LabelWidth == null)
            {
                this.LabelWidth = this.Width>60 ? 30 : this.Width/2;
            }

            if (!string.IsNullOrEmpty(labelToDisplay))
            {
                context.AddElement(new PrintableElement
                {
                    ElementType = ElementType.Text,
                    Height = this.Height.Value,
                    Width = this.LabelWidth.Value,
                    X = context.CurrentX,
                    Y = context.CurrentY,
                    Value = labelToDisplay
                });
            }

            if (valueToDisplay != null)
            {
                var v = new PrintableElement
                {
                    ElementType = ElementType.Text,
                    Height = this.Height.Value,
                    Width = this.Width.Value - LabelWidth.Value,
                    X = context.CurrentX + this.LabelWidth.Value,
                    Y = context.CurrentY,
                    Value = valueToDisplay
                };
                
                v.AddProperty(PrintableElementProperty.FontStyle,"Bold");
                
                if (this.Precision > 0)
                {
                    v.AddProperty(PrintableElementProperty.Precision, this.Precision);
                }
                
                context.AddElement(v);
            }
        }
    }
}
