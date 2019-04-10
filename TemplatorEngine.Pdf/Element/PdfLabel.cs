/*
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using TemplatorEngine.Core.Element;
using TemplatorEngine.Core.Model;

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

        public override void Render(PdfRenderContext ctx)
        {
            using (var gfx = XGraphics.FromPdfPage(ctx.CurrentPage))
            {
                var font = new XFont("Arial", 12, XFontStyle.Bold);
                gfx.DrawString(this.text, font, XBrushes.Black, new XPoint(ctx.CurrentPosition.X, ctx.CurrentPosition.Y));

            }
        }
    }
}
*/