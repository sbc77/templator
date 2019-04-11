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

        /*public static PropertyData GetPropertyData(this object data, string name)
        {
     
        }*/
    }
}
