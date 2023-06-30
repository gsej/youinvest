using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace consumer.Entities;

public class CashStatementItem
{
    public Guid CashStatementItemId { get; set; }
    
    [MaxLength(20)]
    [Required]
    public string Account { get; set; }
    
    [MaxLength(10)]
    [Required]
    public string Date { get; set; }
    
    [MaxLength(200)]
    [Required]
    public string Description { get; set; }
    
    [Precision(19, 5)]
    public decimal ReceiptAmountGbp { get; set; }
    
    [Precision(19, 5)]
    public decimal PaymentAmountGbp { get; set; }
    
    [MaxLength(100)]
    public string CashStatementItemType { get; set; }
}