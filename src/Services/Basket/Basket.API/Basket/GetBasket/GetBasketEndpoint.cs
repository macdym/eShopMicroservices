﻿namespace Basket.API.Basket.GetBasket
{
    public record GetBasketRequest(string UserName);

    public record GetBasketResponse(ShoppingCart ShoppingCart);

    public class GetBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(
                "/basket/{userName}",
                async (string userName, ISender sender) =>
                {
                    //var query = request.Adapt<GetBasketQuery>();
                    var wtf = new GetBasketQuery(userName);
                    var result = await sender.Send(wtf);

                    var response = result.Adapt<GetBasketResponse>();

                    return Results.Ok(response);
                })
                .WithName("GetBasket")
                .Produces<GetBasketResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithDescription("Get Basket")
                .WithSummary("Get Basket");
        }
    }
}
