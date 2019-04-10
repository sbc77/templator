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
        private readonly List<Type> renderers = new List<Type>();

        public PdfRenderContext(PrintTemplate template, PdfDocument document)
        {
            this.template = template;
            this.document = document;
            this.CurrentPosition = new Position(this.Margin, this.Margin);
            
            /*
            this.renderers.Add(typeof(PdfLabel));
            this.renderers.Add(typeof(PdfPageNofM));
            this.renderers.Add(typeof(PdfTable));
            */
            
            this.renderers.Add(typeof(PdfImage));
            this.renderers.Add(typeof(PdfBarcode));
            this.renderers.Add(typeof(PdfField));
            this.renderers.Add(typeof(PdfLine));
            this.renderers.Add(typeof(PdfIterator));
        }
        public PdfPage CurrentPage { get; private set; }
        
        // public Func<PdfPage> PageFactory { get; set; }
        
        public void SetNewPage()
        {
            this.CurrentPage = this.CreateNewPage();
            this.CurrentPosition= new Position(this.Margin, this.Margin);
            this.PagesCount++;
        }

        public int PagesCount { get; private set; }

        public double Margin { get; set; }
        
        public Position CurrentPosition { get; private set; }
        
        public void RenderElement(TemplateElementBase element, IEnumerable<PropertyData> data)
        {
            var renderer = this.GetRenderer(element);

            renderer.GetType().GetMethod("Render").Invoke(renderer, new object[] {element, data, this});
        }

        public Position GetPosition(double width, double height)
        {
            var oldPos = this.CurrentPosition;

            this.CurrentPosition = new Position(this.Margin, oldPos.Y + height);

            return oldPos;
        }

        public double GetMaxWidth() => this.CurrentPage.Width - (this.Margin * 2) - this.CurrentPosition.X;

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
        
        private PdfPage CreateNewPage(IEnumerable<PropertyData> data)
        {
            PdfPage page;
            
            if (this.document.PageCount == 0)
            {
                page = this.document.AddPage();    
            }
            else
            {
                var c = this.document.Pages[this.document.PageCount - 1];
                
                page = HasContent(c) ? this.document.AddPage() : this.document.Pages[this.document.PageCount - 1];
            }
            
            page.Size = Enum.Parse<PageSize>(this.template.PageSettings.Format);
            
            foreach (var element in this.template.PageHeader)
            {
                this.RenderElement(element, data);
            }
            
            return page;
        }
        
        public static bool HasContent(PdfPage page)
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
    }
}