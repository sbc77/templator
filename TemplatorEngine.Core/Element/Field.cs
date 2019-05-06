using System.Xml.Serialization;
using TemplatorEngine.Core.Abstract;
using TemplatorEngine.Core.Model;

namespace TemplatorEngine.Core.Element
{

    public class Field : TemplateElementBase
    {
        [XmlAttribute]
        public string DataField { get; set; }
        
        [XmlAttribute]
        public string Label { get; set; }
        
        [XmlAttribute]
        public int Lines { get; set; }
        
        [XmlAttribute]
        public double FontSize { get; set; }
        
        [XmlAttribute]
        public double LabelWidth { get; set; }
        
        [XmlAttribute]
        public string LabelAlign { get; set; }
        
        [XmlAttribute]
        public string ValueAlign { get; set; }
        
        public override bool IsLayout => false;
        public override void Initialize(double? maxWidth, double? maxHeight, RenderContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
