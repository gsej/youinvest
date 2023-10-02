namespace AjBell
{
    // TODO: turn into record
    public class AjBellStockTransaction
    {
        public string Account { get; set; }
        public string Date { get; set; }
        public string Transaction { get; set; } // this has a limited set of values
        public string Description { get; set; } // this includes the stock name. needs to be translated to a symbol
        public int Quantity { get; set; }
        public decimal Amount_Gbp { get; set; }
        public string Reference { get; set; }
    }
}