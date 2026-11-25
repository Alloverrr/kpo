namespace FitnessClubApp.Models;

public abstract class User
{
    public Guid Id { get; set; }
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public UserRole Role { get; protected set; }

    public bool CheckPassword(string password) => throw new NotImplementedException();
}

public class Client : User
{
    public List<Membership> Memberships { get; set; } = [];
    public List<Booking> Bookings { get; set; } = [];
    public List<Order> Orders { get; set; } = [];
}

public class Trainer : User
{
    public string Specialization { get; set; } = string.Empty;
    public List<TrainingSession> Sessions { get; set; } = [];
}

public class Admin : User
{
}