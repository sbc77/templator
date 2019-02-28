using System;
using TemplatorEngine.Core.Abstract;
using TemplatorEngine.Core.Model;

namespace TemplatorEngine.Svg
{
    public class SvgRenderer : ITemplateRenderer
    {
        private readonly SvgConfig cfg;

        public SvgRenderer(SvgConfig cfg)
        {
            this.cfg = cfg;
        }

        public void Render(PrintTemplate template, object data)
        {
            throw new NotImplementedException();
        }
    }
}
