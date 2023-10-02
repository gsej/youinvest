using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace database.Entities;

public class StockTransaction
{
    public Guid StockTransactionId { get; set; }
    
    [MaxLength(20)]
    [Required]
    public string Account { get; set; }
    
    [MaxLength(10)]
    [Required]
    public string Date { get; set; }
    
    [MaxLength(20)]
    [Required]
    public string Transaction { get; set; }
    
    [MaxLength(200)]
    [Required]
    public string Description { get; set; }
    
    [Required]
    public int Quantity { get; set; }
    
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
    public string TransactionType { get; set; }
}