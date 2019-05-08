using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using TemplatorEngine.Core.Abstract;
using TemplatorEngine.Core.Model;

namespace TemplatorEngine.Core.Element
{
    public class Image : TemplateElementBase
    {
        [XmlAttribute]
        public string Src { get; set; }
        
        public override void Initialize(double? maxWidth, double? maxHeight, RenderContext context, IList<PropertyData> data)
        {
            if (this.Height == null)
            {
                this.Height = maxHeight ?? context.MaxPageHeight;
            }

            if (this.Width == null)
            {
                this.Width = maxWidth ?? context.MaxPageWidth;
            }

            var pe = new PrintableElement
            {
                ElementType = ElementType.Image,
                Height = this.Height.Value,
                Width = this.Width.Value,
                X = context.CurrentX ,
                Y = context.CurrentY, 
                Value = this.Src
            };

            context.AddElement(pe);
        }
    }
}
