using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace database.Entities;

[Table("StockAlias")]
public class StockAlias
{
    // [MaxLength(15)]
    // [Required]
    // [ForeignKey()]
    // public string StockSymbol { get; set; }
    
    [MaxLength(50)]
    [Required]
    [Key]
    public string Description { get; set; }
    
    [Required]
    public Stock Stock { get; set; }
}