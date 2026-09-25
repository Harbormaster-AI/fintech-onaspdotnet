
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class TradeEndpoints
{
    public static IEndpointRouteBuilder MapTradeEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/trade").WithTags("Trades");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignOrder", AssignOrder);
        group.MapPut("/unassignOrder", UnassignOrder);
        group.MapPut("/assignSecurity", AssignSecurity);
        group.MapPut("/unassignSecurity", UnassignSecurity);
        group.MapPut("/assignInvestmentAccount", AssignInvestmentAccount);
        group.MapPut("/unassignInvestmentAccount", UnassignInvestmentAccount);


        return app;
    }

    private static async Task<IResult> Create(
        TradeRequest request,
        ITradeService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToTrade( request );

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
        TradeRequest request,
        ITradeService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToTrade( request );

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
        ITradeService service,
        CancellationToken cancellationToken) {

        var trade = await service.Get(identifier, cancellationToken);
        return trade is null ? Results.NotFound() : Results.Ok( trade );
    }


    private static async Task<IResult> GetAll(
        ITradeService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( TradeResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ITradeService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignOrder(
        AssociationRequest request,
        ITradeService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignOrder(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignOrder(
    AssociationRequest request,
    ITradeService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignOrder(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSecurity(
        AssociationRequest request,
        ITradeService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignSecurity(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignSecurity(
    AssociationRequest request,
    ITradeService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignSecurity(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignInvestmentAccount(
        AssociationRequest request,
        ITradeService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignInvestmentAccount(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignInvestmentAccount(
    AssociationRequest request,
    ITradeService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignInvestmentAccount(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static Trade mapRequestToTrade( TradeRequest request ) {
        var model = new Trade
        {
            Id = request.Id,
            ExecutedAt = request.ExecutedAt,
            Quantity = request.Quantity,
            Price = request.Price,
            Fees = request.Fees,
            SettlementDate = request.SettlementDate,
        };
        return model;
    }

}
