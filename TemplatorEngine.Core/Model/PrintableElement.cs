using System;
using System.Xml.Serialization;

namespace TemplatorEngine.Core.Model
{
    public class PrintableElement
    {
        public double X { get; set; }
        
        public double Y { get; set; }
        
        public double Width { get; set; }
        
        public double Height { get; set; }
        
        public object Value { get; set; }
        
        public ElementType ElementType { get; set; }
        
        public string StyleName { get; set; }
    }
}