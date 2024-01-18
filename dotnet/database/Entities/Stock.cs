using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace database.Entities;

[Table("Stock")]
public class Stock
{
    // TODO: use builder instead of these constructors....
    public Stock(string stockSymbol, string description, string stockType, IEnumerable<StockAlias> aliases, string defaultCurrency)
    {
        StockSymbol = stockSymbol;
        Description = description;
        StockType = stockType;
        DefaultCurrency = defaultCurrency;
        Aliases = aliases;
    }

    public Stock(string stockSymbol, string description, string stockType, string defaultCurrency, string? notes = null)
    {
        StockSymbol = stockSymbol;
        Description = description;
        StockType = stockType;
        DefaultCurrency = defaultCurrency;
        Notes = notes;
    }

    public Stock(
        string stockSymbol, 
        string description, 
        string stockType,
        string defaultCurrency,
        string? notes,
        IEnumerable<StockAlias> aliases, 
        IEnumerable<AlternativeSymbol> alternativeSymbols)
    {
        StockSymbol = stockSymbol;
        Description = description;
        Notes = notes;
        StockType = stockType;
        DefaultCurrency = defaultCurrency;
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

    [MaxLength(10)]
    // This is the currency assumed for prices
    // when the stock price file being imported doesn't have a currency specified.
    public string? DefaultCurrency { get; }

    [Required]
    public bool SubjectToStampDuty { get; set; }
    
    [MaxLength(500)]
    public string? Notes { get; set; }
    
    
    // Create a builder for the class Stock
    
    public class StockBuilder 
    {
        private string _stockSymbol;
        private string _isin;
        private string _description;
        private string _stockType;
        private string _defaultCurrency;
        private string? _notes;
     
        private IEnumerable<StockAlias> _aliases = new List<StockAlias>();
        private IEnumerable<AlternativeSymbol> _alternativeSymbols = new List<AlternativeSymbol>();
      
        public StockBuilder(string stockSymbol, string description, string stockType)
        {
            _stockSymbol = stockSymbol;
            _description = description;
            _stockType = stockType;
        }
        
        public StockBuilder WithIsin(string isin)
        {
            _isin = isin;
            return this;
        }
        
        public StockBuilder WithStockType(string stockType)
        {
            _stockType = stockType;
            return this;
        }
        
        public StockBuilder WithDefaultCurrency(string defaultCurrency)
        {
            _defaultCurrency = defaultCurrency;
            return this;
        }
        
        public StockBuilder WithNotes(string notes)
        {
            _notes = notes;
            return this;
        }
        
        public StockBuilder WithAliases(IEnumerable<StockAlias> aliases)
        {
            _aliases = aliases;
            return this;
        }
        
        public StockBuilder WithAlternativeSymbols(IEnumerable<AlternativeSymbol> alternativeSymbols)
        {
            _alternativeSymbols = alternativeSymbols;
            return this;
        }
        
        public Stock Build()
        {
            return new Stock(
                _stockSymbol,
                _description,
                _stockType,
                _defaultCurrency,
                _notes,
                _aliases,
                _alternativeSymbols) { Isin = _isin };
        }
    }
}
