using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PdfSharpCore;
using PdfSharpCore.Drawing;
using PdfSharpCore.Drawing.Layout;
using PdfSharpCore.Pdf;
using TemplatorEngine.Core.Abstract;
using TemplatorEngine.Core.Model;
using TemplatorEngine.Pdf.Abstract;
using TemplatorEngine.Pdf.Element;

namespace TemplatorEngine.Pdf
{
    public class PdfRenderContext 
    {

        private readonly List<PdfElementRendererBase> renderers = new List<PdfElementRendererBase>();

        public PdfRenderContext(PrintTemplate template)
        {
            this.PageSettings = template.PageSettings;
            this.Template = template;
            this.IsDebug = template.IsDebug;
            
            this.renderers.Add(new PdfText());
            this.renderers.Add(new PdfLine());
            this.renderers.Add(new PdfImage());
            this.renderers.Add(new PdfRect());
        }

        public PrintTemplate Template { get; }

        public bool IsDebug { get; }
        
        public PageSettings PageSettings { get; }

        private PdfElementRendererBase GetRenderer(PrintableElement element)
        {
            var r = this.renderers.SingleOrDefault(x => x.ElementType == element.ElementType);
            
            if (r == null)
            {
                throw new Exception($"Cannot find PDF renderer for element type [{element.ElementType}]");
            }

            return r;
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

        public void Render(PrintableElement element, PdfPage page)
        {
            var renderer = this.GetRenderer(element);

            renderer.Render(element,page);
            
            if (this.Template.IsDebug)
            {
                this.RenderDebug(element,page);
            }
        }

        private void RenderDebug(PrintableElement element, PdfPage page)
        {
            using (var gfx = XGraphics.FromPdfPage(page))
            {
                var rect = element.AsXRect();

                gfx.DrawRectangle(XPens.Silver, rect);
                
                var font = new XFont("Arial Narrow", 4, XFontStyle.Regular);
                var tf = new XTextFormatter(gfx) {Alignment = XParagraphAlignment.Right};
                var width = rect.Width > 18 ?   18: 9;
                var height = rect.Width > 18 ? 5 : 10;
                var dbgStr =$"{element.ElementType} ({XUnit.FromPoint(rect.X).Millimeter:F0},{XUnit.FromPoint(rect.Y).Millimeter:F0})";
                
                gfx.DrawRectangle(new XSolidBrush(XColor.FromArgb(128+64,240,240,240)), rect.TopRight.X - width, rect.Y, width, height);
                tf.DrawString(dbgStr, font, XBrushes.Navy, element.AsXRect());
            }
        }
    }
}