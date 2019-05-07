using System.Collections.Generic;
using System.Xml.Serialization;
using TemplatorEngine.Core.Abstract;
using TemplatorEngine.Core.Model;

namespace TemplatorEngine.Core.Element
{
    public class Label : TemplateElementBase
    {
        [XmlAttribute]
        public string Text { get; set; }
        
        [XmlIgnore]
        public double? FontSize { get; set; }
        
        [XmlAttribute("FontSize")]
        public string FontSizeStr {
            get => (this.FontSize.HasValue) ? this.FontSize.ToString() : null;
            set => this.FontSize = !string.IsNullOrEmpty(value) ? double.Parse(value) : default(double?);
        }
        
        [XmlAttribute]
        public string Align { get; set; }
        
        public override bool IsLayout => false;
        public override void Initialize(double? maxWidth, double? maxHeight, RenderContext context, IList<PropertyData> data)
        {
            if (string.IsNullOrWhiteSpace(this.Text))
            {
                return;
            }
            
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
            
            if (this.Align == null)
            {
                this.Align = "Left";
            }

            var pe = new PrintableElement
            {
                ElementType = ElementType.Text,
                // StyleName = "Label",
                Height = this.Height.Value,
                Width = this.Width.Value,
                X = context.CurrentX,
                Y = context.CurrentY,
                Value = this.Text
            };

            context.AddElement(pe);
        }
    }
}
