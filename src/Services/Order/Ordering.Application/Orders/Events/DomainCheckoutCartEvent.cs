using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Ordering.Application.Dtos;
using Ordering.Domain.Abstractions;
using Ordering.Domain.DomainAbstraction;

namespace Ordering.Application.Orders.Events;

public class DomainCheckoutCartEvent
(IPublishEndpoint publishEndpoint)
: INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
            var order = new OrderDto(
                    Id: domainEvent.order.Id,
                    CustomerId: domainEvent.order.CustomerId,
                    OrderName: domainEvent.order.OrderName,
                    ShippingAddress: new AddressDto(domainEvent.order.ShippingAddress.Country, domainEvent.order.ShippingAddress.City, domainEvent.order.ShippingAddress.ZipCode),
                    PaymentAddress: new AddressDto(domainEvent.order.ShippingAddress.Country, domainEvent.order.ShippingAddress.City, domainEvent.order.ShippingAddress.ZipCode),
                    Payment: new PaymentDto(domainEvent.order.Payment.CardName, domainEvent.order.Payment.CardNumber, domainEvent.order.Payment.Expiration, domainEvent.order.Payment.CVV, domainEvent.order.Payment.PaymentMethod),
                    Status: domainEvent.order.Status,
                    OrderItems: domainEvent.order.OrderItems.Select(oi => new OrderItemDto(oi.OrderId, oi.ProductId, oi.Quantity, oi.Price)).ToList()
                );
            await publishEndpoint.Publish(order, cancellationToken);
        
    }
}