using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using PdfSharpCore.Pdf;
using TemplatorEngine.Core.Abstract;
using TemplatorEngine.Core.Element;
using TemplatorEngine.Core.Model;

namespace TemplatorEngine.Pdf.Element
{
    public class PdfIterator : PdfElementRendererBase<Iterator>
    {
        protected override void OnRender(Iterator element, IEnumerable<PropertyData> d, PdfRenderContext context)
        {
            var data = d.ToList();
            
            var rows = ((IEnumerable<IEnumerable<PropertyData>>)data.Single(x => x.Name == element.DataField).Value).ToList();
            
            if (!rows.Any())
            {
                return;
            }

            foreach (var row in rows)
            {
                var flatten = new List<PropertyData>();

                foreach (var prop in data)
                {
                    if (prop.Name == element.DataField)
                    {
                        continue;
                    }
                    
                    flatten.Add(prop);
                }
                
                if (element.UseNewPage)
                {
                    context.SetNewPage();
                }

                foreach (var rowItem in row)
                {
                    var ri = new PropertyData
                    {
                        Name = element.ItemReferenceName+"."+rowItem.Name,
                        Label = rowItem.Label,
                        Value = rowItem.Value
                    };
                    
                    flatten.Add(ri);
                }

                foreach (var item in element.Items)
                {
                    context.RenderElement(item, flatten);
                }
            }
        }
    }
}