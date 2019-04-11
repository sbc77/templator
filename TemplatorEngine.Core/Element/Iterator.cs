using System.Collections.Generic;
using System.Xml.Serialization;
using TemplatorEngine.Core.Abstract;

namespace TemplatorEngine.Core.Element
{
    
    public class Iterator : TemplateElementBase
    {
        [XmlAttribute]
        public string DataField { get; set; }
        
        [XmlAttribute]
        public string ItemReferenceName { get; set; }
        
        [XmlAttribute]
        public bool UseNewPage { get; set; }
     
        [XmlArray]
        [XmlArrayItem(Type = typeof(Field))]
        [XmlArrayItem(Type = typeof(Line))]
        [XmlArrayItem(Type = typeof(Table))]
        [XmlArrayItem(Type = typeof(Label))]
        [XmlArrayItem(Type = typeof(Image))]
        [XmlArrayItem(Type = typeof(Barcode))]
        [XmlArrayItem(Type = typeof(Iterator))]
        public List<TemplateElementBase> Items { get; set; }
    }
}