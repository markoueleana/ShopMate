using System.Windows.Input;
using BuildingBlocks.CQRS;
using Cart.API.Dtos;
using Cart.API.Entities;
using Marten;
using BuildingBlocksMessaging.Events;
using Mapster;
using MassTransit;
namespace Cart.API.CheckoutCart;

public record CheckoutCartCommand(CartCheckoutDto CheckoutCartDto) : ICommand<CheckoutCartResult>;

public record CheckoutCartResult(bool isSuccess) ;

public class CheckoutCartHandler (IDocumentSession session,IPublishEndpoint publishEndpoint) : ICommandHandler<CheckoutCartCommand, CheckoutCartResult>
{
    public async Task<CheckoutCartResult> Handle(CheckoutCartCommand command, CancellationToken cancellationToken)
    {
        var cart = await session.LoadAsync<ShoppingCart>(command.CheckoutCartDto.UserName, cancellationToken);

        var eventMessage = command.CheckoutCartDto.Adapt<CheckoutCartEvent>();
        await publishEndpoint.Publish(eventMessage, cancellationToken);
        return new CheckoutCartResult(true);
    }
}
