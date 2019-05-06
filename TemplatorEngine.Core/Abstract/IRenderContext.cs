using System.Collections.Generic;
using TemplatorEngine.Core.Model;

namespace TemplatorEngine.Core.Abstract
{
    public interface IRenderContext<T>
    {
        // T CurrentPage { get; }

        // void RequestNewPage();

        //int PagesCount { get; }

        PageSettings PageSettings { get; }
        
        //Position CurrentPosition { get;  }

        void RenderElement(PrintableElement element); //, IEnumerable<PropertyData> data = null);

        // Position GetPosition(double width, double height);
    }
}
