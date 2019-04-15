using System;
using System.Collections.Generic;
using TemplatorEngine.Core.Abstract;

namespace TemplatorEngine.Svg
{
    public class SvgRenderer : ITemplateRenderer
    {
        private readonly SvgConfig cfg;

        public SvgRenderer(SvgConfig cfg)
        {
            this.cfg = cfg;
        }

        public byte[] Render(IEnumerable<PropertyData> data)
        {
            throw new NotImplementedException();
        }
    }
}
