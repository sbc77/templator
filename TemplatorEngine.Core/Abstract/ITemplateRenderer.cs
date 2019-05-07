using System.Collections.Generic;
using TemplatorEngine.Core.Model;

namespace TemplatorEngine.Core.Abstract
{
    public interface ITemplateRenderer
    {
        byte[] Render(IEnumerable<Page> pages);
    }
}
