using System.Windows.Input;
using BuildingBlocks.CQRS;
using Cart.API.Entities;
using Marten;

namespace Cart.API.Cart.StoreCart;

public record StoreCartCommad (ShoppingCart Cart) : ICommand<StoreCartResult>;
public record StoreCartResult(string UserName);

public class StoreCartCommandHandler(IDocumentSession session) : ICommandHandler<StoreCartCommad, StoreCartResult>
{
    public async Task<StoreCartResult> Handle(StoreCartCommad command, CancellationToken cancellationToken)
    {
        ShoppingCart cart = command.Cart;
        session.Store(cart);

        await session.SaveChangesAsync(cancellationToken);
        return new StoreCartResult(cart.UserName);
    }
}
