namespace EquipmentRental.Models;

public class Laptop : Equipment
{
    public int RamGb { get; set; }
    public string Cpu { get; set; }
    public string ScreenDiagonal { get; set; }

    public Laptop(string name, int ramGb, string cpu, string screenDiagonal)
        : base(name)
    {
        RamGb = ramGb;
        Cpu = cpu;
        ScreenDiagonal = screenDiagonal;
    }
}