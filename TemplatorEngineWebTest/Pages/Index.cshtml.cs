using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TemplatorEngine.Core;
using TemplatorEngine.Pdf;
using TemplatorEngineWebTest.Model;

namespace TemplatorEngineWebTest.Pages
{
    public class IndexModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            var cfg = new PdfConfig
            {
                FontPaths = new[] {"/Library/Fonts" },
            };

            var bytes = Templator
                .Create("print.xml")
                .UsePdfRenderer(cfg)
                .Render(GetData());

            await Task.CompletedTask;

            return this.File(bytes, "application/pdf");
        }

        private static object GetData()
        {
            var rows = new List<Gs1DataRow>();

            for (var i = 0; i < 20; i++)
            {
                rows.Add(

                    new Gs1DataRow
                    {
                        SsccNo = "376113650000131748",
                        ArticleDescription =
                            "Ordner, 7 cm, gelb, actually this is really long article description, long enough to be wrapped.",
                        ArticleId = $"07610811240002-{i:D2}",
                        ArticleEan = $"07610811240002-{i:D2}",
                        Quantity = 40,
                        Barcode = $"(02)076113653311{i:D2}(37)112(400)20216916"

                    });
            }
            
            return new Gs1Data
            {
                CustomerName = "Awesome Company Ltd",
                CustomerStreet = "Industriestrasse 666",
                CustomerZip = "CH 2555",
                CustomerCity = "Brügg",
                CustomerReference = "4591354435",
                SsccBarcode  = "(00)376113650002691578",
                Lines = rows
            };
        }
    }
}