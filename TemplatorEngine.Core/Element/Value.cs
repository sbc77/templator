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

        public override bool IsLayout => false;

        public override void Initialize(double? maxWidth, double? maxHeight, RenderContext context,
            IList<PropertyData> data)
        {
            var styles = new List<ElementStyle>();
            var dataItem = data.SingleOrDefault(x => x.Name == this.DataField);

            if (dataItem == null)
            {
                throw new Exception($"Cannot find data for property: {this.DataField}");
            }
            
            var valueToDisplay = dataItem.Value;

            if (this.FontSize == null)
            {
                this.FontSize = context.PageSettings.FontSize;
            }
            else
            {
                styles.Add(new ElementStyle( StyleAttribute.FontSize,this.FontSize));
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
                styles.Add(new ElementStyle( StyleAttribute.Align,this.Align));
            }

            if (valueToDisplay != null)
            {
                context.AddElement(new PrintableElement
                {
                    ElementType = ElementType.Text,
                    // StyleName = this.Style ?? "Value",
                    Height = this.Height.Value,
                    Width = this.Width.Value,
                    X = context.CurrentX,
                    Y = context.CurrentY,
                    Value = valueToDisplay,
                    Style = styles
                });
            }
        }
    }
}