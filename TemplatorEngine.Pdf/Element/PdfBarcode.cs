
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
            
            var fontSize = Utils.GetGreaterThanZeroOrDefault(12, element.LabelFontSize, ctx.PageSettings.FontSize);

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

            var posX = 0.0;

            if (element.Label != null)
            {
                posX = fontSize + 50;
            }

            var barcodeHeight = element.Height <= 0 ? 80 : element.Height;
            var bPos = ctx.GetPosition(0, barcodeHeight);

            var scale = element.Scale >0 ? element.Scale : 1;
            
            using (var gfx = XGraphics.FromPdfPage(ctx.CurrentPage))
            {
                var labelFont = new XFont("Arial", fontSize, XFontStyle.Regular);
                var barLength = barcodeHeight - 20;
                
                if (element.Label !=null)
                {
                    var state = gfx.Save();
                    var point = new XPoint(ctx.PageSettings.Margin , bPos.Y+barLength);
                    
                    gfx.RotateAtTransform(-90, point);
                    
                    var labelRect = new XRect(point.X, point.Y, barLength, 0);
                    var format = new XStringFormat {Alignment = XStringAlignment.Center};
                    gfx.DrawString(element.Label, labelFont, XBrushes.Black, labelRect, format);
                    gfx.Restore(state);
                }

                this.gs1.OnRenderBar = (bar) =>
                {
                    var pen = new XPen(XColors.Transparent);
                    var brush = XBrushes.Black;

                    var barWidth = bar.Width * 1.4 * scale;
                    if (barWidth < 1.5)
                    {
                        barWidth = 1.8;
                    }
                    
                    gfx.DrawRectangle(pen, brush, bPos.X +posX + bar.X * 1.20*scale, bPos.Y, barWidth , barLength);
                };
                
                var valueFont = new XFont("Courier New", 14);
                this.gs1.Render(barcodeStr);
                
                
                var tPos = new Position(bPos.X+posX, bPos.Y+(barcodeHeight-7));
                gfx.DrawString(barcodeStr, valueFont, XBrushes.Black, tPos.AsXPoint());
            }
        }
    }
}
