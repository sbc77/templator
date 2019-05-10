using System.Collections.Generic;

namespace TemplatorEngine.Core.Model
{
    public class PrintableElement
    {
        public PrintableElement()
        {
            this.Properties = new Dictionary<PrintableElementProperty, object> {{PrintableElementProperty.Value, null}};
        }
        public double X { get; set; }
        
        public double Y { get; set; }
        
        public double Width { get; set; }
        
        public double Height { get; set; }

        public object Value
        {
            get => this.Properties[PrintableElementProperty.Value];
            set => this.Properties[PrintableElementProperty.Value] = value;
        }
        
        public Dictionary<PrintableElementProperty,object> Properties { get; }
        
        public ElementType ElementType { get; set; }

        public void AddProperty(PrintableElementProperty property, object value)
        {
            this.Properties.Add(property,value);
        }

        public bool HasProperty(PrintableElementProperty property)
        {
            return this.Properties.ContainsKey(property);
        }
    }
}