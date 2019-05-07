using PdfSharpCore.Pdf;
using TemplatorEngine.Core.Model;

namespace TemplatorEngine.Pdf.Abstract
{
    public interface IPdfElementRenderer 
    {
        void Render(PrintableElement element, PdfPage page);
    }
}
