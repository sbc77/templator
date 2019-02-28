


using System.Xml.Serialization;
using TemplatorEngine.Core.Abstract;

namespace TemplatorEngine.Core.Model.Element
{

    public class Field : TemplateElementBase
    {
        [XmlAttribute]
        public string DataField { get; set; }
    }
}
