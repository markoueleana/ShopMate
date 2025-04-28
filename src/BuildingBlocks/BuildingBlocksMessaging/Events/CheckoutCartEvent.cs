
namespace BuildingBlocksMessaging.Events;

public record CheckoutCartEvent
{
    public string UserName { get;  set; } = default!;
    public Guid CustomerId { get;  set; } = default!;
    public string Country { get; set; } = default!;
    public string ZipCode { get; set; } = default!;
    public string City { get; set; } = default!;
    public string CardName { get; set; } = default!;
    public string CardNumber { get; set; } = default!;
    public string CVV { get; set; } = default!;
    public string PaymentMethod { get; set; } = default!;
    public string Expiration { get; set; } = default!;
    public decimal TotalPrice { get; set; } = default!;
}
