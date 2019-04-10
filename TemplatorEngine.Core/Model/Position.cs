namespace TemplatorEngine.Core.Model
{
    public class Position
    {
        public Position(double x, double y, double maxWidth=0, double maxHeight=0)
        {
            this.X = x;
            this.Y = y;
            this.MaxWidth = maxWidth;
            this.MaxHeight = maxHeight;
        }
        
        public Position(Position pos, double maxWidth, double maxHeight)
        {
            this.X = pos.X;
            this.Y = pos.Y;
            this.MaxWidth = maxWidth;
            this.MaxHeight = maxHeight;
        }

        public readonly double X;

        public readonly double Y;

        public readonly double MaxWidth;

        public readonly double MaxHeight;
    }
}