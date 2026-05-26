using System.ComponentModel.DataAnnotations;

namespace Tutorial8.Models;

public class Component
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    [MaxLength(50)]
    public string Type { get; set; } = null!;

    public decimal Price { get; set; }

    public ICollection<PcComponent> PcComponents { get; set; } = new List<PcComponent>();
}