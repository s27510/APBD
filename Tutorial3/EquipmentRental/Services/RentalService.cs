using EquipmentRental.Models;
using EquipmentRental.Models.Users;

namespace EquipmentRental.Services;

public class RentalService
{
    private List<Rental> rentals = new();

    private const decimal DailyPenalty = 10;

    public Rental Rent(User user, Equipment equipment, int days)
    {
        if (!equipment.IsAvailable)
            throw new Exception("Equipment " + equipment.Name + " is currently unavailable");

        int activeRentals = rentals.Count(r =>
            r.User.Id == user.Id && !r.IsReturned);

        if (activeRentals >= user.MaxRentals)
            throw new Exception("User " + user.FirstName + " exceeded rental limit");

        var rental = new Rental
        {
            User = user,
            Equipment = equipment,
            RentDate = DateTime.Now,
            DueDate = DateTime.Now.AddDays(days)
        };

        equipment.IsAvailable = false;
        rentals.Add(rental);

        return rental;
    }

    public void Return(Rental rental)
    {
        rental.ReturnDate = DateTime.Now;

        if (rental.ReturnDate > rental.DueDate)
        {
            int lateDays = (rental.ReturnDate.Value - rental.DueDate).Days;
            rental.Penalty = lateDays * DailyPenalty;
        }

        rental.Equipment.IsAvailable = true;
    }

    public List<Rental> GetActiveRentals()
    {
        return rentals
            .Where(r => !r.IsReturned)
            .ToList();
    }

    public List<Rental> GetOverdueRentals()
    {
        return rentals.Where(r => r.IsOverdue).ToList();
    }
}