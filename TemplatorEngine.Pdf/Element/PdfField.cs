using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using PdfSharpCore.Drawing;
using PdfSharpCore.Drawing.Layout;
using PdfSharpCore.Pdf;
using TemplatorEngine.Core.Model;
using TemplatorEngine.Core.Model.Element;

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

        public override void Render(PdfPage page, Positon currentPosition)
        {
           
            using (var gfx = XGraphics.FromPdfPage(page))
            {
                var labelFont = new XFont("Arial Narrow", 12, XFontStyle.Bold);
                var valueFont = new XFont("Arial Narrow", 14);

                var p1 = currentPosition.AsXPoint();
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
                var px = new XPoint(page.Width-currentPosition.Margin, p1.Y+ this.Height-15);
                
                var rect = new XRect(p2,px);

                // gfx.DrawRectangle(XBrushes.Silver,rect);
                tf.DrawString(this.text, valueFont, XBrushes.Black,rect);
                
            }
        }
    }
}
