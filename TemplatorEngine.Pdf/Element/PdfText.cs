
using System;
using System.Collections.Generic;
using PdfSharpCore.Drawing;
using PdfSharpCore.Drawing.Layout;
using PdfSharpCore.Pdf;
using TemplatorEngine.Core.Abstract;
using TemplatorEngine.Core.Element;
using TemplatorEngine.Core.Model;

namespace TemplatorEngine.Pdf.Element
{
    public class PdfText : PdfElementRendererBase
    {
        protected override void OnRender(PrintableElement element, PdfPage context)
        {
            using (var gfx = XGraphics.FromPdfPage(context))
            {
                
                var labelFont = new XFont("Arial Narrow", 12, XFontStyle.Regular); //element.FontSize.Value, XFontStyle.Regular);
                var tf = new XTextFormatter(gfx);

                // tf.Alignment = Enum.Parse<XParagraphAlignment>(element.Align);
                
                tf.DrawString(element.Value.ToString(), labelFont, XBrushes.Black, element.AsXRect());
            }
        }

        public override ElementType ElementType => ElementType.Text;
    }
}
