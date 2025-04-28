using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildingBlocks.CQRS;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Dtos;
using Ordering.Domain.Entities;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.Queries;
public record GetOrdersByNameQuery(string Name): IQuery<GetOrdersByNameResult>;

public record GetOrdersByNameResult(IEnumerable<OrderDto> Orders);

public class GetOrdersByNameHandler(IOrderDbContext dbContext): IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
{
    public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
                .Include(o => o.OrderItems)
                .Where(o => o.OrderName.Contains(query.Name))
                .OrderBy(o => o.OrderName)
                .ToListAsync(cancellationToken);

        var dtos =  orders.Select(orderDto => new OrderDto(
            orderDto.Id, orderDto.CustomerId, orderDto.OrderName,
           new AddressDto(orderDto.ShippingAddress.Country, orderDto.ShippingAddress.City, orderDto.ShippingAddress.ZipCode),
           new AddressDto(orderDto.PaymentAddress.Country, orderDto.PaymentAddress.City, orderDto.PaymentAddress.ZipCode),
           new PaymentDto(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration, orderDto.Payment.CVV, orderDto.Payment.PaymentMethod),
           orderDto.Status,
           orderDto.OrderItems.Select(oi => new OrderItemDto(oi.OrderId, oi.ProductId, oi.Quantity, oi.Price)).ToList()
        ));

        return new GetOrdersByNameResult(dtos);
    }
}