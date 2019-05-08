
using System;
using System.Collections.Generic;
using System.Linq;
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
                var value = element.Value.ToString();

                if (element.Value is string || element.Value is DateTime)
                {
                    tf.Alignment = XParagraphAlignment.Left;
                }
                else
                {
                    if (element.HasProperty(PrintableElementProperty.Precision))
                    {
                        var prec = element.Properties[PrintableElementProperty.Precision];
                        value = Convert.ToDecimal(element.Value).ToString("F" + prec);
                    }
                    tf.Alignment = XParagraphAlignment.Right;
                }
                
                var labelFont = this.ApplyStyle(element, tf);

                var rect = element.AsXRect();

                if (element.HasProperty(PrintableElementProperty.Rotate))
                {
                    var state = gfx.Save();
                    var angle = Convert.ToDouble(element.Properties[PrintableElementProperty.Rotate]);
                    gfx.RotateAtTransform(angle, rect.TopLeft);
                    tf.DrawString(value, labelFont, XBrushes.Black, rect);
                    gfx.Restore(state);
                }
                else
                {
                    tf.DrawString(value, labelFont, XBrushes.Black, rect);
                }
            }
        }

        private XFont ApplyStyle(PrintableElement element, XTextFormatter tf)
        {
            var fontSize = 12;
            var fontFamily = "Arial Narrow";
            var fontStyle = XFontStyle.Regular;
            

            if (element.HasProperty(PrintableElementProperty.Align))
            {
                tf.Alignment = Enum.Parse<XParagraphAlignment>(element.Properties[PrintableElementProperty.Align].ToString());
            }
            
            if (element.HasProperty(PrintableElementProperty.FontSize))
            {
                fontSize = Convert.ToInt32(element.Properties[PrintableElementProperty.FontSize]);
            }

            if (element.HasProperty(PrintableElementProperty.FontFamily))
            {
                fontFamily = element.Properties[PrintableElementProperty.FontFamily].ToString();
            }
            
            if (element.HasProperty(PrintableElementProperty.FontStyle))
            {
                fontStyle = Enum.Parse<XFontStyle>(element.Properties[PrintableElementProperty.FontStyle].ToString());
            }
            
            return new XFont(fontFamily, fontSize, fontStyle);
        }

        public override ElementType ElementType => ElementType.Text;
    }
}
