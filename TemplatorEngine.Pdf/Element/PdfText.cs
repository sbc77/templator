
using System;
using System.Collections.Generic;
using PdfSharpCore.Drawing;
using PdfSharpCore.Drawing.Layout;
using PdfSharpCore.Pdf;
using TemplatorEngine.Core.Abstract;
using TemplatorEngine.Core.Element;
using TemplatorEngine.Core.Model;
using TemplatorEngine.Pdf.Abstract;

namespace TemplatorEngine.Pdf.Element
{
    public class PdfText : PdfElementRendererBase
    {
        protected override void OnRender(PrintableElement element, PdfPage context)
        {
            using (var gfx = XGraphics.FromPdfPage(context))
            {
                var tf = new XTextFormatter(gfx);

                if (element.Value is string || element.Value is DateTime)
                {
                    tf.Alignment = XParagraphAlignment.Left;
                }
                else
                {
                    tf.Alignment = XParagraphAlignment.Right;
                }
                
                var labelFont = this.ApplyStyle(element, tf);

                tf.DrawString(element.Value.ToString(), labelFont, XBrushes.Black, element.AsXRect());
            }
        }

        private XFont ApplyStyle(PrintableElement element, XTextFormatter tf)
        {
            var fontSize = 12;
            var fontFamily = "Arial Narrow";

            if (element.Style == null)
            {
                return new XFont(fontFamily, fontSize, XFontStyle.Regular);
            }
            
            foreach (var item in element.Style)
            {
                switch (item.Attribute)
                {
                    case StyleAttribute.Align:
                        tf.Alignment = Enum.Parse<XParagraphAlignment>(item.Value.ToString());
                        break;
                    case StyleAttribute.FontSize:
                        fontSize = Convert.ToInt32(item.Value);
                        break;
                    case StyleAttribute.FontFamily:
                        fontFamily = item.Value.ToString();
                        break;
                }
            }

            return new XFont(fontFamily, fontSize, XFontStyle.Regular);
        }

        public override ElementType ElementType => ElementType.Text;
    }
}
