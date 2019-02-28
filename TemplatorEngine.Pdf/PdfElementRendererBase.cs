using PdfSharpCore.Pdf;
using TemplatorEngine.Core.Abstract;
using TemplatorEngine.Core.Model;

namespace TemplatorEngine.Pdf
{
    public abstract class PdfElementRendererBase<T> : IPdfElementRenderer where T : TemplateElementBase
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public void Setup(T element, object data)
        {
            this.Width = element.Width;
            this.Height = element.Height;
            this.X = element.X;
            this.Y = element.Y;

            this.OnSetup(element, data);
        }

        public abstract void OnSetup(T element, object data);

        public abstract void Render(PdfPage page, Positon currentPosition);
    }
}
