using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace TemplatorEngine.Core.Model
{
    public class PageSettings
    {
        private readonly IEnumerable<PageDimension> dimensions = new List<PageDimension>()
        {
            new PageDimension("A0", 841, 1189),
            new PageDimension("A1", 594, 841),
            new PageDimension("A2", 420, 594),
            new PageDimension("A3", 297, 420),
            new PageDimension("A4", 210, 297),
            new PageDimension("A5", 148, 210),
            new PageDimension("A6", 105, 148),
            new PageDimension("A7", 74,  105),
            new PageDimension("A8", 52,  74 )
        };

        [XmlAttribute]
        public Orientation Orientation { get; set; }

        [XmlAttribute]
        public string Format { get; set; }

        [XmlAttribute]
        public double Width { get; set; }

        [XmlAttribute]
        public double Height { get; set; }

        [XmlAttribute]
        public double Margin { get; set; }

        public void Initialize()
        {
            if (this.Format != null)
            {
                var d = dimensions.Single(x => x.Name == this.Format);

                if (this.Orientation == Orientation.Portrait)
                {
                    this.Width = d.Width;
                    this.Height = d.Height;
                }
                else
                {
                    this.Width = d.Height;
                    this.Height = d.Width;
                }
            }
        }
    }
}
