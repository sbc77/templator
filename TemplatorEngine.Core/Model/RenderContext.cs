using System.Collections.Generic;

namespace TemplatorEngine.Core.Model
{
    public class RenderContext
    {
        public RenderContext(PageSettings ps)
        {
            this.PageSettings = ps;
            this.PrintableElements = new List<PrintableElement>();
            this.CurrentX = ps.Margin ?? 5;
            this.CurrentY = ps.Margin ?? 5;
        }
        public PageSettings PageSettings { get; }
        
        public List<PrintableElement> PrintableElements { get; }
        
        public double FontSizeHeightRatio => .4;

        public double MaxPageWidth => this.PageSettings.Width - this.PageSettings.Margin.Value * 2;
        
        public double MaxPageHeight => this.PageSettings.Height - this.PageSettings.Margin.Value * 2;
        
        public double CurrentX { get; set; }
        
        public double CurrentY { get; set; }

        public void AddElement(PrintableElement pe)
        {
            this.PrintableElements.Add(pe);
        }
    }
}