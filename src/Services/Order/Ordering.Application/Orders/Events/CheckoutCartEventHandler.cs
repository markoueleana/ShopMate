using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildingBlocksMessaging.Events;
using MassTransit;
using MediatR;
using Ordering.Application.Dtos;
using Ordering.Application.Orders.Commands;
using Ordering.Domain.Enums;

namespace Ordering.Application.Orders.Events;

public class CheckoutCartEventHandler(ISender sender) : IConsumer<CheckoutCartEvent>
{
    public async Task Consume(ConsumeContext<CheckoutCartEvent> context)
    {
        var createOrderCommand = MapToCreateOrderCommand(context.Message);
        var order = await sender.Send(createOrderCommand);
    }
    private CreateOrderCommand MapToCreateOrderCommand(CheckoutCartEvent message)
    {
        var addressDto = new AddressDto( message.Country, message.City, message.ZipCode);
        var paymentDto = new PaymentDto(message.CardName, message.CardNumber, message.Expiration, message.CVV, message.PaymentMethod);
        var orderId = Guid.NewGuid();

        var orderDto = new OrderDto(
            Id: orderId,
            CustomerId: message.CustomerId,
            OrderName: message.UserName,
            ShippingAddress: addressDto,
            PaymentAddress: addressDto,
            Payment: paymentDto,
            Status: OrderStatus.Pending,
            OrderItems:
            [
                new OrderItemDto(orderId, new Guid("f2a1e4fc-1b75-4b29-912f-3ef567b421a1"), 2, 29.99M),
                new OrderItemDto(orderId, new Guid("c3e4520a-21aa-4e3a-b7cc-6f3654f6e320"), 1, 89.99M)
            ]);

        return new CreateOrderCommand(orderDto);
    }
}
