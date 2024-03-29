using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace database.Entities;

[Table("StockAlias")]
public class StockAlias
{
    public StockAlias(string description) 
    {
        Description = description;
    }

    [MaxLength(50)]
    [Required]
    [Key]
    public string Description { get; set; }
    
    [Required]
    public Stock? Stock { get; set; }
}