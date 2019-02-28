using TemplatorEngine.Core;

namespace TemplatorEngine.Svg
{
    public static class SvgExtensions
    {
        public static Templator UseSvgRenderer(this Templator templator, SvgConfig cfg)
        {
            templator.SetRenderer(new SvgRenderer(cfg));
            return templator;
        }
    }
}
