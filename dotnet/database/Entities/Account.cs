using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace database.Entities;

[Table("Account")]
public class Account
{
    [MaxLength(20)]
    [Required]
    [Key]
    public string AccountCode { get; set; }
}