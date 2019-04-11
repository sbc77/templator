

using System.Xml.Serialization;

namespace TemplatorEngine.Core.Abstract
{
    public abstract class TemplateElementBase
    {
        [XmlAttribute]
        public double X { get; set; }

        [XmlAttribute]
        public double Y { get; set; }

        [XmlAttribute]
        public double Width { get; set; }

        [XmlAttribute]
        public double Height { get; set; }
       
    }
}