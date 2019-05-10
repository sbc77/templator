using System.Collections.Generic;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using TemplatorEngine.Core.Abstract;
using TemplatorEngine.Core.Element;
using TemplatorEngine.Core.Model;
using Xunit;

namespace TemplatorEngineCore.Test
{
    public class PrintTemplateTest
    {
        [Fact]
        public void ProperNewPageCreated()
        {
            var pt = new PrintTemplate
            {
                PageSettings = new PageSettings
                {
                    Format = "A4"
                },

                ReportBody = new List<TemplateElementBase>
                {
                    new Row
                    {
                        Items = new List<TemplateElementBase>
                        {
                            new Column
                            {
                                Items = new List<TemplateElementBase>
                                {
                                    new Label {Text = "This is label 1 of column 1"},
                                    new Label {Text = "This is label 2 of column 1"}
                                }
                            },

                            new Column
                            {
                                Items = new List<TemplateElementBase>
                                {
                                    new Label {Text = "This is label 1 of column 2"},
                                    new Label {Text = "This is label 2 of column 2"},
                                    new Label {Text = "This is label 3 of column 2"}
                                }
                            }
                        }
                    }
                }
            };

            var data = new List<PropertyData>();

            pt.PageSettings.Initialize();
            var result = pt.GetPrintableElements(data);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            
            var doc = new PdfDocument();

            var page = doc.AddPage();
            
            
            page.Height = XUnit.FromMillimeter(pt.PageSettings.Height).Point;
            page.Width = XUnit.FromMillimeter(pt.PageSettings.Width).Point;
            
            using (var gfx = XGraphics.FromPdfPage(page))
            {
                foreach (var item in result)
                {
                    var x = XUnit.FromMillimeter(item.X).Point;
                    var y = XUnit.FromMillimeter(item.Y).Point;
                    var w = XUnit.FromMillimeter(item.Width).Point;
                    var h = XUnit.FromMillimeter(item.Height).Point;

                    var rect = new XRect(x, y, w, h);

                    gfx.DrawRectangle(XPens.Silver, rect);

                    // var labelFont = new XFont("Arial Narrow", 12, XFontStyle.Regular);
                    // var tf = new XTextFormatter(gfx);
                    // tf.DrawString(item.Value.ToString(), labelFont, XBrushes.Black, rect);
                }
            }
            
            doc.Save("tmpTest.pdf");
        }
    }
}