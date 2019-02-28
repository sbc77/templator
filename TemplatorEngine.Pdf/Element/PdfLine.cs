using System;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using TemplatorEngine.Core.Model;
using TemplatorEngine.Core.Model.Element;
namespace TemplatorEngine.Pdf.Element
{
    public class PdfLine : PdfElementRendererBase<Line>
    {


        public override void OnSetup(Line element, object data)
        {
            this.Height = 5;
        }

        public override void Render(PdfPage page, Positon currentPosition)
        {
            using (var gfx = XGraphics.FromPdfPage(page))
            {
                var pt1 = currentPosition.AsXPoint();
                var pt2 = currentPosition.AsXPoint();

                pt2.X = page.Width.Point - currentPosition.Margin;

                gfx.DrawLine(XPens.Black, pt1, pt2);
            }
        }
    }
}
