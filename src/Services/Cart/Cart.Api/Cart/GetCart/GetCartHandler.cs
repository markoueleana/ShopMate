using BuildingBlocks.CQRS;
using Cart.API.Entities;
using Marten;

namespace Cart.API.Cart.GetCart;

public record GetCartQuery(string UserName)  : IQuery<GetCartResult>;
public record GetCartResult(ShoppingCart Cart);
public class GetCartQueryHandler(IDocumentSession session)
    : IQueryHandler<GetCartQuery, GetCartResult>
{
    public async Task<GetCartResult> Handle(GetCartQuery query, CancellationToken cancellationToken)
    {
        var cart = await session.LoadAsync<ShoppingCart>(query.UserName, cancellationToken);
        return new GetCartResult(cart);
    }
}
