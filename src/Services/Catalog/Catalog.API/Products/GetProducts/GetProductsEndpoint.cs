﻿using BuildingBlocks.CQRS;
using Carter;
using Catalog.API.Entities;
using Mapster;
using MediatR;

namespace Catalog.API.Products.GetProducts;

public record GetProductsRequest() : IQuery<GetProductsResponse>;
public record GetProductsResponse(IEnumerable<Product> Products);
public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
        {
            var result = await sender.Send(new GetProductsQuery());
            var response = result.Adapt<GetProductsResponse>();

            return Results.Ok(response);
        })
       .WithName("GetProducts")
         .Produces<GetProductsResponse>(StatusCodes.Status200OK)
         .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
