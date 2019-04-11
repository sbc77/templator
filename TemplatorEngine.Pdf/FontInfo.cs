using System;
using System.IO;

namespace TemplatorEngine.Pdf
{
    public class FontInfo
    {
        public FontInfo(string path)
        {
            this.FullPath = path;
            this.Name = Path.GetFileNameWithoutExtension(path);

            this.ProcessBold();
            this.ProcessItalic();

            this.Name = this.Name.Trim();
        }

        public string Name { get; set; }

        public bool Bold { get; set; }

        public bool Italic { get; set; }

        public string FullPath { get; set; }

        public override string ToString()
        {
            return $"{this.Name} {(this.Bold ? "B" : string.Empty)}{(this.Italic ? "I" : string.Empty)}".Trim();
        }

        private void ProcessBold()
        {
            var boldKeys = new string[] { "bold", "800", "700", "600" };

            foreach (var item in boldKeys)
            {
                if (this.Name.ToLower().Contains(item))
                {
                    this.Bold = true;
                    this.Name = this.Name.Replace(item, string.Empty, StringComparison.InvariantCultureIgnoreCase);

                    return;
                }
            }
        }

        private void ProcessItalic()
        {
            if (this.Name.ToLower().Contains("italic"))
            {
                this.Italic = true;
                this.Name = this.Name.Replace("italic", string.Empty, StringComparison.InvariantCultureIgnoreCase);
            }
        }
    }
}
