using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace database.Entities;

[Table("Stock")]
public class Stock
{
    public Stock(string stockSymbol, string description, string stockType, IEnumerable<StockAlias> aliases)
    {
        StockSymbol = stockSymbol;
        Description = description;
        Aliases = aliases;
        StockType = stockType;
    }

    public Stock(string stockSymbol, string description, string stockType, string? notes = null)
    {
        StockSymbol = stockSymbol;
        Description = description;
        StockType = stockType;
        Notes = notes;
    }

    public Stock(
        string stockSymbol, 
        string description, 
        string stockType, 
        string? notes,
        IEnumerable<StockAlias> aliases, 
        IEnumerable<AlternativeSymbol> alternativeSymbols)
    {
        StockSymbol = stockSymbol;
        Description = description;
        Notes = notes;
        StockType = stockType;
        Aliases = aliases;
        AlternativeSymbols = alternativeSymbols;
    }

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