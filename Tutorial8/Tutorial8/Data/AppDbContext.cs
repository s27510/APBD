using Microsoft.EntityFrameworkCore;
using Tutorial8.Models;

namespace Tutorial8.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Pc> Pcs => Set<Pc>();
    public DbSet<Component> Components => Set<Component>();
    public DbSet<PcComponent> PcComponents => Set<PcComponent>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PcComponent>()
            .HasKey(pc => new { pc.PcId, pc.ComponentId });

        modelBuilder.Entity<PcComponent>()
            .HasOne(pc => pc.Pc)
            .WithMany(p => p.PcComponents)
            .HasForeignKey(pc => pc.PcId);

        modelBuilder.Entity<PcComponent>()
            .HasOne(pc => pc.Component)
            .WithMany(c => c.PcComponents)
            .HasForeignKey(pc => pc.ComponentId);

        modelBuilder.Entity<Component>()
            .Property(c => c.Price)
            .HasColumnType("decimal(10,2)");

        modelBuilder.Entity<Pc>().HasData(
            new Pc
            {
                Id = 1,
                Name = "Gaming Beast X",
                Weight = 12.5,
                Warranty = 36,
                CreatedAt = new DateTime(2026, 5, 8, 9, 0, 0),
                Stock = 5
            },
            new Pc
            {
                Id = 2,
                Name = "Office Mini Pro",
                Weight = 4.2,
                Warranty = 24,
                CreatedAt = new DateTime(2026, 4, 15, 13, 30, 0),
                Stock = 12
            },
            new Pc
            {
                Id = 3,
                Name = "Workstation Ultra",
                Weight = 9.8,
                Warranty = 48,
                CreatedAt = new DateTime(2026, 3, 10, 11, 0, 0),
                Stock = 3
            }
        );

        modelBuilder.Entity<Component>().HasData(
            new Component
            {
                Id = 1,
                Name = "RTX 5090",
                Type = "GPU",
                Price = 9999.99m
            },
            new Component
            {
                Id = 2,
                Name = "Ryzen 9 9950X",
                Type = "CPU",
                Price = 3499.99m
            },
            new Component
            {
                Id = 3,
                Name = "32GB DDR5",
                Type = "RAM",
                Price = 699.99m
            }
        );

        modelBuilder.Entity<PcComponent>().HasData(
            new PcComponent
            {
                PcId = 1,
                ComponentId = 1,
                Quantity = 1
            },
            new PcComponent
            {
                PcId = 1,
                ComponentId = 2,
                Quantity = 1
            },
            new PcComponent
            {
                PcId = 1,
                ComponentId = 3,
                Quantity = 2
            }
        );
    }
}
