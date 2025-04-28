using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildingBlocks.CQRS;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Domain.Entities;

namespace Ordering.Application.Orders.Commands;

public record DeleteOrderCommand(Guid OrderId)  : ICommand<DeleteOrderResult>;
public record DeleteOrderResult(bool IsSuccess);

public class DeleteOrderHandler(IOrderDbContext dbContext) : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
    {
        public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            var orderId = command.OrderId;
            var order = await dbContext.Orders.FindAsync([orderId], cancellationToken: cancellationToken);
            dbContext.Orders.Remove(order);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new DeleteOrderResult(true);
        }
    }

