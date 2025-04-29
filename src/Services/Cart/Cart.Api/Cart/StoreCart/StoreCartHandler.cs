using System.Windows.Input;
using BuildingBlocks.CQRS;
using Cart.API.Entities;
using Discount.Grpc;
using Marten;
using NetTopologySuite.Index.HPRtree;

namespace Cart.API.Cart.StoreCart;

public record StoreCartCommad (ShoppingCart Cart) : ICommand<StoreCartResult>;
public record StoreCartResult(string UserName);

public class StoreCartCommandHandler(IDocumentSession session, DiscountProtoService.DiscountProtoServiceClient discountProto) : ICommandHandler<StoreCartCommad, StoreCartResult>
{
    public async Task<StoreCartResult> Handle(StoreCartCommad command, CancellationToken cancellationToken)
    {
        ShoppingCart cart = command.Cart;
        foreach(var item in cart.Items)
        {
            var requestProto = new GetDiscountRequest { ProductName = item.ProductName };
            var discount = discountProto.GetDiscount(requestProto);
            if (discount is not null)
            {
                item.Price = (decimal )item.Price - (decimal)discount.Amount;
            }
        }
        session.Store(cart);

        await session.SaveChangesAsync(cancellationToken);
        return new StoreCartResult(cart.UserName);
    }
}
