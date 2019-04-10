
using System;
using System.Collections.Generic;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using TemplatorEngine.Core.Abstract;
using TemplatorEngine.Core.Element;
using TemplatorEngine.Core.Model;

namespace TemplatorEngine.Pdf.Element
{
    public class PdfLine : PdfElementRendererBase<Line>
    {
        protected override void OnRender(Line element, IEnumerable<PropertyData> data, PdfRenderContext ctx)
        {
            using (var gfx = XGraphics.FromPdfPage(ctx.CurrentPage))
            {
                if (element.Height <= 0)
                {
                    element.Height = 5;
                }

                var pos1 = ctx.GetPosition(0, element.Height);
                var pos2 = new Position(pos1.X,ctx.GetMaxWidth());

                gfx.DrawLine(XPens.Black, pos1.AsXPoint(), pos2.AsXPoint());
            }
        }
    }
}
