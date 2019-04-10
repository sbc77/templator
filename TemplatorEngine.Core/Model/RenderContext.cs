using System.Collections.Generic;
using TemplatorEngine.Core.Abstract;

namespace TemplatorEngine.Core.Model
{
    public interface IRenderContext<T>
    {
        T CurrentPage { get; }

        void SetNewPage();

        int PagesCount { get; }
        
        
        double Margin { get; set; }
        
        Position CurrentPosition { get;  }

        void RenderElement(TemplateElementBase element, IEnumerable<PropertyData> data);

        Position GetPosition(double width, double height);
    }
}
