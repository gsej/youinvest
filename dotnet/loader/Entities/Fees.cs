using System.ComponentModel.DataAnnotations;

namespace loader.Entities;

public class Fees
{
    [MaxLength(20)]
    [Required]
    public string Account { get; set; }
    
    [Required]
    public int Year { get; set; }
    
  //  [Precision(19, 5)]
    public decimal Total { get; set; }
}