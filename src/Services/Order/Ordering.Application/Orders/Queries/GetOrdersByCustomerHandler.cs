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

namespace Ordering.Application.Orders.Queries;
public record GetOrdersByCustomerQuery(Guid CustomerId): IQuery<GetOrdersByCustomerResult>;

public record GetOrdersByCustomerResult(IEnumerable<OrderDto> Orders);

public class GetOrdersByCustomerHandler(IOrderDbContext dbContext) : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
{
    public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery query, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders.Include(o => o.OrderItems)
                        .Where(o => o.CustomerId == query.CustomerId)
                        .OrderBy(o => o.OrderName)
                        .ToListAsync(cancellationToken);

        var dtos = orders.Select(orderDto => new OrderDto(
            orderDto.Id,
            orderDto.CustomerId,
            orderDto.OrderName,
            new AddressDto(orderDto.ShippingAddress.Country, orderDto.ShippingAddress.City, orderDto.ShippingAddress.ZipCode),
            new AddressDto(orderDto.PaymentAddress.Country, orderDto.PaymentAddress.City, orderDto.PaymentAddress.ZipCode),
            new PaymentDto(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration, orderDto.Payment.CVV, orderDto.Payment.PaymentMethod),
            orderDto.Status,
            orderDto.OrderItems.Select(oi => new OrderItemDto(oi.OrderId, oi.ProductId, oi.Quantity, oi.Price)).ToList()
     ));
        return new GetOrdersByCustomerResult(dtos);
    }
}