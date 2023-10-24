using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace database.Entities;

[Table("AlternativeSymbol")]
public class AlternativeSymbol
{
    [MaxLength(15)]
    [Required]
    [Key]
    public string Alternative { get; set; }
    
    [Required]
    public Stock Stock { get; set; }
}

[Table("StockAlias")]
public class StockAlias
{
    [MaxLength(50)]
    [Required]
    [Key]
    public string Description { get; set; }
    
    [Required]
    public Stock Stock { get; set; }
}