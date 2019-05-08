using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using pdfCoreTest.Model;
using TemplatorEngine.Core;
using TemplatorEngine.Pdf;

namespace pdfCoreTest
{
    class Program
    {
        static void Main(string[] args)
        {
           //GenerateLabel();
           GenerateInvoice();
        }

        private static void GenerateLabel()
        {
            var data = GetGs1Data();

            var cfg = new PdfConfig
            {
                FontPaths = new []{ "/Library/Fonts" } // this is configured for mac, on windows you have to change it
            };

            var bytes = Templator.Create("Template/label.xml")
                .UsePdfRenderer(cfg)
                .Render(data);
            
            File.WriteAllBytes("Result/label.pdf",bytes);
        }
        
        private static void GenerateInvoice()
        {
            var data = GetInvoiceData();

            var cfg = new PdfConfig
            {
                FontPaths = new []{ "/Library/Fonts" } // this is configured for mac, on windows you have to change it
            };

            var bytes = Templator.Create("Template/invoice.xml")
                .UsePdfRenderer(cfg)
                .Render(data);
            
            File.WriteAllBytes("Result/invoice.pdf",bytes);
        }
            

        private static Gs1Data GetGs1Data()
        {
            return new Gs1Data
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
        }

        private static Invoice GetInvoiceData()
        {
            var items = new List<InvoiceItem>();

            for (var i = 1; i <= 50; i++)
            {
                items.Add(new InvoiceItem
                {
                    PosNo = i,
                    ArticleId = $"ART{i:D5}",
                    ArticleDescription = $"Quite long article {i:D5} description",
                    Quantity = 10 + i,
                    UoM = "Pcs",
                    TaxId = "7.7%",
                    NetAmount = i * 10,
                    TaxAmount = i * 10 * 0.077m,
                    Amount = i * 10 + (i * 10 * 0.077m)
                });
            }


            return new Invoice
            {
                Created = DateTime.Now,
                No = "INV/2019/000321",
                Barcode = "(00)376113650002691578",
                CustomerName = "Apfel GmbH",
                SupplierName = "Biella AG",
                Items = items,
                TotalAmount = items.Sum(x=>x.Amount),
                TotalNetAmount = items.Sum(x=>x.NetAmount)
            };
        }
    }
}
