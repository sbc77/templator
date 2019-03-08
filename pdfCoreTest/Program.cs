using TemplatorEngine.Core;
using TemplatorEngine.Pdf;

namespace pdfCoreTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = new Gs1Data
            {
                CustomerName = "Awesome Company Ltd",
                CustomerStreet = "Industriestrasse 666",
                CustomerZip = "CH 2555",
                CustomerCity = "Brügg",
                SsccNo = "376113650000131748",
                ArticleDescription = "Ordner, 7 cm, gelb, actually this is really long article description.",
                ArticleId = "07610811240002",
                ArticleEan = "07610811240002",
                CustomerReference = "4591354435",
                Quantity = 40,
                Barcode1 = "(02)07611365331178(37)112(400)20216916",
                Barcode2 = "(00)376113650002691578"

            };

            var cfg = new PdfConfig
            {
                OutFile = "result.pdf",
                FontPaths = new string[] { "/Library/Fonts" } // this is configured for mac, on windows you have to change it
            };

            Templator.Create("label.xml")
                    .UsePdfRenderer(cfg)
                    .Render(data);
        }
    }
}