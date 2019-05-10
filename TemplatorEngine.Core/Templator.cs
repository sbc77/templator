using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using TemplatorEngine.Core.Abstract;
using TemplatorEngine.Core.Model;

namespace TemplatorEngine.Core
{
    public class Templator
    {
        public PrintTemplate PrintTemplate { get; }

        private ITemplateRenderer renderer;

        private Templator(string templateFilePath)
        {
            var xs = new XmlSerializer(typeof(PrintTemplate));

            using (var stream = File.OpenRead(templateFilePath))
            {
                this.PrintTemplate = (PrintTemplate)xs.Deserialize(stream);
            }
        }

        public byte[] Render(object data)
        {
            this.PrintTemplate.PageSettings.Initialize();

            var d = ConvertData(data).ToList();

            var elements = this.PrintTemplate.GetPrintableElements(d);

            var pages = this.GetPages(elements);
            
            return this.renderer.Render(pages);
        }

        private IEnumerable<Page> GetPages(IEnumerable<PrintableElement> elements)
        {
            var pages = new List<Page>();

            Page currentPage = null;
            foreach (var element in elements)
            {
                if (element.ElementType == ElementType.NewPage)
                {
                    currentPage = new Page
                    {
                        Elements = new List<PrintableElement>(),
                        Height = this.PrintTemplate.PageSettings.Height,
                        Width = this.PrintTemplate.PageSettings.Width
                    };
                    
                    pages.Add(currentPage);
                    continue;
                }
                
                currentPage.Elements.Add(element);
            }

            if (currentPage.Elements.Count == 0)
            {
                pages.Remove(currentPage);
            }

            return pages;
        }

        public void SetRenderer(ITemplateRenderer rend)
        {
            this.renderer = rend;
        }

        public static Templator Create(string templateFilePath)
        {
            return new Templator(templateFilePath);
        }
        
        private static IEnumerable<PropertyData> ConvertData(object data)
        {
            var result = new List<PropertyData>();
            
            foreach (var prop in data.GetType().GetProperties())
            {
                var val = prop.GetValue(data);

                if (val is ICollection)
                {
                    var rowsResult = new List<List<PropertyData>>();

                    foreach (var row in (IEnumerable<object>)val)
                    {
                        var props = ConvertData(row).ToList();
                        rowsResult.Add(props);
                    }

                    val = rowsResult;
                }
                
                var da = prop.GetCustomAttribute<DisplayAttribute>();

                result.Add(new PropertyData
                {
                    Name = prop.Name,
                    Label = da?.Name ?? prop.Name,
                    Value = val
                });
            }

            return result;
        }
    }
}
