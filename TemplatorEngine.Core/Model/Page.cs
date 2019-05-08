using System.Collections.Generic;
using TemplatorEngine.Core.Abstract;

namespace TemplatorEngine.Core.Model
{
    public class Page
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public double MinY { get; set; }
        public double MaxY { get; set; }
        
        public List<PrintableElement> Elements { get; set; }
    }
}