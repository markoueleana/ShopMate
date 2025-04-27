using Ordering.Domain.Entities;
using Ordering.Domain.Enums;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Dtos;

public record OrderDto
 (
    Guid Id,
    Guid CustomerId,
    string OrderName,
    AddressDto ShippingAddress,
    AddressDto PaymentAddress,
    PaymentDto Payment,
    OrderStatus Status,
    List<OrderItemDto> OrderItems
);
