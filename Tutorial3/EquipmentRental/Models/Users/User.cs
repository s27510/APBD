namespace EquipmentRental.Models.Users;

public abstract class User
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    
    public int MaxRentals { get; set; }

    protected User(string firstName, string lastName, string email, int maxRentals)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        MaxRentals = maxRentals;
    }
    
}