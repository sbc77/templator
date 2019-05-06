using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using TemplatorEngine.Core.Abstract;
using TemplatorEngine.Core.Model;
using TemplatorEngine.Pdf.Element;

namespace TemplatorEngine.Pdf
{
    public class PdfRenderContext : IRenderContext<PdfPage>
    {
        private readonly PrintTemplate template;
        //private readonly PdfDocument document;
        private readonly List<PdfElementRendererBase> renderers = new List<PdfElementRendererBase>();

        public PdfRenderContext(PrintTemplate template)
        {
            this.template = template;
            //this.document = document;
            this.PageSettings = this.template.PageSettings;
            // this.CurrentPosition = new Position(this.PageSettings.Margin.Value, this.PageSettings.Margin);
            this.IsDebug = template.IsDebug;
            
            this.renderers.Add(new PdfText());
            
            //this.renderers.Add(ElementType.Text, typeof(PdfLabel));
            //this.renderers.Add(ElementType.Line, typeof(PdfLine));
            //this.renderers.Add(typeof(PdfImage));
            //this.renderers.Add(typeof(PdfBarcode));
            //this.renderers.Add(typeof(PdfField));
            //this.renderers.Add(typeof(PdfLine));
        }
        // public PdfPage CurrentPage { get; private set; }

        // public int PagesCount { get; private set; }
        
        public bool IsDebug { get; }
        
        public PageSettings PageSettings { get; }

        // public Position CurrentPosition { get; private set; }

        public void RenderElement(PrintableElement element, PdfPage page) //, IEnumerable<PropertyData> d = null)
        {

            var renderer = this.GetRenderer(element);

            renderer.Render(element,page);
        }

        /*public Position GetPosition(double width, double height)
        {
            if (this.CurrentPage == null )
            {
                this.RequestNewPage();
            }

            if (this.CurrentPosition.Y + this.PageSettings.Margin+height > this.CurrentPage.Height.Value )
            {
                this.RequestNewPage();
            }
            
            var oldPos = this.CurrentPosition;

            this.CurrentPosition = new Position(this.PageSettings.Margin, oldPos.Y + height);

            return oldPos;
        }

        public double GetMaxWidth()
        {
            if (this.CurrentPage == null)
            {
                this.RequestNewPage();
            }

            return this.CurrentPage.Width - (this.PageSettings.Margin * 2);// - this.CurrentPosition.X;
        }*/

        private PdfElementRendererBase GetRenderer(PrintableElement element)
        {
            var r = this.renderers.SingleOrDefault(x => x.ElementType == element.ElementType);
            
            if (r == null)
            {
                throw new Exception($"Cannot find PDF renderer for [{element.GetType().Name}]");
            }

            return r;
        }

        /*public void RequestNewPage()
        {
            this.ShowDebug("New page request");
            
            if (this.document.PageCount == 0)
            {
                this.CreateNewPage();
            }
            else
            {
                var c = this.document.Pages[this.document.PageCount - 1];

                if (HasContent(c))
                {
                    this.CreateNewPage();
                }
                else
                {
                    this.ShowDebug("Empty page re-used");
                }
            }
            
            this.ShowDebug($"Rendering PageHeader Start");
            
            foreach (var element in this.template.PageHeader)
            {
                this.RenderElement(element);
            }
            
            this.ShowDebug($"Rendering PageHeader End");
        }

        private void CreateNewPage()
        {
            var page = this.document.AddPage();
            this.CurrentPosition = new Position(this.PageSettings.Margin, this.PageSettings.Margin);
            this.CurrentPage = page;
            this.PagesCount++;
            page.Size = Enum.Parse<PageSize>(this.template.PageSettings.Format);
            
            this.ShowDebug("New page added");
        }*/

        private static bool HasContent(PdfPage page)
        {
            for(var i = 0; i < page.Contents.Elements.Count; i++)
            {
                if (page.Contents.Elements.GetDictionary(i).Stream.Length > 76)
                {
                    return true;
                }
            }
            return false;
        }

        /*private void ShowDebug(string message)
        {
            var txt = $"{this.CurrentPosition} - {message}";
            Console.WriteLine(txt);
        }*/
    }
}