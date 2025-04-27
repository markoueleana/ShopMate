using Ordering.Domain.DomainAbstraction;
using Ordering.Domain.Enums;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Entities;

public class Order : Aggregate<Guid>
{
    private readonly List<OrderItem> orderItems = new();
    public IReadOnlyList<OrderItem> Items=>orderItems.AsReadOnly();
    public Guid CustomerId { get; private set; } = default!;
    public string OrderName { get; private set; } = default!;
    public Address ShippingAddress  { get; private set; } = default!;
    public Address PaymentAddress { get; private set; } = default!;
    public Payment Payment { get; private set; } = default!;
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;

    public decimal TotalAmount { get; private set; } = default!;

    public static Order Create(Guid orderId, Guid customerId, string orderName, Address shippingAddress, Address paymentAddress,Payment payment)
    {
        return new Order
        {
            Id = orderId,
            CustomerId = customerId,
            OrderName = orderName,
            ShippingAddress = shippingAddress,
            PaymentAddress = paymentAddress,
            Status = OrderStatus.Pending,
        };

    }
    public void Update(string orderName, Address shippingAddress, Address paymentAddress, Payment payment, OrderStatus orderStatus)
    { 
        OrderName = orderName;
        ShippingAddress = shippingAddress;
        PaymentAddress = paymentAddress;
        Status = orderStatus;
    }
    public void Add(Guid productId, int quantity, decimal price)
    {

        var orderItem = new OrderItem(Id, productId, quantity, price);
        orderItems.Add(orderItem);
    }

    public void Remove(Guid productId)
    {
        var orderItem = orderItems.FirstOrDefault(x => x.ProductId == productId);
        if (orderItem is not null)
        {
            orderItems.Remove(orderItem);
        }
    }


}
