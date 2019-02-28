using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using TemplatorEngine.Core.Model;
using TemplatorEngine.Core.Model.Element;

namespace TemplatorEngine.Pdf.Element
{
    public class PdfLabel : PdfElementRendererBase<Label>
    {
        private string text;
        public override void OnSetup(Label element, object data)
        {
            this.text = element.Text;
            this.Height = 15;
        }

        public override void Render(PdfPage page, Positon currentPosition)
        {
            using (var gfx = XGraphics.FromPdfPage(page))
            {
                var font = new XFont("Arial", 12, XFontStyle.Bold);
                gfx.DrawString(this.text, font, XBrushes.Black, new XPoint(currentPosition.X, currentPosition.Y));

            }
        }
    }
}
