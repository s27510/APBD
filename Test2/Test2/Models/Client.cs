using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test2.Models;

[Table("Client") ]
public class Client
{
    [Key]
    public int ID { get; set; }
    
    [MaxLength(50)]
    public string FirstName { get; set; }
    
    [MaxLength(50)]
    public string LastName { get; set; }
    
    public ICollection<Order> Orders { get; set; } = null!;
}