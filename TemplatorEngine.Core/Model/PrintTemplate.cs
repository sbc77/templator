﻿using System.Collections.Generic;
using System.Xml.Serialization;
using TemplatorEngine.Core.Abstract;
using TemplatorEngine.Core.Element;

namespace TemplatorEngine.Core.Model
{
    [XmlRoot(ElementName = "Template")]
    public class PrintTemplate
    {
        public bool IsDebug { get; set; }
        
        public PageSettings PageSettings { get; set; }

        [XmlArray]
        [XmlArrayItem(Type = typeof(Field))]
        [XmlArrayItem(Type = typeof(Line))]
        [XmlArrayItem(Type = typeof(Label))]
        [XmlArrayItem(Type = typeof(Image))]
        [XmlArrayItem(Type = typeof(Barcode))]
        [XmlArrayItem(Type = typeof(Row))]
        [XmlArrayItem(Type = typeof(Column))]
        public List<TemplateElementBase> PageHeader { get; set; }


        [XmlArray]
        [XmlArrayItem(Type = typeof(Field))]
        [XmlArrayItem(Type = typeof(Line))]
        [XmlArrayItem(Type = typeof(Label))]
        [XmlArrayItem(Type = typeof(Image))]
        [XmlArrayItem(Type = typeof(Barcode))]
        [XmlArrayItem(Type = typeof(Iterator))]
        [XmlArrayItem(Type = typeof(Row))]
        [XmlArrayItem(Type = typeof(Column))]
        public List<TemplateElementBase> ReportBody { get; set; }

        
        [XmlArray]
        [XmlArrayItem(Type = typeof(Field))]
        [XmlArrayItem(Type = typeof(Line))]
        [XmlArrayItem(Type = typeof(Label))]
        [XmlArrayItem(Type = typeof(PageNofM))]
        [XmlArrayItem(Type = typeof(Barcode))]
        [XmlArrayItem(Type = typeof(Row))]
        [XmlArrayItem(Type = typeof(Column))]
        public List<TemplateElementBase> PageFooter { get; set; }

        public IList<PrintableElement> GetPrintableElements(IEnumerable<PropertyData> data)
        {

            var context = new RenderContext(this.PageSettings);

            /*foreach (var item in this.PageHeader)
            {
                item.CalculateDimensions(this.PageSettings.Width, this.PageSettings.Height, this.PageSettings);
            }*/
            
            
            foreach (var item in this.ReportBody)
            {
                item.Initialize(this.PageSettings.Width, this.PageSettings.Height, context);
            }
            
            /*foreach (var item in this.PageFooter)
            {
                item.CalculateDimensions(this.PageSettings.Width, this.PageSettings.Height, this.PageSettings);
            }*/

            return context.PrintableElements;
        }
    }
}
