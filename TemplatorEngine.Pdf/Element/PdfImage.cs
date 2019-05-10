using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using TemplatorEngine.Core.Model;
using TemplatorEngine.Pdf.Abstract;

namespace TemplatorEngine.Pdf.Element
{
    public class PdfImage : PdfElementRendererBase
    {

        protected override void OnRender(PrintableElement element, PdfPage page)
        {
            using (var gfx = XGraphics.FromPdfPage(page))
            {
                var img = XImage.FromFile(element.Value.ToString());
                gfx.DrawImage(img, element.AsXRect());
            }
        }

        public override ElementType ElementType => ElementType.Image;
    }
}
