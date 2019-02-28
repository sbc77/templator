using System.Collections.Generic;
using System.Xml.Serialization;
using TemplatorEngine.Core.Abstract;
using TemplatorEngine.Core.Model.Element;

namespace TemplatorEngine.Core.Model
{
    [XmlRoot(ElementName = "Template")]
    public class PrintTemplate
    {
        public PageSettings PageSettings { get; set; }

        [XmlArray]
        [XmlArrayItem(Type = typeof(Field))]
        [XmlArrayItem(Type = typeof(Line))]
        [XmlArrayItem(Type = typeof(Label))]
        [XmlArrayItem(Type = typeof(Image))]
        [XmlArrayItem(Type = typeof(Barcode))]
        public List<TemplateElementBase> PageHeader { get; set; }


        [XmlArray]
        [XmlArrayItem(Type = typeof(Field))]
        [XmlArrayItem(Type = typeof(Line))]
        [XmlArrayItem(Type = typeof(Table))]
        [XmlArrayItem(Type = typeof(Label))]
        [XmlArrayItem(Type = typeof(Image))]
        [XmlArrayItem(Type = typeof(Barcode))]
        public List<TemplateElementBase> ReportBody { get; set; }



        [XmlArray]
        [XmlArrayItem(Type = typeof(Field))]
        [XmlArrayItem(Type = typeof(Line))]
        [XmlArrayItem(Type = typeof(Label))]
        [XmlArrayItem(Type = typeof(PageNofM))]
        [XmlArrayItem(Type = typeof(Barcode))]
        public List<TemplateElementBase> PageFooter { get; set; }
    }
}
