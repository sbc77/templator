using System.Collections.Generic;
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
        
        public string Group { get; set; }
        
        public IEnumerable<Gs1DataRow> Rows { get; set; }
    }
}