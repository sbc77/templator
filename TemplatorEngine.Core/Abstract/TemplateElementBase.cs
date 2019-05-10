using System.Collections.Generic;
using System.Xml.Serialization;
using TemplatorEngine.Core.Model;

namespace TemplatorEngine.Core.Abstract
{
    public abstract class TemplateElementBase
    {
        [XmlIgnore]
        public double? X { get; set; }
        
        [XmlAttribute(AttributeName = "X")]
        public string Xs {
            get => (this.X.HasValue) ? this.X.ToString() : null;
            set => this.X = !string.IsNullOrEmpty(value) ? double.Parse(value) : default(double?);
        } 
        
        [XmlIgnore]
        public double? Y { get; set; }
        
        [XmlAttribute(AttributeName = "Y")]
        public string Ys {
            get => (this.Y.HasValue) ? this.Y.ToString() : null;
            set => this.Y = !string.IsNullOrEmpty(value) ? double.Parse(value) : default(double?);
        } 

        [XmlIgnore]
        public double? Width { get; set; }
        
        [XmlAttribute(AttributeName = "Width")]
        public string WidthS {
            get => (this.Width.HasValue) ? this.Width.ToString() : null;
            set => this.Width = !string.IsNullOrEmpty(value) ? double.Parse(value) : default(double?);
        }

        [XmlIgnore]
        public double? Height { get; set; }
        
        [XmlAttribute(AttributeName = "Height")]
        public string HeightS {
            get => (this.Height.HasValue) ? this.Height.ToString() : null;
            set => this.Height = !string.IsNullOrEmpty(value) ? double.Parse(value) : default(double?);
        }

        public abstract void Initialize(double? maxWidth, double? maxHeight, RenderContext ctx, IList<PropertyData>  data);
    }
}