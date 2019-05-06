using System.Reflection.Metadata;
using System.Xml.Serialization;
using TemplatorEngine.Core.Abstract;
using TemplatorEngine.Core.Model;

namespace TemplatorEngine.Core.Element
{
    public class Label : TemplateElementBase
    {
        [XmlAttribute]
        public string Text { get; set; }
        
        [XmlAttribute]
        public double? FontSize { get; set; }
        
        [XmlAttribute]
        public string Align { get; set; }
        
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
            
            if (this.Align == null)
            {
                this.Align = "Left";
            }

            var pe = new PrintableElement
            {
                BaseType = typeof(Label),
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
