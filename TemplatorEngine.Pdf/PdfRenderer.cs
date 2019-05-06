﻿using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using PdfSharpCore.Drawing;
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
        private static bool fontResolverAssigned;

        public PdfRenderer(PdfConfig config, PrintTemplate template)
        {
            this.cfg = config;
            this.template = template;
        }

        public byte[] Render(IEnumerable<Page> pages)
        {
            Debug.WriteLine("Rendering started");

            if (!fontResolverAssigned)
            {
                GlobalFontSettings.FontResolver = new FontResolver(this.cfg.FontPaths);
                fontResolverAssigned = true;
            }

            var document = new PdfDocument();
            
            var ctx = new PdfRenderContext(this.template);

            foreach (var page in pages)
            {
                var pdfPage = document.AddPage();

                pdfPage.Height = XUnit.FromMillimeter(this.template.PageSettings.Height).Point;
                pdfPage.Width = XUnit.FromMillimeter(this.template.PageSettings.Width).Point;

                foreach (var element in page.Elements)
                {
                    ctx.RenderElement(element);
                }
            }


            Debug.WriteLine("Saving to PDF file");

            using (MemoryStream stream = new MemoryStream())
            {
                document.Save(stream, true);
                return stream.ToArray();
            }
        }
    }
}