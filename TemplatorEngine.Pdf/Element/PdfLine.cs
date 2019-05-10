using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using TemplatorEngine.Core.Model;
using TemplatorEngine.Pdf.Abstract;

namespace TemplatorEngine.Pdf.Element
{
    public class PdfLine : PdfElementRendererBase
    {
        protected override void OnRender(PrintableElement element, PdfPage page)
        {
            using (var gfx = XGraphics.FromPdfPage(page))
            {
                var rect = element.AsXRect();

                gfx.DrawLine(XPens.Black, rect.Left, rect.Center.Y, rect.Right, rect.Center.Y);
            }
        }

        public override ElementType ElementType => ElementType.Line;
    }
}
