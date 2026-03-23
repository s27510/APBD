using EquipmentRental.Models.Users;

namespace EquipmentRental.Services;

public class UserService
{
    private List<User> users = new();

    public void Add(User user) => users.Add(user);
}