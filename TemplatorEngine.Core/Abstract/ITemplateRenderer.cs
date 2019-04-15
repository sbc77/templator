using System.Collections.Generic;

namespace TemplatorEngine.Core.Abstract
{
    public interface ITemplateRenderer
    {
        byte[] Render(IEnumerable<PropertyData> data);
    }
}
