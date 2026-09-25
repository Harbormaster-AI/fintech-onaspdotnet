
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class TradeOrderEndpoints
{
    public static IEndpointRouteBuilder MapTradeOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/tradeOrder").WithTags("TradeOrders");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignPortfolio", AssignPortfolio);
        group.MapPut("/unassignPortfolio", UnassignPortfolio);
        group.MapPut("/assignSecurity", AssignSecurity);
        group.MapPut("/unassignSecurity", UnassignSecurity);

    group.MapPut("/addToTrades", AddToTrades);
    group.MapPut("/removeFromTrades", RemoveFromTrades);


        return app;
    }

    private static async Task<IResult> Create(
        TradeOrderRequest request,
        ITradeOrderService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToTradeOrder( request );

        try
        {
            await service.Create(model, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }

        return Results.NoContent();
    }

    private static async Task<IResult> Update(
        TradeOrderRequest request,
        ITradeOrderService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToTradeOrder( request );

        try
        {
            var updated = await service.Update(model, cancellationToken);
            return updated ? Results.NoContent() : Results.NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    }


    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        ITradeOrderService service,
        CancellationToken cancellationToken) {

        var tradeOrder = await service.Get(identifier, cancellationToken);
        return tradeOrder is null ? Results.NotFound() : Results.Ok( tradeOrder );
    }


    private static async Task<IResult> GetAll(
        ITradeOrderService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( TradeOrderResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ITradeOrderService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPortfolio(
        AssociationRequest request,
        ITradeOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPortfolio(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPortfolio(
    AssociationRequest request,
    ITradeOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPortfolio(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSecurity(
        AssociationRequest request,
        ITradeOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignSecurity(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignSecurity(
    AssociationRequest request,
    ITradeOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignSecurity(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToTrades(
        MultipleAssociationRequest request,
        ITradeOrderService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToTrades(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromTrades(
        MultipleAssociationRequest request,
        ITradeOrderService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromTrades(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static TradeOrder mapRequestToTradeOrder( TradeOrderRequest request ) {
        var model = new TradeOrder
        {
            Id = request.Id,
            OrderId = request.OrderId,
            Quantity = request.Quantity,
            LimitPrice = request.LimitPrice,
            PlacedAt = request.PlacedAt,
            Side = request.Side,
            Type = request.Type,
            Status = request.Status,
            TimeInForce = request.TimeInForce,
        };
        return model;
    }

}
