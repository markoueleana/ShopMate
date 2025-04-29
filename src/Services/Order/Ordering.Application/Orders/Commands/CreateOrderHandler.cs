
using System.Threading;
using BuildingBlocks.CQRS;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Dtos;
using Ordering.Domain.Entities;
using Ordering.Domain.ValueObjects;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Ordering.Application.Orders.Commands;

public record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;
public record CreateOrderResult(Guid Id);
public class CreateOrderHandler (IOrderDbContext dbContext): ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand commad, CancellationToken cancellationToken)
    {
        var order = CreateOrder(commad.Order);
        dbContext.Orders.Add(order);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new CreateOrderResult(order.Id);
    }

    private Order CreateOrder(OrderDto orderDto)
    {
        var shippingAddress = Address.CreateAddress(orderDto.ShippingAddress.Country, orderDto.ShippingAddress.ZipCode, orderDto.ShippingAddress.City);
        var paymentAddress = Address.CreateAddress(orderDto.PaymentAddress.Country, orderDto.PaymentAddress.ZipCode, orderDto.PaymentAddress.City);
        var payment = Payment.CreatePayment(orderDto.Payment.CardNumber, orderDto.Payment.CardNumber, orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod,orderDto.Payment.Expiration);

        var newOrder = Order.Create(Guid.NewGuid(), orderDto.CustomerId, orderDto.OrderName, shippingAddress, paymentAddress, payment);

        newOrder.CreatedAt = DateTime.Now;

        foreach (var orderItemDto in orderDto.OrderItems)
        {
            newOrder.Add(orderItemDto.ProductId, orderItemDto.Quantity, orderItemDto.Price);
        }
        return newOrder;
    }
}
