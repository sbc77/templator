using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using TemplatorEngine.Core.Model;
using TemplatorEngine.Core.Model.Element;

namespace TemplatorEngine.Pdf.Element
{
    public class PdfField : PdfElementRendererBase<Field>
    {
        private string text;
        private string label;

        private const double labelWidth = 100;

        public override void OnSetup(Field element, object data)
        {
            this.Height = 20;
            this.Width = labelWidth + 100;

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


                gfx.DrawString(this.label, labelFont, XBrushes.Black, p1);

                if (this.text == null)
                {
                    return;
                }

                var p2 = currentPosition.AsXPoint();
                p2.X += labelWidth;

                gfx.DrawString(this.text, valueFont, XBrushes.Black, p2);
            }
        }
    }
}
