using PdfSharpCore.Pdf;
using TemplatorEngine.Core.Model;

namespace TemplatorEngine.Pdf
{
    public interface IPdfElementRenderer
    {
        double X { get; set; }
        double Y { get; set; }
        double Width { get; set; }
        double Height { get; set; }

        void Render(PdfPage page, Positon currentPosition);
    }
}
