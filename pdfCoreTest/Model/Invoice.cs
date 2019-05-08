using System;
using System.Collections.Generic;

namespace pdfCoreTest.Model
{
    public class Invoice
    {
        public string No { get; set; }
        public DateTime Created { get; set; }
        public string CustomerName { get; set; }
        
        public IEnumerable<InvoiceItem> Items { get; set; }
        
        public decimal TotalAmount { get; set; } 
        public decimal TotalNetAmount { get; set; }
        public string SupplierName { get; set; }
        public string Barcode { get; set; }
    }
}