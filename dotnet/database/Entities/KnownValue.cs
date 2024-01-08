using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace database.Entities;

[Table("KnownValue")]
public class KnownValue
{
    public KnownValue(string accountCode, string date, decimal totalValue)
    {
        AccountCode = accountCode;;
        Date = date;
        TotalValue = totalValue;
    }

    [Key]
    public Guid KnownValueId { get; set; }
    
    [MaxLength(20)]
    [Required]
    [ForeignKey(nameof(Account))]
    public string AccountCode { get; private set; }
    
    public Account? Account { get; private set; }
   
    [MaxLength(10)]
    [Required]
    public string Date { get; set; }
    
    [Precision(19,5)]
    [Required]
    public decimal TotalValue { get; set; }
}