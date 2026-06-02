using Test2.Models;

namespace Test2.Data;
using Microsoft.EntityFrameworkCore;

public class DatabaseContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductOrder> ProductOrders { get; set; }
    public DbSet<Status> Statuses { get; set; }
    
    protected DatabaseContext()
    {
        
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>().HasData(new List<Client>()
        {
            new Client() { ID = 1, FirstName = "John", LastName = "Doe" },
            new Client() { ID = 2, FirstName = "Jane", LastName = "Doe" },
            new Client() { ID = 3, FirstName = "Julie", LastName = "Doe" },
        });
        
        modelBuilder.Entity<Status>().HasData(new List<Status>()
        {
            new Status() { ID = 1, Name = "Created" },
            new Status() { ID = 2, Name = "Ongoing" },
            new Status() { ID = 3, Name = "Completed" },
        });
        
        modelBuilder.Entity<Product>().HasData(new List<Product>()
        {
            new Product() { ID = 1, Name = "Apple", Price = 3.45m },
            new Product() { ID = 2, Name = "Bananas", Price = 5.55m },
            new Product() { ID = 3, Name = "Orange", Price = 12.37m },
        });
        
        modelBuilder.Entity<Order>().HasData(new List<Order>()
        {
            new Order() { ID = 1, CreatedAt = DateTime.Parse("2025-05-01"), FullfieldAt = DateTime.Parse("2025-05-02"), ClientID = 1, StatusID = 3},
            new Order() { ID = 2, CreatedAt = DateTime.Parse("2025-05-02"), FullfieldAt = null, ClientID = 1, StatusID = 2},
            new Order() { ID = 3, CreatedAt = DateTime.Parse("2025-05-03"), FullfieldAt = null, ClientID = 1, StatusID = 1},
            new Order() { ID = 4, CreatedAt = DateTime.Parse("2025-05-04"), FullfieldAt = null, ClientID = 2, StatusID = 1},
        });
        
        modelBuilder.Entity<ProductOrder>().HasData(new List<ProductOrder>()
        {
            new ProductOrder() { ProductID = 1, OrderID = 1, Amount = 3},
            new ProductOrder() { ProductID = 2, OrderID = 1, Amount = 5},
            new ProductOrder() { ProductID = 3, OrderID = 1, Amount = 8},
            new ProductOrder() { ProductID = 3, OrderID = 2, Amount = 1},
            new ProductOrder() { ProductID = 2, OrderID = 2, Amount = 2},
            new ProductOrder() { ProductID = 3, OrderID = 3, Amount = 8},
            new ProductOrder() { ProductID = 1, OrderID = 3, Amount = 12},
        });
    }
}