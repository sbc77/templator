using System.Xml.Serialization;
using TemplatorEngine.Core.Abstract;

namespace TemplatorEngine.Core.Element
{
    public class Label : TemplateElementBase
    {
        [XmlAttribute]
        public string Text { get; set; }
        
        [XmlAttribute]
        public double FontSize { get; set; }
        
        [XmlAttribute]
        public string Align { get; set; }
    }
}
