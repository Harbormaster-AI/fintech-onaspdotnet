
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class FXDealEndpoints
{
    public static IEndpointRouteBuilder MapFXDealEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/fXDeal").WithTags("FXDeals");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignQuote", AssignQuote);
        group.MapPut("/unassignQuote", UnassignQuote);

    group.MapPut("/addToPaymentOrders", AddToPaymentOrders);
    group.MapPut("/removeFromPaymentOrders", RemoveFromPaymentOrders);


        return app;
    }

    private static async Task<IResult> Create(
        FXDealRequest request,
        IFXDealService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToFXDeal( request );

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
        FXDealRequest request,
        IFXDealService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToFXDeal( request );

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
        IFXDealService service,
        CancellationToken cancellationToken) {

        var fXDeal = await service.Get(identifier, cancellationToken);
        return fXDeal is null ? Results.NotFound() : Results.Ok( fXDeal );
    }


    private static async Task<IResult> GetAll(
        IFXDealService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( FXDealResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IFXDealService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignQuote(
        AssociationRequest request,
        IFXDealService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignQuote(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignQuote(
    AssociationRequest request,
    IFXDealService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignQuote(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToPaymentOrders(
        MultipleAssociationRequest request,
        IFXDealService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPaymentOrders(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPaymentOrders(
        MultipleAssociationRequest request,
        IFXDealService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPaymentOrders(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static FXDeal mapRequestToFXDeal( FXDealRequest request ) {
        var model = new FXDeal
        {
            Id = request.Id,
            DealReference = request.DealReference,
            BaseCurrency = request.BaseCurrency,
            QuoteCurrency = request.QuoteCurrency,
            Rate = request.Rate,
            Amount = request.Amount,
            SettlementDate = request.SettlementDate,
            Status = request.Status,
        };
        return model;
    }

}
