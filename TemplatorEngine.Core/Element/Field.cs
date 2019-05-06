using System.Xml.Serialization;
using TemplatorEngine.Core.Abstract;
using TemplatorEngine.Core.Model;

namespace TemplatorEngine.Core.Element
{

    public class Field : TemplateElementBase
    {
        [XmlAttribute]
        public string DataField { get; set; }
        
        [XmlAttribute]
        public string Label { get; set; }
        
        [XmlAttribute]
        public int Lines { get; set; }
        
        
        [XmlIgnore]
        public double? FontSize { get; set; }
        
        public string FontSizeStr {
            get => (this.FontSize.HasValue) ? this.FontSize.ToString() : null;
            set => this.FontSize = !string.IsNullOrEmpty(value) ? double.Parse(value) : default(double?);
        }
        
        [XmlIgnore]
        public double? LabelWidth { get; set; }
        
        [XmlAttribute(AttributeName = "LabelWidth")]
        public string LabelWidthStr {
            get => (this.LabelWidth.HasValue) ? this.LabelWidth.ToString() : null;
            set => this.LabelWidth = !string.IsNullOrEmpty(value) ? double.Parse(value) : default(double?);
        }
        
        [XmlAttribute]
        public string LabelAlign { get; set; }
        
        [XmlAttribute]
        public string ValueAlign { get; set; }
        
        public override bool IsLayout => false;
        public override void Initialize(double? maxWidth, double? maxHeight, RenderContext context)
        {
            if (this.FontSize == null)
            {
                this.FontSize = context.PageSettings.FontSize;
            }

            if (this.Width == null)
            {
                this.Width = maxWidth ?? context.PageSettings.Width;
            }

            if (this.Height == null)
            {
                this.Height = 20;
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
                this.LabelWidth = this.Width / 2;
            }

            var lb = new PrintableElement
            {
                ElementType = ElementType.Text,
                StyleName = "Label",
                Height = this.Height.Value,
                Width = this.LabelWidth.Value,
                X = context.CurrentX,
                Y = context.CurrentY,
                Value = this.Label
            };
            
            var vl = new PrintableElement
            {
                ElementType = ElementType.Text,
                StyleName = "Value",
                Height = this.Height.Value,
                Width = this.Width.Value,
                X = context.CurrentX,
                Y = context.CurrentY,
                Value = "[data]"
            };

            context.AddElement(lb);
            context.AddElement(vl);
        }
    }
}
