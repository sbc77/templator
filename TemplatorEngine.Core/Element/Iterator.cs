using System.Collections.Generic;
using System.Xml.Serialization;
using TemplatorEngine.Core.Abstract;
using TemplatorEngine.Core.Model;

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

        [XmlElement(Type = typeof(Field)),
        XmlElement(Type = typeof(Line)),
        XmlElement(Type = typeof(Label)),
        XmlElement(Type = typeof(Image)),
        XmlElement(Type = typeof(Barcode)),
        XmlElement(Type = typeof(Iterator)),
        XmlElement(Type = typeof(Row)),
        XmlElement(Type = typeof(Column))]
        public List<TemplateElementBase> Items { get; set; }
        
        public override bool IsLayout => false;
        public override void Initialize(double? maxWidth, double? maxHeight, RenderContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}