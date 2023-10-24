using System.ComponentModel.DataAnnotations;

namespace loader.Entities;

public class Dividends
{
    [MaxLength(20)]
    [Required]
    public string Account { get; set; }
    
    [Required]
    public int Year { get; set; }
    
  //  [Precision(19, 5)]
    public decimal TotalReceived { get; set; }
}