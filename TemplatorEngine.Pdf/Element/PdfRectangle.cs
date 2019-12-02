using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using TemplatorEngine.Core.Model;
using TemplatorEngine.Pdf.Abstract;

namespace TemplatorEngine.Pdf.Element
{
    public class PdfRect : PdfElementRendererBase
    {
        protected override void OnRender(PrintableElement element, PdfPage page)
        {
            using (var gfx = XGraphics.FromPdfPage(page))
            {
                var rect = element.AsXRect();
                
                gfx.DrawRectangle(null, XBrushes.Black, rect);
            }
        }

        public override ElementType ElementType => ElementType.Rectangle;
    }
}