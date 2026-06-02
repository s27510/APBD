using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Test2.Models;

[Table("Product") ]
public class Product
{
    [Key]
    public int ID { get; set; }
    
    [MaxLength(50)]
    public string Name { get; set; }
    
    [Column(TypeName = "decimal")]
    [Precision(10, 2)]
    public decimal Price { get; set; }
    
    public ICollection<ProductOrder> ProductOrders { get; set; } = null!;
}