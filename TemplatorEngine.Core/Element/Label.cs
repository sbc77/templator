using System.Xml.Serialization;
using TemplatorEngine.Core.Abstract;

namespace TemplatorEngine.Core.Element
{
    public class Label : TemplateElementBase
    {
        [XmlAttribute]
        public string Text { get; set; }
    }
}
