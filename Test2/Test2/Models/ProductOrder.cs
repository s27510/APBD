using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Test2.Models;

[Table("Product_Order")]
[PrimaryKey(nameof(ProductID), nameof(OrderID))]
public class ProductOrder
{
    [ForeignKey(nameof(Product))]
    public int ProductID { get; set; }
    
    [ForeignKey(nameof(Order))]
    public int OrderID { get; set; }
    
    public int Amount { get; set; }
    
    public Product Product { get; set; } = null!;
    public Order Order { get; set; } = null!;
}