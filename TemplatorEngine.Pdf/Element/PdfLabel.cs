
using System;
using System.Collections.Generic;
using PdfSharpCore.Drawing;
using PdfSharpCore.Drawing.Layout;
using TemplatorEngine.Core.Abstract;
using TemplatorEngine.Core.Element;
using TemplatorEngine.Core.Model;

namespace TemplatorEngine.Pdf.Element
{
    public class PdfLabel : PdfElementRendererBase<Label>
    {
        private double lineHeight;

        protected override void OnRender(Label element, IEnumerable<PropertyData> data, PdfRenderContext ctx)
        {
            if (element.Align == null)
            {
                element.Align = "Left";
            }
            
            var fontSize = Utils.GetGreaterThanZeroOrDefault(12, element.FontSize, ctx.PageSettings.FontSize);
            this.lineHeight = element.Height <= 0 ? fontSize + 2 : element.Height;

            if (element.Width <= 0)
            {
                element.Width = ctx.GetMaxWidth();
            }

            var p1 = ctx.GetPosition(element.Width, this.lineHeight + 1);
            
            using (var gfx = XGraphics.FromPdfPage(ctx.CurrentPage))
            {
                var p2 = new Position(p1.X + element.Width, p1.Y + this.lineHeight);
                var rect = new XRect(p1.AsXPoint(), p2.AsXPoint());
            
                var labelFont = new XFont("Arial Narrow", fontSize, XFontStyle.Regular);
                var tf = new XTextFormatter(gfx);

                tf.Alignment = Enum.Parse<XParagraphAlignment>(element.Align);
                
                tf.DrawString(element.Text, labelFont, XBrushes.Black, rect);
            }
        }
    }
}
