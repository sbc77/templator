
using System;
using System.Collections.Generic;
using System.Linq;
using BarcodeCore;
using PdfSharpCore.Drawing;
using TemplatorEngine.Core.Abstract;
using TemplatorEngine.Core.Element;
using TemplatorEngine.Core.Model;

namespace TemplatorEngine.Pdf.Element
{
    public class PdfBarcode : PdfElementRendererBase<Barcode>
    {
        private IBarcode gs1;

        protected override void OnRender(Barcode element, IEnumerable<PropertyData> data, PdfRenderContext ctx)
        {
            var prop = data.SingleOrDefault(x => x.Name == element.DataField);

            if (prop == null)
            {
                throw new Exception($"Requested property [{element.DataField}] does not exists");
            }

            var barcodeStr = prop.Value as string;

            if (barcodeStr == null)
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



            var barcodeHeight = element.Height <= 0 ? 80 : element.Height;
            var bPos = ctx.GetPosition(0, barcodeHeight);
            using (var gfx = XGraphics.FromPdfPage(ctx.CurrentPage))
            {
                
                
                this.gs1.OnRenderBar = (bar) =>
                {
                    var pen = new XPen(XColors.Transparent);
                    var brush = XBrushes.Black;

                    
                    gfx.DrawRectangle(pen, brush, bar.X * 1.40 + ctx.PageSettings.Margin, bPos.Y, bar.Width * 1.8, barcodeHeight-20);
                };
                
                var valueFont = new XFont("Arial Narrow", 14);
                this.gs1.Render(barcodeStr);
                
                
                var tPos = new Position(bPos.X, bPos.Y+(barcodeHeight-7));
                gfx.DrawString(barcodeStr, valueFont, XBrushes.Black, tPos.AsXPoint());
            }
        }
    }
}
