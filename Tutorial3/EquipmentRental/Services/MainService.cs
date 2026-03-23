namespace EquipmentRental.Services;

public class MainService
{
    public void displayReport(EquipmentService equipmentService, RentalService rentalService)
    {
        Console.WriteLine("---------------------------");
        Console.WriteLine("Equipment List:");
        foreach (var equipment in equipmentService.GetAll())
        {
            Console.WriteLine(" - " + equipment.Name + ": " + (equipment.IsAvailable ? "Available" : "Unavailable"));
        }

        Console.WriteLine("\n Available equipment:");
        foreach (var equipment in equipmentService.GetAvailable())
        {
            Console.WriteLine(" - " + equipment.Name);
        }

        Console.WriteLine("\n Overdue rentals:");
        foreach (var rental in rentalService.GetOverdueRentals())
        {
            Console.WriteLine(" - " + rental.Equipment.Name + ": " + rental.User.FirstName + " " + rental.User.LastName);
        }
        Console.WriteLine("\n Active rentals:");
        foreach (var rental in rentalService.GetActiveRentals())
        {
            Console.WriteLine(" - " + rental.Equipment.Name + ": " + rental.User.FirstName + " " + rental.User.LastName);
        }
        Console.WriteLine("---------------------------");
    }
}