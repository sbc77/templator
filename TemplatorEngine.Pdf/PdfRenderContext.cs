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
        private readonly PdfDocument document;
        private readonly IEnumerable<PropertyData> data;
        private readonly List<Type> renderers = new List<Type>();

        public PdfRenderContext(PrintTemplate template, PdfDocument document, IEnumerable<PropertyData> data)
        {
            this.template = template;
            this.document = document;
            this.data = data;
            this.PageSettings = this.template.PageSettings;
            this.CurrentPosition = new Position(this.PageSettings.Margin, this.PageSettings.Margin);
            
            this.renderers.Add(typeof(PdfLabel));
            this.renderers.Add(typeof(PdfImage));
            this.renderers.Add(typeof(PdfBarcode));
            this.renderers.Add(typeof(PdfField));
            this.renderers.Add(typeof(PdfLine));
            this.renderers.Add(typeof(PdfIterator));
        }
        public PdfPage CurrentPage { get; private set; }

        public int PagesCount { get; private set; }
        
        public PageSettings PageSettings { get; }
        
        public Position CurrentPosition { get; private set; }
        
        public void RenderElement(TemplateElementBase element, IEnumerable<PropertyData> d = null)
        {
            var renderer = this.GetRenderer(element);

            if (d == null)
            {
                d = this.data;
            }

            this.ShowDebug($"Rendering element {element.GetType()}");
            renderer.GetType().GetMethod("Render").Invoke(renderer, new object[] {element, d, this});
        }

        public Position GetPosition(double width, double height)
        {
            if (this.CurrentPage == null)
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
            
            return this.CurrentPage.Width - (this.PageSettings.Margin * 2) - this.CurrentPosition.X;
        }

        private object GetRenderer(TemplateElementBase element)
        {
            foreach (var type in this.renderers)
            {
                var renderer = (TypeInfo) type;
                
                var ii = renderer.BaseType;

                if (ii.GenericTypeArguments.All(x => x != element.GetType()))
                {
                    continue;
                }
                
                try
                {
                    return  Activator.CreateInstance(renderer) ;
                }
                catch (Exception e)
                {
                    throw e.InnerException;
                }
            }
            
            throw new Exception($"Cannot find PDF renderer for [{element.GetType().Name}]");
        }
        
        public void RequestNewPage()
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
        }

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

        private void ShowDebug(string message)
        {
            var txt = $"{this.CurrentPosition} - {message}";
            Console.WriteLine(txt);
        }
    }
}