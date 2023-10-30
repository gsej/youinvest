using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace database.Entities;

[Table("Account")]
public class Account
{
    public Account(string accountCode)
    {
        AccountCode = accountCode;
    }

    [MaxLength(20)]
    [Required]
    [Key]
    public string AccountCode { get; set; }
}