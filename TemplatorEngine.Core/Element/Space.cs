using System.Collections.Generic;
using TemplatorEngine.Core.Abstract;
using TemplatorEngine.Core.Model;

namespace TemplatorEngine.Core.Element
{
    public class Space : TemplateElementBase
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
        }
    }
}