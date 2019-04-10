using System.Collections.Generic;
using TemplatorEngine.Core.Model;

namespace TemplatorEngine.Core.Abstract
{
    public interface ITemplateRenderer
    {
        void Render(IEnumerable<PropertyData> data);
    }
}
