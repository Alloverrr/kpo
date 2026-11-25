namespace FitnessClubApp.Models;

public class Membership
{
    public Guid Id { get; set; }
    public Client? Client { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int RemainingSessions { get; set; }
    public MembershipStatus Status { get; set; }

    public bool IsActive() => throw new NotImplementedException();
    public void Freeze(int days) => throw new NotImplementedException();
    public void Extend(int days, int sessions) => throw new NotImplementedException();
    public bool UseSession() => throw new NotImplementedException();
}

public class TrainingSession
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Direction { get; set; } = string.Empty;
    public string HallName { get; set; } = string.Empty;
    public bool IsPersonal { get; set; }
    public Trainer? Trainer { get; set; }
    public DateTime StartTime { get; set; }
    public int DurationMinutes { get; set; }
    public int MaxParticipants { get; set; }
    public List<Booking> Bookings { get; set; } = [];

    public bool HasFreeSpots() => throw new NotImplementedException();
}

public class Booking
{
    public Guid Id { get; set; }
    public Client? Client { get; set; }
    public TrainingSession? Session { get; set; }
    public Membership? Membership { get; set; }
    public DateTime BookedAt { get; set; }
    public BookingStatus Status { get; set; }

    public void Cancel() => throw new NotImplementedException();
    public void MarkAttendance(bool attended) => throw new NotImplementedException();
}

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ProductType Type { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public int ValidityDays { get; set; }
    public int SessionsCount { get; set; }
}

public class Order
{
    public Guid Id { get; set; }
    public Client? Client { get; set; }
    public Product? Product { get; set; }
    public int Quantity { get; set; }
    public decimal PriceAtPurchase { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsPaid { get; set; }

    public void Pay() => throw new NotImplementedException();
}