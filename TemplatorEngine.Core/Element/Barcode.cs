


using System.Xml.Serialization;
using TemplatorEngine.Core.Abstract;

namespace TemplatorEngine.Core.Model.Element
{
    public class Barcode : TemplateElementBase
    {
        [XmlAttribute]
        public string DataField { get; set; }

        [XmlAttribute]
        public string Type { get; set; }
    }
}
