using System.Collections.Generic;
using TemplatorEngine.Core.Abstract;
using TemplatorEngine.Core.Model;

namespace TemplatorEngine.Core.Element
{
    public class NewPage : TemplateElementBase
    {
        public override void Initialize(double? maxWidth, double? maxHeight, RenderContext context, IList<PropertyData> data)
        {
            if (this.Height == null)
            {
                this.Height = 0;
            }

            if (this.Width == null)
            {
                this.Width = 0;
            }
            
            var pe = new PrintableElement
            {
                ElementType = ElementType.NewPage,
                Height = this.Height.Value,
                Width = this.Width.Value,
                X = context.CurrentX ,
                Y = context.CurrentY 
            };

            context.CurrentY = context.PageSettings.Margin.Value;

            context.AddElement(pe);
        }
    }
}