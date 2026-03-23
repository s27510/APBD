using EquipmentRental.Models;
namespace EquipmentRental.Services;

public class EquipmentService
{
    private List<Equipment> equipmentList = new();

    public void Add(Equipment equipment)
    {
        equipmentList.Add(equipment);
    }

    public List<Equipment> GetAll() => equipmentList;

    public List<Equipment> GetAvailable()
    {
        return equipmentList.Where(e => e.IsAvailable).ToList();
    }

    public void MarkUnavailable(Guid id)
    {
        var eq = equipmentList.First(e => e.Id == id);
        eq.IsAvailable = false;
    }
}