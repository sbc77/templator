using PdfSharpCore.Drawing;
using TemplatorEngine.Core;
using TemplatorEngine.Core.Model;

namespace TemplatorEngine.Pdf
{
    public static class PdfExtensions
    {
        public static Templator UsePdfRenderer(this Templator templator, PdfConfig cfg)
        {
            templator.SetRenderer(new PdfRenderer(cfg));
            return templator;
        }

        public static XPoint AsXPoint(this Positon position)
        {
            return new XPoint(position.X + position.Margin, position.Y + position.Margin);
        }
    }
}
