using TemplatorEngine.Core.Model;

namespace TemplatorEngine.Core.Abstract
{
    public interface ITemplateRenderer
    {
        void Render(PrintTemplate template, object data);
    }
}
