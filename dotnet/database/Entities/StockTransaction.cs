using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace database.Entities;

[Table(nameof(StockTransaction))]
public class StockTransaction
{
    [Key]
    public Guid StockTransactionId { get; set; }
    
    [MaxLength(20)]
    [Required]
    [ForeignKey(nameof(Account))]
    public string AccountCode { get; set; }
    
    public Account Account { get; set; }
    
    [MaxLength(15)]
    //[Required] // TODO: make required? 
    [ForeignKey(nameof(Stock))]
    public string? StockSymbol { get; set; }
    
    public Stock? Stock { get; set; }
    
    [MaxLength(10)]
    [Required]
    public string Date { get; set; }
    
    [MaxLength(200)]
    [Required]
    public string Transaction { get; set; }
    
    [MaxLength(200)]
    [Required]
    public string Description { get; set; }
    
    //public string AlternateDescriptions { get; set; }
    
    
    
    [Precision(19,5)]
    [Required]
    public decimal Quantity { get; set; }
    
    [Precision(19,5)]
    [Required]
    public decimal AmountGbp { get; set; }
    
    [MaxLength(20)]
    [Required]
    public string Reference { get; set; }
    
    [Precision(19,5)]
    [Required]
    public decimal Fee { get; set; }
    
    [Precision(19,5)]
    [Required]
    public decimal StampDuty { get; set; }
    
    [MaxLength(100)]
    [Required]
    public string TransactionType { get; set; }
}