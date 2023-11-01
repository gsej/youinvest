using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace database.Entities;

[Table("StockPrice")]
public class StockPrice
{
    public StockPrice(string stockSymbol, string date, decimal price, string currency)
    {
        StockSymbol = stockSymbol;
        Date = date;
        Price = price;
        Currency = currency;
    }

    [Key]
    public Guid StockPriceId { get; set; }
    
    [MaxLength(15)]
    //[ForeignKey(nameof(Stock))]
    public string StockSymbol { get; set; }
    
    //public Stock Stock { get; set; }
    
    [MaxLength(10)]
    [Required]
    public string Date { get; set; }
    
    [Precision(19,5)]
    [Required]
    public decimal Price { get; set; }
    
    [Required]
    public string Currency { get; set; }
}