namespace TemplatorEngine.Pdf
{
    public class PdfConfig
    {
        public static PdfConfig Create()
        {
            return new PdfConfig();
        }

        public string[] FontPaths { get; set; }

        // public string OutFile { get; set; }

    }
}
