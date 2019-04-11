using System.Collections.Generic;
using TemplatorEngine.Core.Abstract;

namespace TemplatorEngine.Pdf
{
    public interface IPdfElementRenderer 
    {
        /*double X { get; set; }
        double Y { get; set; }
        double Width { get; set; }
        double Height { get; set; }*/

        void Render(TemplateElementBase element, IEnumerable<PropertyData> data, PdfRenderContext context);
    }
}
