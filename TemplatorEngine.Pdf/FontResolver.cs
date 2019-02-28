using System.Collections.Generic;
using System.IO;
using System.Linq;
using PdfSharpCore.Fonts;

namespace TemplatorEngine.Pdf
{
    public class FontResolver : IFontResolver
    {
        public FontResolver(IEnumerable<string> fontPaths)
        {
            this.FontFiles = new List<FontInfo>();

            foreach (var dir in fontPaths)
            {
                foreach (var file in Directory.GetFiles(dir, "*.ttf"))
                {
                    var ff = new FontInfo(file);

                    if (this.FontFiles.Any(x => x.Name == ff.Name && x.Bold == ff.Bold && ff.Italic == x.Italic))
                    {
                        continue;
                    }

                    FontFiles.Add(ff);
                }
            }

            this.FontFiles = this.FontFiles.OrderBy(x => x.FullPath).ToList();
        }

        public List<FontInfo> FontFiles { get; }

        public string DefaultFontName => "Arial";

        public byte[] GetFont(string fullPath)
        {
            return File.ReadAllBytes(fullPath);
        }

        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            var fontFile = this.FontFiles.Single(x => x.Name == familyName && x.Bold == isBold && x.Italic == isItalic);

            return new FontResolverInfo(fontFile.FullPath);
        }
    }
}
