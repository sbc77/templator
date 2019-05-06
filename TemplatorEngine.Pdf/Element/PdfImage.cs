using System.Collections.Generic;
using PdfSharpCore.Drawing;
using TemplatorEngine.Core.Abstract;
using TemplatorEngine.Core.Element;

namespace TemplatorEngine.Pdf.Element
{
    /*public class PdfImage : PdfElementRendererBase<Image>
    {
        protected override void OnRender(Image element, IEnumerable<PropertyData> data, PdfRenderContext ctx)
        {
            var width = element.Width;
            var height = element.Height;
            var pos = ctx.GetPosition(width, height);
            
            using (var gfx = XGraphics.FromPdfPage(ctx.CurrentPage))
            {
                var img = XImage.FromFile(element.Src);
                var rect = new XRect(pos.X,pos.Y,width,height);

                gfx.DrawImage(img, rect);
            }
        }
    }*/
}
