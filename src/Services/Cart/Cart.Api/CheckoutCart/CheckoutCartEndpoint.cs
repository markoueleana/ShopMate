using Cart.API.Dtos;
using Carter;
using Mapster;
using MediatR;

namespace Cart.API.CheckoutCart;

public record CheckoutCartRequest(CartCheckoutDto CartCheckoutDto);

public record CheckoutCartReponse(bool IsSuccess);
public class CheckoutCartEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/cart/checkout", async (CheckoutCartRequest request, ISender sender) =>
        {
            var command = request.Adapt<CheckoutCartCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CheckoutCartReponse>();

            return Results.Ok(response);
        });
    }
}
