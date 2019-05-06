
using TemplatorEngine.Core.Abstract;
using TemplatorEngine.Core.Model;

namespace TemplatorEngine.Core.Element
{
    public class Line : TemplateElementBase
    {
        public override bool IsLayout => false;
        public override void Initialize(double? maxWidth, double? maxHeight, RenderContext context)
        {
            if (this.Height == null)
            {
                this.Height = 5;
            }

            if (this.Width == null)
            {
                this.Width = maxWidth ?? context.PageSettings.Width;
            }
            
            var pe = new PrintableElement
            {
                ElementType = ElementType.Line,
                Height = this.Height.Value,
                Width = this.Width.Value,
                X = context.CurrentX + (this.Height.Value/2),
                Y = context.CurrentY,
            };

            context.AddElement(pe);
        }
    }
}
