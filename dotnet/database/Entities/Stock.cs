using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace database.Entities;

[Table("Stock")]
public class Stock
{
    [MaxLength(15)]
    [Required]
    [Key]
    public string StockSymbol { get; set; }

    [MaxLength(50)]
    [Required]
    public string Description { get; set; }
    
    [MaxLength(15)]
    // [Required] // TODO: make required 
    public string? StockType { get; set; }
    
    [Required]
    public bool SubjectToStampDuty { get; set; }
    
    [MaxLength(500)]
    public string? Notes { get; set; }
}