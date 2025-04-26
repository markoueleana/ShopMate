using System.Windows.Input;
using BuildingBlocks.CQRS;
using Cart.API.Entities;
using Marten;

namespace Cart.API.Cart.DeleteCart;

public record DeleteCartCommand(string UserName) : ICommand<DeleteCartResult>;
public record DeleteCartResult(bool IsSuccess);

public class DeleteCartCommandHandler (IDocumentSession session) : ICommandHandler<DeleteCartCommand, DeleteCartResult>
{
    public async Task<DeleteCartResult> Handle(DeleteCartCommand commad, CancellationToken cancellationToken)
    {
        session.Delete<ShoppingCart>(commad.UserName);

        await session.SaveChangesAsync(cancellationToken);
        return new DeleteCartResult(true);
    }
}
