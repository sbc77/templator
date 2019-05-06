using System.Xml.Serialization;
using TemplatorEngine.Core.Model;

namespace TemplatorEngine.Core.Abstract
{
    public abstract class TemplateElementBase
    {
        [XmlElement(IsNullable = true)]
        public double? X { get; set; }

        [XmlElement(IsNullable = true)]
        public double? Y { get; set; }

        [XmlElement(IsNullable = true)]
        public double? Width { get; set; }

        [XmlElement(IsNullable = true)]
        public double? Height { get; set; }
        
        [XmlIgnore]
        public abstract bool IsLayout { get; }

        public abstract void Initialize(double? maxWidth, double? maxHeight, RenderContext ctx);
    }
}