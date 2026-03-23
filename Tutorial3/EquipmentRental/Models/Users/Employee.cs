namespace EquipmentRental.Models.Users;

public class Employee : User
{
    public Employee(string firstName, string lastName, string email) 
        : base(firstName, lastName, email, 5) { }
}