using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test2.Models;

[Table("Order") ]
public class Order
{
    [Key]
    public int ID { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime? FullfieldAt { get; set; }
    
    [ForeignKey(nameof(Client))]
    public int ClientID { get; set; }
    
    [ForeignKey(nameof(Status))]
    public int StatusID { get; set; }

    public Client Client { get; set; } = null!;
    public Status Status { get; set; } = null!;
    
    public ICollection<ProductOrder> ProductOrders { get; set; } = null!;
}