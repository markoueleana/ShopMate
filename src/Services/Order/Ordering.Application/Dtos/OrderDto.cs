using Ordering.Domain.Entities;
using Ordering.Domain.Enums;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Dtos;

public record OrderDto
 (
    Guid Id,
    Guid CustomerId,
    string OrderName,
    Address ShippingAddress,
    Address PaymentAddress,
    Payment Payment,
    OrderStatus Status,
    List<OrderItem> OrderItems
);
