namespace EquipmentRental.Models;

public class Projector : Equipment
{
    public int Lumens { get; set; }
    public bool Is4k { get; set; }

    public Projector(string name, int lumens, bool is4K)
        : base(name)
    {
        Lumens = lumens;
        Is4k = is4K;
    }
}