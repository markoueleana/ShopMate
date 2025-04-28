using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildingBlocks.CQRS;
using Ordering.Application.Data;
using Ordering.Application.Dtos;
using Ordering.Domain.Entities;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.Commands.UpdateOrder;
public record UpdateOrderCommand(OrderDto Order) : ICommand<UpdateOrderResult>;

public record UpdateOrderResult(bool IsSuccess);

public class UpdateOrderHandler(IOrderDbContext dbContext) : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
   
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var orderId = command.Order.Id;
        var order = await dbContext.Orders.FindAsync([orderId], cancellationToken: cancellationToken);

        UpdateOrderWithNewValues(order, command.Order);
        dbContext.Orders.Update(order);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new UpdateOrderResult(true);
    }

    public void UpdateOrderWithNewValues(Order order, OrderDto orderDto)
    {
        var shippingAddress = Address.CreateAddress( orderDto.ShippingAddress.Country, orderDto.ShippingAddress.City, orderDto.ShippingAddress.ZipCode);
        var paymentAddress = Address.CreateAddress( orderDto.PaymentAddress.Country, orderDto.PaymentAddress.City, orderDto.PaymentAddress.ZipCode);
        var payment = Payment.CreatePayment(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration, orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod);

        order.Update(orderDto.OrderName, shippingAddress, paymentAddress, payment,orderDto.Status);
    }
}