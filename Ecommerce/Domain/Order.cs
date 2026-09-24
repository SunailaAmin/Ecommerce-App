namespace Ecommerce.Domain;

public class Order
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public decimal TotalAmount { get; set; }

    public string Status { get; set; } = "Pending";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Property
    public User? User { get; set; }

    // Navigation Property
    public ICollection<OrderItem> OrderItems { get; set; }
        = new List<OrderItem>();
}