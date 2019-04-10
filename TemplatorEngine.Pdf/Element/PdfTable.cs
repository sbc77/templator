/*
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using TemplatorEngine.Core.Element;
using TemplatorEngine.Core.Model;

namespace TemplatorEngine.Pdf.Element
{
    public class PdfTable : PdfElementRendererBase<Table>
    {
        private IEnumerable<object> rows;
        const double RowHeight = 15;

        public override void OnSetup(Table element, object data)
        {
            this.Width = element.Width;


            var prop = data.GetType().GetProperty(element.DataField);

            this.rows = prop.GetValue(data) as IEnumerable<object>;

            this.Height = RowHeight * this.rows.Count();

            // var da = prop.GetCustomAttribute<DisplayAttribute>();

            //this.label = da?.Name ?? element.DataField;
        }

        public override void Render(PdfRenderContext ctx)
        {
            using (var gfx = XGraphics.FromPdfPage(ctx.CurrentPage))
            {
                var brush = new XPen(XColor.FromArgb(0, 0, 0));
                var valueFont = new XFont("Arial Narrow", 14);

                foreach (var row in this.rows)
                {
                    gfx.DrawString("row", valueFont, XBrushes.Black,  new XPoint(ctx.CurrentPosition.X, ctx.CurrentPosition.Y));
                    ctx.CurrentPosition.Y += RowHeight;
                }

                //gfx.DrawRectangle(brush, new XRect(currentPosition.X, currentPosition.Y, this.Width, this.Height));
            }
        }
    }
}
*/