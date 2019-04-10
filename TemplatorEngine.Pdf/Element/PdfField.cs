
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using PdfSharpCore.Drawing;
using PdfSharpCore.Drawing.Layout;
using TemplatorEngine.Core.Abstract;
using TemplatorEngine.Core.Element;

namespace TemplatorEngine.Pdf.Element
{
    public class PdfField : PdfElementRendererBase<Field>
    {
        private const double LabelWidth = 100;
        private const double LineHeight = 18;

        protected override void OnRender(Field element, IEnumerable<PropertyData> data, PdfRenderContext ctx)
        {
            if (element.Height <= 0)
            {
                element.Height = LineHeight;
            }
            
            var pos = ctx.GetPosition(element.Width, element.Height);

            if (element.Lines == 0)
            {
                element.Lines = 1;
            }

            var propData = data.Single(x => x.Name == element.DataField);

            using (var gfx = XGraphics.FromPdfPage(ctx.CurrentPage))
            {
                var labelFont = new XFont("Arial Narrow", 12, XFontStyle.Bold);
                var valueFont = new XFont("Arial Narrow", 14);

                var p1 = pos.AsXPoint();

                gfx.DrawString(propData.Label, labelFont, XBrushes.Black, p1);

                var text = propData.Value as string;

                if (text == null)
                {
                    return;
                }

                var p2 = new XPoint(p1.X, p1.Y - LineHeight + 5);
                p2.X += LabelWidth;

                var tf = new XTextFormatter(gfx);
                var px = new XPoint(ctx.CurrentPage.Width - ctx.Margin, p1.Y + element.Height - 15);

                var rect = new XRect(p2, px);

                // gfx.DrawRectangle(XBrushes.Silver,rect);
                tf.DrawString(text, valueFont, XBrushes.Black, rect);
            }
        }
    }
}


/*using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using PdfSharpCore.Drawing;
using PdfSharpCore.Drawing.Layout;
using PdfSharpCore.Pdf;
using TemplatorEngine.Core.Element;
using TemplatorEngine.Core.Model;

namespace TemplatorEngine.Pdf.Element
{
    public class PdfField : PdfElementRendererBase<Field>
    {
        private string text;
        private string label;
        private int lines;

        private const double LabelWidth = 100;
        private const double LineHeight = 18;

        public override void OnSetup(Field element, object data)
        {
            if (element.Lines == 0)
            {
                element.Lines = 1;
            }

            this.lines = element.Lines;
            
            this.Height = LineHeight * element.Lines;
            this.Width = LabelWidth + 100;

            var prop = data.GetType().GetProperty(element.DataField);

            if (prop == null)
            {
                throw new Exception($"Requested property [{element.DataField}] does not exists");
            }

            var da = prop.GetCustomAttribute<DisplayAttribute>();

            this.label = da?.Name ?? element.DataField;
            this.text = Convert.ToString(prop.GetValue(data));
        }

        public override void Render(PdfRenderContext ctx)
        {
           
            using (var gfx = XGraphics.FromPdfPage(ctx.CurrentPage))
            {
                var labelFont = new XFont("Arial Narrow", 12, XFontStyle.Bold);
                var valueFont = new XFont("Arial Narrow", 14);

                var p1 = ctx.AsXPoint();
                p1.Y = p1.Y - ((this.lines-1)*LineHeight);

                gfx.DrawString(this.label, labelFont, XBrushes.Black, p1);

                if (this.text == null)
                {
                    return;
                }

                var p2 = new XPoint(p1.X, p1.Y-LineHeight+5);
                p2.X += LabelWidth;
                
                var sf = new XStringFormat();
                sf.LineAlignment = XLineAlignment.Near;
                sf.Alignment = XStringAlignment.Near;
                
                var tf = new XTextFormatter(gfx);
                var px = new XPoint(ctx.CurrentPage.Width - ctx.Margin, p1.Y + this.Height - 15);
                
                var rect = new XRect(p2,px);

                // gfx.DrawRectangle(XBrushes.Silver,rect);
                tf.DrawString(this.text, valueFont, XBrushes.Black, rect);
            }
        }
    }
}
*/