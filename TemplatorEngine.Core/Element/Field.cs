using System.Xml.Serialization;
using TemplatorEngine.Core.Abstract;

namespace TemplatorEngine.Core.Element
{

    public class Field : TemplateElementBase
    {
        [XmlAttribute]
        public string DataField { get; set; }
        
        [XmlAttribute]
        public int Lines { get; set; }
    }
}
