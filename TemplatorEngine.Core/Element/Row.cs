using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using TemplatorEngine.Core.Abstract;
using TemplatorEngine.Core.Model;

namespace TemplatorEngine.Core.Element
{
    public class Row : TemplateElementBase
    {
        [XmlElement(Type = typeof(Field)),
        XmlElement(Type = typeof(Line)),
        XmlElement(Type = typeof(Space)),
        XmlElement(Type = typeof(Label)),
        XmlElement(Type = typeof(Value)),
        XmlElement(Type = typeof(Image)),
        XmlElement(Type = typeof(Barcode)),
        XmlElement(Type = typeof(Iterator)),
        XmlElement(Type = typeof(Row)),
        XmlElement(Type = typeof(Column))]
        public List<TemplateElementBase> Items { get; set; }

        public bool KeepTogether { get; set; }
        public override void Initialize(double? maxWidth, double? maxHeight, RenderContext context, IList<PropertyData> data)
        {
            var ps = context.PageSettings;
            
            if (this.Height == null)
            {
                this.Height = maxHeight ?? context.MaxPageHeight;
            }

            if (maxWidth == null)
            {
                maxWidth = context.MaxPageWidth;
            }

            if (this.Width != null)
            {
                throw new Exception("Width of element Row is calculated automatically - cannot be set in XML");
            }

            var totalFixedWidth = this.Items.Where(x => x.Width != null).Sum(s => s.Width+ps.Spacing);
            var restWithToSplit = maxWidth - totalFixedWidth+ps.Spacing;
            var autoWithItemsCount = this.Items.Count(x => x.Width == null);
            var autoSizedSegmentWidth = restWithToSplit / autoWithItemsCount - ps.Spacing.Value;

            
            // var currentX = context.CurrentX;
            var currentY = context.CurrentY;
            foreach (var item in this.Items)
            {
                
                
                if (item.Width == null)
                {
                    item.Width = autoSizedSegmentWidth;
                }
                
                item.Initialize(item.Width, this.Height,context, data);

                context.CurrentX += item.Width.Value + ps.Spacing.Value;
                
                this.Width += item.Width + ps.Spacing;
                
                context.CurrentY = currentY;
            }

            this.Height = this.Items.Max(x => x.Height);
        }
    }
}