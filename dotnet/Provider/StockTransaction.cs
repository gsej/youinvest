namespace Provider
{
    public class StockTransaction
    {
        public string Date { get; set; }
        public string Transaction { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Amount_Gbp { get; set; }
        public string Reference { get; set; }
    }
}