using System.Collections.Generic;
using TemplatorEngine.Core.Model;

namespace TemplatorEngine.Core.Abstract
{
    public interface IRenderContext<T>
    {
        void Render(IEnumerable<Page> pages); //, IEnumerable<PropertyData> data = null);
        
    }
}
