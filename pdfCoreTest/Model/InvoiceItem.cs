namespace pdfCoreTest.Model
{
    public class InvoiceItem
    {
        public int PosNo { get; set; }
        
        public string ArticleId { get; set; }
        
        public string ArticleDescription { get; set; }
        
        public string UoM { get; set; }
        
        public string TaxId { get; set; }
        
        public decimal Quantity { get; set; }
        
        public decimal NetAmount { get; set; }
        
        public decimal TaxAmount { get; set; }
        
        public decimal Amount { get; set; }
    }
}