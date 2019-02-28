using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PdfSharpCore.Fonts;
using PdfSharpCore.Pdf;
using TemplatorEngine.Core.Abstract;
using TemplatorEngine.Core.Model;
using TemplatorEngine.Pdf.Element;

namespace TemplatorEngine.Pdf
{
    public class PdfRenderer : ITemplateRenderer
    {
        private const double HeaderHeight = 20;
        private const double FooterHeight = 20;

        private static bool fontResolverAssigned;

        private readonly PdfConfig cfg;

        private readonly List<Type> renderers = new List<Type>();

        public PdfRenderer(PdfConfig config)
        {
            this.cfg = config;
            this.renderers.Add(typeof(PdfField));
            this.renderers.Add(typeof(PdfImage));
            this.renderers.Add(typeof(PdfLabel));
            this.renderers.Add(typeof(PdfLine));
            this.renderers.Add(typeof(PdfPageNofM));
            this.renderers.Add(typeof(PdfTable));
            this.renderers.Add(typeof(PdfBarcode));
        }

        public void Render(PrintTemplate template, object data)
        {
            if (!fontResolverAssigned)
            {
                GlobalFontSettings.FontResolver = new FontResolver(this.cfg.FontPaths);
                fontResolverAssigned = true;
            }

            var document = new PdfDocument();

            var page = CreateNewPage(document, template, data);

            var currentPosition = new Positon
            {
                PageNo = 1,
                Margin = template.PageSettings.Margin,
                X = 0,
                Y = HeaderHeight
            };

            foreach (var element in template.ReportBody)
            {
                this.RenderElement(document, element, template, data, page, currentPosition);
            }

            document.Save(this.cfg.OutFile);
        }

        private void RenderElement(PdfDocument document, TemplateElementBase element, PrintTemplate template, object data, PdfPage page, Positon currentPosition)
        {
            var elementRenderer = this.GetElementRenderer(element, data);

            if (elementRenderer.Height + currentPosition.Y > page.Height.Point - FooterHeight)
            {
                currentPosition.Y = currentPosition.Margin;
                currentPosition.PageNo++;
                page = CreateNewPage(document, template, data);
            }
            else
            {
                currentPosition.Y += elementRenderer.Height;
            }

            elementRenderer.Render(page, currentPosition);
        }

        private void RenderHeaderElement(TemplateElementBase element, object data, PdfPage page, double margin)
        {
            var elementRenderer = this.GetElementRenderer(element, data);

            var cp = new Positon
            {
                Margin = margin
            };

            elementRenderer.Render(page, cp);
        }

        private void RenderFooterElement(TemplateElementBase element, object data, PdfPage page, double margin)
        {
            var elementRenderer = this.GetElementRenderer(element, data);

            var cp = new Positon
            {
                Margin = margin,
                X = 0,
                Y = page.Height.Point - FooterHeight
            };

            elementRenderer.Render(page, cp);
        }

        private PdfPage CreateNewPage(PdfDocument document, PrintTemplate template, object data)
        {
            var page = document.AddPage();
            page.Size = Enum.Parse<PdfSharpCore.PageSize>(template.PageSettings.Format);

            foreach (var element in template.PageHeader)
            {
                this.RenderHeaderElement(element, data, page, template.PageSettings.Margin);
            }

            foreach (var element in template.PageFooter)
            {
                this.RenderFooterElement(element, data, page, template.PageSettings.Margin);
            }

            return page;
        }

        private IPdfElementRenderer GetElementRenderer(TemplateElementBase element, object data)
        {
            foreach (TypeInfo renderer in this.renderers)
            {
                var ii = renderer.BaseType;
                if (ii.GenericTypeArguments.Any(x => x == element.GetType()))
                {
                    var ri = Activator.CreateInstance(renderer) as IPdfElementRenderer;
                    try
                    {
                        ri.GetType().GetMethod("Setup").Invoke(ri, new object[] { element, data });
                    }
                    catch (Exception e)
                    {
                        throw e.InnerException;
                    }


                    return ri;

                }
            }

            throw new Exception($"Cannot find PDF renderer for [{element.GetType().Name}]");
        }
    }
}
