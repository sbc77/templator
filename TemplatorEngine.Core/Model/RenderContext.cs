using System.Collections.Generic;
using TemplatorEngine.Core.Abstract;

namespace TemplatorEngine.Core.Model
{
    public interface IRenderContext<T>
    {
        T CurrentPage { get; }

        void RequestNewPage();

        int PagesCount { get; }

        PageSettings PageSettings { get; }
        
        Position CurrentPosition { get;  }

        void RenderElement(TemplateElementBase element, IEnumerable<PropertyData> data = null);

        Position GetPosition(double width, double height);
    }
}
