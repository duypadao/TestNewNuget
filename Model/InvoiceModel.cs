namespace PDFCreator.Model
{
    public class InvoiceModel
    {
        public int InvoiceNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }

        public Address SellerAddress { get; set; } = new Address();
        public Address CustomerAddress { get; set; } = new Address();

        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public string Comments { get; set; } = string.Empty;
    }
}
