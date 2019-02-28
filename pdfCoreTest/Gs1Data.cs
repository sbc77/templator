using System.ComponentModel.DataAnnotations;

namespace pdfCoreTest
{

    public class Gs1Data
    {
        [Display(Name = "Shipment ID")]
        public int ShipmentId { get; set; }

        [Display(Name = "Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Street")]
        public string CustomerStreet { get; set; }

        [Display(Name = "")]
        public string CustomerStreet2 { get; set; }

        [Display(Name = "ZIP")]
        public string CustomerZip { get; set; }

        [Display(Name = "City")]
        public string CustomerCity { get; set; }

        [Display(Name = "Cust. order No")]
        public string CustomerReference { get; set; }

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

        public string Barcode1 { get; set; }

        public string Barcode2 { get; set; }
    }
}