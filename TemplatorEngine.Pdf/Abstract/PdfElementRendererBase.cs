using PdfSharpCore.Pdf;
using TemplatorEngine.Core.Model;

namespace TemplatorEngine.Pdf.Abstract
{
    public abstract class PdfElementRendererBase :IPdfElementRenderer 
    {
        protected abstract void OnRender(PrintableElement element,  PdfPage context);
        
        public abstract ElementType ElementType { get; }

        public void Render(PrintableElement element, PdfPage page)
        {

            this.OnRender(element, page);
        }
    }
}