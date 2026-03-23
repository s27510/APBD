using EquipmentRental.Models;
using EquipmentRental.Models.Users;
using EquipmentRental.Services;

var equipmentService = new EquipmentService();
var userService = new UserService();
var rentalService = new RentalService();
var mainService = new MainService();

// Add equipment
var laptop = new Laptop("Dell XPS", 16, "i7", "15.3");
var laptop2 = new Laptop("Acer Predator", 8, "i5", "17.3");
var projector = new Projector("Epson LT3500", 3000, true);
var projector2 = new Projector("Epson XS1000", 1000, false);
var camera = new Camera("Canon XP2000", 24, "black");

equipmentService.Add(laptop);
equipmentService.Add(laptop2);
equipmentService.Add(projector);
equipmentService.Add(projector2);
equipmentService.Add(camera);

// Add users
var student = new Student("ALex", "Johns", "test1@gmail.com");
var employee = new Employee("Bob", "Employee", "test@gmail.com");

userService.Add(student);
userService.Add(employee);

// Valid rental
var rental1 = rentalService.Rent(student, laptop, 3);

// Invalid operation (unavailable laptop)
try
{
    equipmentService.MarkUnavailable(laptop2.Id);
    rentalService.Rent(employee, laptop2, 3);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

// Return on time
rentalService.Return(rental1);

// Late return
var rental2 = rentalService.Rent(employee, laptop, -2);
rentalService.Return(rental2);
if (Convert.ToBoolean(rental2.Penalty))
{
    Console.WriteLine($"Penalty: {rental2.Penalty}$ for {rental2.Equipment.Name} rent for {rental2.User.FirstName} {rental2.User.LastName}");
}

rentalService.Rent(employee, laptop, -2);
rentalService.Rent(employee, projector, 6);
rentalService.Rent(student, camera, 3);


//Report
mainService.displayReport(equipmentService, rentalService);