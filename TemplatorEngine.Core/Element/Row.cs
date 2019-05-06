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
        XmlElement(Type = typeof(Label)),
        XmlElement(Type = typeof(Image)),
        XmlElement(Type = typeof(Barcode)),
        XmlElement(Type = typeof(Iterator)),
        XmlElement(Type = typeof(Row)),
        XmlElement(Type = typeof(Column))]
        public List<TemplateElementBase> Items { get; set; }
        
        public override bool IsLayout => true;
        
        public bool KeepTogether { get; set; }
        public override void Initialize(double? maxWidth, double? maxHeight, RenderContext context)
        {
            var ps = context.PageSettings;
            
            if (this.Height == null)
            {
                this.Height = maxHeight ?? ps.Height - ps.Margin * 2;
            }

            if (maxWidth == null)
            {
                maxWidth = ps.Width;
            }

            if (this.Width != null)
            {
                throw new Exception("Width of element Row is calculated automatically - cannot be set in XML");
            }

            var totalFixedWidth = this.Items.Where(x => x.Width != null).Sum(s => s.Width+ps.Spacing);
            var restWithToSplit = maxWidth - totalFixedWidth;
            var autoWithItemsCount = this.Items.Count(x => x.Width == null);
            var autoSizedSegmentWidth = restWithToSplit / autoWithItemsCount - ps.Spacing;

            var currentY = context.CurrentY;

            foreach (var item in this.Items)
            {
                context.CurrentY = currentY;
                
                if (item.Width == null)
                {
                    item.Width = autoSizedSegmentWidth;
                }
                
                item.Initialize(item.Width, this.Height,context);

                context.CurrentX += item.Width.Value + ps.Spacing.Value;
                
                this.Width += item.Width + ps.Spacing;
            }
        }
    }
}