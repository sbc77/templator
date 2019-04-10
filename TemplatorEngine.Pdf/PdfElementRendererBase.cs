using System.Collections.Generic;
using PdfSharpCore.Pdf;
using TemplatorEngine.Core.Abstract;
using TemplatorEngine.Core.Model;

namespace TemplatorEngine.Pdf
{
    public abstract class PdfElementRendererBase<T>: IPdfElementRenderer where T: TemplateElementBase
    {
        /*public double X { get; set; }
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

        public abstract void OnSetup(T element, object data);*/

        protected abstract void OnRender(T element, IEnumerable<PropertyData> data, PdfRenderContext context);

        public void Render(TemplateElementBase element, IEnumerable<PropertyData> data, PdfRenderContext context)
        {
            this.OnRender((T)element, data,context);
        }
    }
}
