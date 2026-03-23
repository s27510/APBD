using EquipmentRental.Models.Users;
namespace EquipmentRental.Models;

public class Rental
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public User User { get; set; }
    public Equipment Equipment { get; set; }

    public DateTime RentDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }

    public decimal Penalty { get; set; }

    public bool IsReturned => ReturnDate.HasValue;

    public bool IsOverdue =>
        !IsReturned && DateTime.Now > DueDate;
}