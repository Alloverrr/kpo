using FitnessClubApp.Models;

namespace FitnessClubApp.Services;

public class FitnessClubManager
{
    public List<User> Users { get; set; } = [];
    public List<Product> Catalog { get; set; } = [];
    public List<Membership> Memberships { get; set; } = [];
    public List<TrainingSession> Sessions { get; set; } = [];
    public List<Booking> Bookings { get; set; } = [];
    public List<Order> Orders { get; set; } = [];
    public User? CurrentUser { get; private set; }

    public bool Login(string login, string password) => throw new NotImplementedException();
    public Order BuyProduct(Client client, Product product, int quantity) => throw new NotImplementedException();
    public Booking BookSession(Client client, TrainingSession session, Membership membership) => throw new NotImplementedException();
    public void AddSession(TrainingSession session) => throw new NotImplementedException();
    public void MarkAttendance(Booking booking, bool attended) => throw new NotImplementedException();
    public decimal GetTotalRevenue(DateTime from, DateTime to) => throw new NotImplementedException();

    public void SaveToJson(string filePath) => throw new NotImplementedException();
    public void LoadFromJson(string filePath) => throw new NotImplementedException();
}