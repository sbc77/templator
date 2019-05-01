using System.Collections.Generic;
using System.IO;
using System.Net;
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
                ShipmentId = 123456,
                CustomerName = "Awesome Company Ltd",
                CustomerStreet = "Industriestrasse 666",
                CustomerZip = "CH 2555",
                CustomerCity = "Brügg",
                CustomerReference = "4591354435",
                Group = "SBD-71",
                Rows = new List<Gs1DataRow>
                {
                    new Gs1DataRow
                    {
                        SsccNo = "376113650000131748",
                        ArticleDescription = "Ordner, 7 cm, gelb, actually this is really long article description, long enough to be wrapped.",
                        ArticleId = "07610811240002",
                        ArticleEan = "07610811240002",
                        Quantity = 40,
                        Barcode1 = "(02)07611365331178(37)112(400)20216916",
                        Barcode2 = "(00)376113650002691578",
                        Package = "1 von 2"
                        
                    },
                    new Gs1DataRow
                    {
                        SsccNo = "376113650000131747",
                        ArticleDescription = "Ordner, 5 cm, grün",
                        ArticleId = "07610811240001",
                        ArticleEan = "07610811240001",
                        Quantity = 20,
                        Barcode1 = "(02)07611365331178(37)112(400)20216916",
                        Barcode2 = "(00)376113650002691581"        ,
                        Package = "2 von 2"
                    }
                }
            };

            var cfg = new PdfConfig
            {
                FontPaths = new []{ "/Library/Fonts" } // this is configured for mac, on windows you have to change it
            };

            var bytes = Templator.Create("label.xml")
                    .UsePdfRenderer(cfg)
                    .Render(data);
            
            File.WriteAllBytes("result.pdf",bytes);
        }
    }
}
