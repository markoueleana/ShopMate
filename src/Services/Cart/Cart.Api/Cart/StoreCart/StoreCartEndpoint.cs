using System.Windows.Input;
using BuildingBlocks.CQRS;
using Cart.API.Cart.GetCart;
using Cart.API.Entities;
using Carter;
using Mapster;
using MediatR;

namespace Cart.API.Cart.StoreCart;

public record StoreCartRequest (ShoppingCart Cart) : ICommand<StoreCartResponse>;
public record StoreCartResponse(string UserName);

public class StoreCartEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/cart", async (StoreCartRequest req, ISender sender) =>
        {
            var command = req.Adapt<StoreCartCommad>();
            var result = await sender.Send(command);

            var response = result.Adapt<StoreCartResponse>();

            return Results.Created($"/cart/{response.UserName}", response);
        })
        .WithName("StoreCart")
        .Produces<StoreCartResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Store Cart")
        .WithDescription("Store Cart");
    }
}
