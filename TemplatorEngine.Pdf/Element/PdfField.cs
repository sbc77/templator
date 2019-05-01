using System;
using System.Collections.Generic;
using System.Linq;
using PdfSharpCore.Drawing;
using PdfSharpCore.Drawing.Layout;
using TemplatorEngine.Core.Abstract;
using TemplatorEngine.Core.Element;
using TemplatorEngine.Core.Model;

namespace TemplatorEngine.Pdf.Element
{
    public class PdfField : PdfElementRendererBase<Field>
    {
        private double lineHeight;
        private double lines;

        protected override void OnRender(Field element, IEnumerable<PropertyData> data, PdfRenderContext ctx)
        {
            var fontSize = Utils.GetGreaterThanZeroOrDefault(12, element.FontSize, ctx.PageSettings.FontSize);

            element.LabelAlign = element.LabelAlign ?? "Left";
            element.ValueAlign = element.ValueAlign ?? "Right";

            var labelAlign = Enum.Parse<XParagraphAlignment>(element.LabelAlign);
            var valueAlign = Enum.Parse<XParagraphAlignment>(element.ValueAlign);

            this.lineHeight = element.Height <= 0 ? fontSize + 2 : element.Height;
            this.lines = element.Lines == 0 ? 1 : element.Lines;
            
            var pos = ctx.GetPosition(element.Width, this.lineHeight * this.lines + 1 );
            var propData =  data.SingleOrDefault(x => x.Name == element.DataField);

            var labelWidth = Utils.GetGreaterThanZeroOrDefault(100, element.LabelWidth);

            using (var gfx = XGraphics.FromPdfPage(ctx.CurrentPage))
            {
                this.DrawItem(gfx, pos, labelWidth-1, element.Label ?? propData?.Label, fontSize, labelAlign);

                if (propData?.Value ==null)
                {
                    return;
                }
                
                var p = new Position(pos.X + labelWidth, pos.Y);
                
                this.DrawItem(gfx, p,ctx.GetMaxWidth()-labelWidth, propData.Value.ToString(), fontSize,valueAlign,true);
            }
        }

        private void DrawItem(XGraphics gfx, Position p1, double width, string text,double fontSize, XParagraphAlignment alignment, bool bold = false)
        {
            var p2 = new Position(p1.X + width, p1.Y + this.lineHeight * this.lines);
            var rect = new XRect(p1.AsXPoint(), p2.AsXPoint());
            
            // debug
             //this.DrawAnchor(gfx, p1);
             //gfx.DrawRectangle(XBrushes.Silver,rect);
            
            var labelFont = new XFont("Arial Narrow", fontSize, bold ? XFontStyle.Bold : XFontStyle.Regular);
            var tf = new XTextFormatter(gfx);
            tf.Alignment = alignment;
            tf.DrawString(text, labelFont, XBrushes.Black, rect);
        }

        
        private void DrawAnchor(XGraphics gfx, Position p)
        {
            gfx.DrawEllipse(XPens.Blue,p.X-1, p.Y-1, 2,2);
        }
    }
}


