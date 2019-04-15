using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using PdfSharpCore.Fonts;
using PdfSharpCore.Pdf;
using TemplatorEngine.Core.Abstract;
using TemplatorEngine.Core.Model;

namespace TemplatorEngine.Pdf
{
    public class PdfRenderer : ITemplateRenderer
    {
        private readonly PdfConfig cfg;
        private readonly PrintTemplate template;
        private readonly PdfDocument document;
        
        private static bool fontResolverAssigned;

        public PdfRenderer(PdfConfig config, PrintTemplate template)
        {
            this.cfg = config;
            this.template = template;
            this.document = new PdfDocument();   
        }

        public byte[] Render(IEnumerable<PropertyData> d)
        {
            Debug.WriteLine("Rendering started");
            var data = d.ToList();
            
            if (!fontResolverAssigned)
            {
                GlobalFontSettings.FontResolver = new FontResolver(this.cfg.FontPaths);
                fontResolverAssigned = true;
            }

            var ctx = new PdfRenderContext(this.template, this.document, data);

            Debug.WriteLine($"Rendering body");
            foreach (var element in this.template.ReportBody)
            {
                ctx.RenderElement(element);
            }

            Debug.WriteLine("Saving to PDF file");
            //this.document.Save(this.cfg.OutFile);
            
            using(MemoryStream stream = new MemoryStream()) 
            { 
                this.document.Save(stream, true); 
                return stream.ToArray(); 
            }
        }
    }

    /*public class PdfRendererOld : ITemplateRenderer
    {
        private const double HeaderHeight = 20;
        private const double FooterHeight = 20;

        private static bool fontResolverAssigned;

        private readonly PdfConfig cfg;

        private readonly List<Type> renderers = new List<Type>();
        private readonly PdfDocument document;
        private PrintTemplate template;

        public PdfRendererOld(PdfConfig config)
        {
            this.cfg = config;
            this.document = new PdfDocument();
            this.renderers.Add(typeof(PdfField));
            this.renderers.Add(typeof(PdfImage));
            this.renderers.Add(typeof(PdfLabel));
            this.renderers.Add(typeof(PdfLine));
            this.renderers.Add(typeof(PdfPageNofM));
            this.renderers.Add(typeof(PdfTable));
            this.renderers.Add(typeof(PdfBarcode));
            this.renderers.Add(typeof(PdfIterator));
        }

        public void Render(PrintTemplate tmp, object data)
        {
            this.template = tmp;
            
            if (!fontResolverAssigned)
            {
                GlobalFontSettings.FontResolver = new FontResolver(this.cfg.FontPaths);
                fontResolverAssigned = true;
            }

            // var page = this.CreateNewPage(document, template, data);

            var ctx= new PdfRenderContext(() => this.CreateNewPage(data))
            {               
                Margin = this.template.PageSettings.Margin,
                X = 0,
                Y = HeaderHeight
            };

            foreach (var element in this.template.ReportBody)
            {
                this.RenderElement( element, data, ctx);
            }

            this.document.Save(this.cfg.OutFile);
        }

        private void RenderElement(TemplateElementBase element, object data, PdfRenderContext ctx)
        {
            var elementRenderer = this.GetElementRenderer(element, data);

            if (elementRenderer.Height + ctx.Y > ctx.CurrentPage.Height.Point - FooterHeight)
            {
                ctx.Y = ctx.Margin;
                ctx.PageNo++;
                ctx.CurrentPage = this.CreateNewPage( data);
            }
            else
            {
                ctx.Y += elementRenderer.Height;
            }

            elementRenderer.Render(ctx);
        }

        private void RenderHeaderElement(TemplateElementBase element, object data, PdfPage page, double margin)
        {
            var elementRenderer = this.GetElementRenderer(element, data);

            var cp = new PdfRenderContext(() => this.CreateNewPage(data))
            {
                CurrentPage = page,
                Margin = margin
            };

            elementRenderer.Render(cp);
        }

        private void RenderFooterElement(TemplateElementBase element, object data, PdfPage page, double margin)
        {
            var elementRenderer = this.GetElementRenderer(element, data);

            var cp = new PdfRenderContext(() => this.CreateNewPage(data))
            {
                CurrentPage = page,
                Margin = margin,
                X = 0,
                Y = page.Height.Point - FooterHeight
            };

            elementRenderer.Render(cp);
        }

        private PdfPage CreateNewPage(object data)
        {
            var page = this.document.AddPage();
            page.Size = Enum.Parse<PdfSharpCore.PageSize>(this.template.PageSettings.Format);

            foreach (var element in this.template.PageHeader)
            {
                this.RenderHeaderElement(element, data, page, this.template.PageSettings.Margin);
            }

            foreach (var element in this.template.PageFooter)
            {
                this.RenderFooterElement(element, data, page, this.template.PageSettings.Margin);
            }

            return page;
        }

        private IPdfElementRenderer GetElementRenderer(TemplateElementBase element, object data)
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
                    var ri = (IPdfElementRenderer)Activator.CreateInstance(renderer);
                    ri.GetType().GetMethod("Setup").Invoke(ri, new object[] { element, data });
                    return ri;
                }
                catch (Exception e)
                {
                    throw e.InnerException;
                }
            }
            
            throw new Exception($"Cannot find PDF renderer for [{element.GetType().Name}]");
        }
    }*/
}
