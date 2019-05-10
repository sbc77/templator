using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;
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

        [XmlElement(Type = typeof(Field)),
        XmlElement(Type = typeof(Line)),
        XmlElement(Type = typeof(Space)),
        XmlElement(Type = typeof(Label)),
        XmlElement(Type = typeof(Value)),
        XmlElement(Type = typeof(Image)),
        XmlElement(Type = typeof(Barcode)),
        XmlElement(Type = typeof(Iterator)),
        XmlElement(Type = typeof(Row)),
        XmlElement(Type = typeof(NewPage)),
        XmlElement(Type = typeof(Column))]
        public List<TemplateElementBase> Items { get; set; }
        
        // public override bool IsLayout => false;
        public override void Initialize(double? maxWidth, double? maxHeight, RenderContext context, IList<PropertyData> data)
        {
            this.Height = 0;
            var rows = ((IEnumerable<IEnumerable<PropertyData>>)data.Single(x => x.Name == this.DataField).Value).ToList();
            
            if (!rows.Any())
            {
                return;
            }

            var curX = context.CurrentX;
            
            foreach (var row in rows)
            {
                var flatten = this.ConvertData(row, data);
                var items = new List<TemplateElementBase>();

                foreach (var item in this.Items)
                {
                    var i = Clone(item) as TemplateElementBase;
                    
                    i.Initialize(maxWidth, maxHeight, context, flatten);
                    context.CurrentY += i.Height.Value + context.PageSettings.Spacing.Value;
                    context.CurrentX = curX;
                    items.Add(i);
                }

                context.CurrentY -= context.PageSettings.Spacing.Value;
                
            }
        }

        private IList<PropertyData> ConvertData(IEnumerable<PropertyData> row, IEnumerable<PropertyData> data)
        {
            var result = data.Where(prop => prop.Name != this.DataField).ToList();

            foreach (var rowItem in row)
            {
                var ri = new PropertyData
                {
                    Name = this.ItemReferenceName + "." + rowItem.Name,
                    Label = rowItem.Label,
                    Value = rowItem.Value
                };

                result.Add(ri);
            }

            return result;
        }

        private static object Clone(object source)
        {
            var jss = new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All};
            var s = JsonConvert.SerializeObject(source, jss);
            return JsonConvert.DeserializeObject(s, jss);
        }
    }
}