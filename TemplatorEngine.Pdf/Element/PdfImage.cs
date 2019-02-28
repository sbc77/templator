using System;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using TemplatorEngine.Core.Model;
using TemplatorEngine.Core.Model.Element;

namespace TemplatorEngine.Pdf.Element
{
    public class PdfImage : PdfElementRendererBase<Image>
    {
        private string src;

        public override void OnSetup(Image element, object data)
        {
            this.Width = element.Width;
            this.Height = element.Height;
            this.src = element.Src;
        }

        public override void Render(PdfPage page, Positon currentPosition)
        {
            using (var gfx = XGraphics.FromPdfPage(page))
            {
                var img = XImage.FromFile(this.src);

                var rect = new XRect(
                    currentPosition.X + currentPosition.Margin,
                    currentPosition.Y + currentPosition.Margin,
                    this.Width,
                    this.Height);

                gfx.DrawImage(img, rect);
            }
        }
    }
}
