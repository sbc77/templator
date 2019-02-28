
using System.Xml.Serialization;
using TemplatorEngine.Core.Abstract;

namespace TemplatorEngine.Core.Model.Element
{
    public class Image : TemplateElementBase
    {
        [XmlAttribute]
        public string Src { get; set; }
    }
}
