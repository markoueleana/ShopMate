
namespace BuildingBlocksMessaging.Events;

public record CheckoutCartEvent 
{
    public Guid CustomerId { get; private set; } = default!;
    public string OrderName { get; private set; } = default!;
    public string Country { get; set; } = default!;
    public string ZipCode { get; set; } = default!;
    public string City { get; set; } = default!;
    public string CardName { get; set; } = default!;
    public string CardNumber { get; set; } = default!;
    public string CVV { get; set; } = default!;
    public string PaymentMethod { get; set; } = default!;
    public string Expiration { get; set; } = default!;
}
