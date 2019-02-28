using System.IO;
using System.Xml.Serialization;
using TemplatorEngine.Core.Abstract;
using TemplatorEngine.Core.Model;

namespace TemplatorEngine.Core
{
    public class Templator
    {
        private readonly PrintTemplate printTemplate;

        private ITemplateRenderer renderer;

        public Templator(string templateFilePath)
        {
            var xs = new XmlSerializer(typeof(PrintTemplate));

            using (var stream = File.OpenRead(templateFilePath))
            {
                this.printTemplate = (PrintTemplate)xs.Deserialize(stream);
            }
        }

        public void Render(object data)
        {
            this.printTemplate.PageSettings.Initialize();
            this.renderer.Render(this.printTemplate, data);
        }

        public void SetRenderer(ITemplateRenderer renderer)
        {
            this.renderer = renderer;
        }

        public static Templator Create(string templateFilePath)
        {
            return new Templator(templateFilePath);
        }
    }
}
