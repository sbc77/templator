using System.Collections.Generic;

namespace TemplatorEngine.Core.Abstract
{
    public interface ITemplateRenderer
    {
        void Render(IEnumerable<PropertyData> data);
    }
}
