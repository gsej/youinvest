namespace Incoming;

public class AjBellCashStatementItem
{
    public string Account { get; set; }
    public string Date { get; set; }
    public string Description { get; set; }
    public decimal Receipt_Amount_Gbp { get; set; }
    public decimal Payment_Amount_Gbp { get; set; }
   
    //TODo: what's this for?
    public int Year => Int32.Parse(Date.Substring(0, 4));
}