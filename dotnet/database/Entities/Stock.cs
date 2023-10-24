using System;
using System.Collections.Generic;
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
    
    [MaxLength(15)] // todo: check length
    public string? Isin { get; set; }

    [MaxLength(50)]
    [Required]
    public string Description { get; set; }

    public IEnumerable<AlternativeSymbol> AlternativeSymbols { get; set; } = new List<AlternativeSymbol>();// encapsulate this
    
    public IEnumerable<StockAlias> Aliases { get; set; } = new List<StockAlias>();// encapsulate this
    
    [MaxLength(15)]
    // [Required] // TODO: make required 
    public string? StockType { get; set; }
    
    [Required]
    public bool SubjectToStampDuty { get; set; }
    
    [MaxLength(500)]
    public string? Notes { get; set; }
}