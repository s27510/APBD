namespace EquipmentRental.Models;

public class Camera : Equipment
{
    public int Megapixels { get; set; }
    public string Color { get; set; }

    public Camera(string name, int megapixels, string color)
        : base(name)
    {
        Megapixels = megapixels;
        Color = color;
    }
}