namespace TemplatorEngine.Core.Model
{
    public class PageDimension
    {
        public PageDimension(string name, double width, double height)
        {
            this.Name = name;
            this.Width = width;
            this.Height = height;
        }

        public string Name { get; }

        public double Width { get; }

        public double Height { get; }
    }
}
