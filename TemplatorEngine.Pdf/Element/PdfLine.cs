﻿using System.Collections.Generic;
using PdfSharpCore.Drawing;
using TemplatorEngine.Core.Abstract;
using TemplatorEngine.Core.Element;
using TemplatorEngine.Core.Model;

namespace TemplatorEngine.Pdf.Element
{
    /*public class PdfLine : PdfElementRendererBase<Line>
    {
        protected override void OnRender(Line element, IEnumerable<PropertyData> data, PdfRenderContext ctx)
        {
            if (element.Height <= 0)
            {
                element.Height = 10;
            }
            
            var pos = ctx.GetPosition(0, element.Height);
            
            using (var gfx = XGraphics.FromPdfPage(ctx.CurrentPage))
            {
                
                var pos1 = new Position(pos.X, pos.Y + element.Height / 2); // in the middle of requested height
                var pos2 = new Position(gfx.PdfPage.Width - ctx.PageSettings.Margin, pos1.Y);
                
                //var rect = new XRect(pos1.AsXPoint(),pos2.AsXPoint());

                gfx.DrawLine(XPens.Black, pos1.AsXPoint(),pos2.AsXPoint());
                    //gfx.DrawRectangle(XPens.Black, rect);//pos1.AsXPoint(), pos2.AsXPoint());
            }
        }
    }*/
}
