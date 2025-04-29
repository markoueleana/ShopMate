namespace Discount.Grpc.Entities;

public class Coupon
{
    public int Id { get; set; } = default!;
    public string ProductName { get; set; } = default!;
    public decimal Amount { get; set; }
}
