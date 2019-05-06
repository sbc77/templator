using System.Xml.Serialization;
using TemplatorEngine.Core.Abstract;
using TemplatorEngine.Core.Model;

namespace TemplatorEngine.Core.Element
{
    public class Barcode : TemplateElementBase
    {
        [XmlAttribute]
        public string DataField { get; set; }

        [XmlAttribute]
        public string Type { get; set; }
        
        [XmlAttribute]
        public string Label { get; set; }
        
        [XmlAttribute]
        public double LabelFontSize { get; set; }
        
        [XmlAttribute]
        public double Scale { get; set; }

        public override bool IsLayout => false;
        public override void Initialize(double? maxWidth, double? maxHeight, RenderContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
