using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order.Domain.DomainAbstraction;
using Order.Domain.Enums;
using Order.Domain.ValueObjects;

namespace Order.Domain.Entities;

public class Order : Aggregate<Guid>
{
    private readonly List<OrderItem> orderItems = new();
    public IReadOnlyList<OrderItem> Items=>orderItems.AsReadOnly();
    public Guid CustomerId {  get; set; }
    public string OrderName { get; set; }
    public Address ShippingAddress  { get; set; }
    public Address PaymentAddress { get; set; }
    public Payment Payment { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public decimal TotalAmount { get; set; }
}
