using System.ComponentModel.DataAnnotations;

namespace TemplatorEngineWebTest.Model
{

    public class Gs1DataRow
    {
      

        [Display(Name = "SSCC")]
        public string SsccNo { get; set; }

        [Display(Name = "Article ID")]
        public string ArticleId { get; set; }

        [Display(Name = "Article EAN")]
        public string ArticleEan { get; set; }

        [Display(Name = "Description")]
        public string ArticleDescription { get; set; }

        [Display(Name = "Quantity")]
        public decimal Quantity { get; set; }

        public string Barcode { get; set; }
    }
}