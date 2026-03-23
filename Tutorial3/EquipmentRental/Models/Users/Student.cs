namespace EquipmentRental.Models.Users;

public class Student : User
{
    public Student(string firstName, string lastName, string email) 
        : base(firstName, lastName, email, 2) { }
}