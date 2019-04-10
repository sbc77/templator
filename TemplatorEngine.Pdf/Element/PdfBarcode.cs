
using System;
using System.Collections.Generic;
using System.Linq;
using BarcodeCore;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using TemplatorEngine.Core.Abstract;
using TemplatorEngine.Core.Element;
using TemplatorEngine.Core.Model;

namespace TemplatorEngine.Pdf.Element
{
    public class PdfBarcode : PdfElementRendererBase<Barcode>
    {
        private IBarcode gs1;
        private const int spacing = 10;
        private string barcodeStr;
        


        protected override void OnRender(Barcode element, IEnumerable<PropertyData> data, PdfRenderContext ctx)
        {
            var prop = data.SingleOrDefault(x => x.Name == element.DataField);

            if (prop == null)
            {
                throw new Exception($"Requested property [{element.DataField}] does not exists");
            }

            this.barcodeStr = prop.Value as string;

            if (this.barcodeStr == null)
            {
                throw new Exception("Barcode cannot be NULL");
            }

            BarcodeTypes bt;

            switch (element.Type.ToUpper())
            {
                case "EAN128":
                    bt = BarcodeTypes.Ean_128;
                    break;
                case "GS1-128":
                    bt = BarcodeTypes.Gs1_128;
                    break;
                default:
                    throw new NotSupportedException($"Barcode type [{element.Type}] - not supported");
            }

            this.gs1 = BarcodeFactory.Create(bt);

            
            using (var gfx = XGraphics.FromPdfPage(ctx.CurrentPage))
            {
                
                var height = 80 + spacing * 2;
                var pos = ctx.GetPosition(0, height);
                
                this.gs1.OnRenderBar = (bar) =>
                {
                    var pen = new XPen(XColors.Transparent);
                    var brush = XBrushes.Black;

                    //var y = ctx.CurrentPosition.Y - height + ctx.Margin + spacing;
                    
                    
                    gfx.DrawRectangle(pen, brush, bar.X * 1.40 + ctx.Margin, pos.Y, bar.Width * 1.8, height - spacing * 2);
                };

                var valueFont = new XFont("Arial Narrow", 14);
                this.gs1.Render(this.barcodeStr);
                gfx.DrawString(this.barcodeStr, valueFont, XBrushes.Black, ctx.CurrentPosition.AsXPoint());
            }
        }
    }
}
