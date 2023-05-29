namespace common;

public class CashStatementItem
{
    public string Account { get; set; }
    public string Date { get; set; }
    public string Description { get; set; }
    public decimal Receipt_Amount_Gbp { get; set; }
    public decimal Payment_Amount_Gbp { get; set; }

    public int Year => Int32.Parse(Date.Substring(0, 4));

    public bool IsDividend
    {
        get
        {
            return Description.StartsWith("Div");
        }
    }
    
    public bool IsContribution
    {
        get
        {
            return Description.Equals("Contribution", StringComparison.InvariantCultureIgnoreCase) 
                   || Description.Contains("Withdraw", StringComparison.InvariantCultureIgnoreCase)
                   || Description.Contains("Subscription", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}