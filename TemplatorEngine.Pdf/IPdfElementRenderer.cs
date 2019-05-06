using System.Collections.Generic;
using PdfSharpCore.Pdf;
using TemplatorEngine.Core.Abstract;
using TemplatorEngine.Core.Model;

namespace TemplatorEngine.Pdf
{
    public interface IPdfElementRenderer 
    {
        void Render(PrintableElement element, PdfPage page);
    }
}
