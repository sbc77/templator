using System;
using BarcodeCore;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using TemplatorEngine.Core.Model;
using TemplatorEngine.Core.Model.Element;

namespace TemplatorEngine.Pdf.Element
{
    public class PdfBarcode : PdfElementRendererBase<Barcode>
    {
        private IBarcode gs1;
        private const int spacing = 10;
        private string barcodeStr;

        public override void OnSetup(Barcode element, object data)
        {
            this.Height = 80 + spacing * 2;

            var prop = data.GetType().GetProperty(element.DataField);

            if (prop == null)
            {
                throw new Exception($"Requested property [{element.DataField}] does not exists");
            }

            this.barcodeStr = prop.GetValue(data) as string;

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

        }

        public override void Render(PdfPage page, Positon currentPosition)
        {
            using (var gfx = XGraphics.FromPdfPage(page))
            {
                gs1.OnRenderBar = (bar) =>
                {
                    var pen = new XPen(XColors.Transparent);
                    var brush = XBrushes.Black;

                    var y = currentPosition.Y - this.Height + currentPosition.Margin + spacing;
                    gfx.DrawRectangle(pen, brush, bar.X * 1.40 + currentPosition.Margin, y, bar.Width * 1.8, this.Height - spacing * 2);
                };

                var valueFont = new XFont("Arial Narrow", 14);
                gs1.Render(this.barcodeStr);
                gfx.DrawString(this.barcodeStr, valueFont, XBrushes.Black, currentPosition.AsXPoint());
            }
        }
    }
}
