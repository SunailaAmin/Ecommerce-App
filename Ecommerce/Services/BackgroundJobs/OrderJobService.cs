namespace Ecommerce.Services.BackgroundJobs;

public class OrderJobService
{
    public void SendOrderConfirmation(int orderId)
    {
        Console.WriteLine(
            $"[HANGFIRE] Order Confirmation Sent for Order {orderId}");
    }

    public void ProcessPayment(int orderId)
    {
        Console.WriteLine(
            $"[HANGFIRE] Payment Processed for Order {orderId}");
    }
}