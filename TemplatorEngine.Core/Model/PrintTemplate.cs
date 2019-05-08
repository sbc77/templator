using System.Collections.Generic;
using System.Xml.Serialization;
using TemplatorEngine.Core.Abstract;
using TemplatorEngine.Core.Element;

namespace TemplatorEngine.Core.Model
{
    [XmlRoot(ElementName = "Template")]
    public class PrintTemplate
    {
        [XmlAttribute]
        public bool IsDebug { get; set; }
        
        public PageSettings PageSettings { get; set; }

        [XmlArray]
        [XmlArrayItem(Type = typeof(Field))]
        [XmlArrayItem(Type = typeof(Line))]
        [XmlArrayItem(Type = typeof(Space))]
        [XmlArrayItem(Type = typeof(Label))]
        [XmlArrayItem(Type = typeof(Value))]
        [XmlArrayItem(Type = typeof(Image))]
        [XmlArrayItem(Type = typeof(Barcode))]
        [XmlArrayItem(Type = typeof(Iterator))]
        [XmlArrayItem(Type = typeof(Row))]
        [XmlArrayItem(Type = typeof(Column))]
        public List<TemplateElementBase> ReportBody { get; set; }
        
        public IList<PrintableElement> GetPrintableElements(IList<PropertyData> data)
        {
            var context = new RenderContext(this.PageSettings);
            
            foreach (var item in this.ReportBody)
            {
                var maxWidth = this.PageSettings.Width - this.PageSettings.Margin*2;
                var maxHeight = this.PageSettings.Height - this.PageSettings.Margin*2;
                item.Initialize(maxWidth,maxHeight, context, data);
            }

            return context.PrintableElements;
        }
    }
}
