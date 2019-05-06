using PdfSharpCore.Drawing;
using TemplatorEngine.Core;
using TemplatorEngine.Core.Model;

namespace TemplatorEngine.Pdf
{
    public static class PdfExtensions
    {
        public static Templator UsePdfRenderer(this Templator templator, PdfConfig cfg)
        {
            templator.SetRenderer(new PdfRenderer(cfg, templator.PrintTemplate));
            return templator;
        }

        public static XPoint AsXPoint(this Position pos)
        {
            return new XPoint(pos.X , pos.Y );
        }

        public static XRect AsXRect(this PrintableElement item)
        {
            var x = XUnit.FromMillimeter(item.X).Point;
            var y = XUnit.FromMillimeter(item.Y).Point;
            var w = XUnit.FromMillimeter(item.Width).Point;
            var h = XUnit.FromMillimeter(item.Height).Point;

            return new XRect(x, y, w, h);
        }
    }
}
