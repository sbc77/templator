using System.Collections.Generic;

namespace TemplatorEngine.Core.Model
{
    public class RenderContext
    {
        public RenderContext(PageSettings ps)
        {
            this.PageSettings = ps;
            this.PrintableElements = new List<PrintableElement>
            {
                new PrintableElement
                {
                    ElementType = ElementType.NewPage, Height = 0,
                    Width = this.MaxPageWidth
                }
            };

            ps.Margin = ps.Margin ?? 5;
            
            this.CurrentX = ps.Margin.Value;
            this.CurrentY = ps.Margin.Value;
        }
        public PageSettings PageSettings { get; }
        
        public List<PrintableElement> PrintableElements { get; }
        
        public double FontSizeHeightRatio => .45;

        public double MaxPageWidth => this.PageSettings.Width - this.PageSettings.Margin.Value * 2;
        
        public double MaxPageHeight => this.PageSettings.Height - this.PageSettings.Margin.Value * 2;
        
        public double CurrentX { get; set; }
        
        public double CurrentY { get; set; }
        public bool NewPageCreated { get; set; }

        public void AddElement(PrintableElement pe)
        {
            
            if (pe.Y + pe.Height > this.MaxPageHeight || pe.ElementType==ElementType.NewPage)
            {
                this.CurrentY = this.PageSettings.Margin.Value;

                pe.Y = this.CurrentY;
                
                this.PrintableElements.Add(new PrintableElement
                {
                    Height = 0,
                    Width = this.MaxPageWidth,
                    ElementType = ElementType.NewPage
                });

                this.NewPageCreated = true;
            }

            if (pe.ElementType == ElementType.NewPage)
            {
                return;
            }
            
            this.PrintableElements.Add(pe);
            
        }
    }
}