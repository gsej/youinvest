using System.ComponentModel.DataAnnotations;

namespace monolith.Entities;

public class Stock
{
    public Guid StockId { get; set; }
    
    [MaxLength(15)]
    [Required]
    public string Symbol { get; set; }

    [MaxLength(50)]
    [Required]
    public string Description { get; set; }
    
    [Required]
    public bool SubjectToStampDuty { get; set; }
}