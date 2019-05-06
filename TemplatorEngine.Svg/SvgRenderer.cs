using System;
using System.Collections.Generic;
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
        

        public byte[] Render(IEnumerable<Page> pages)
        {
            throw new NotImplementedException();
        }
    }
}
