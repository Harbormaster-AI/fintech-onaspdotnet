
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class PayoutEndpoints
{
    public static IEndpointRouteBuilder MapPayoutEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/payout").WithTags("Payouts");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignMerchant", AssignMerchant);
        group.MapPut("/unassignMerchant", UnassignMerchant);
        group.MapPut("/assignSettlementBatch", AssignSettlementBatch);
        group.MapPut("/unassignSettlementBatch", UnassignSettlementBatch);
        group.MapPut("/assignDestinationAccount", AssignDestinationAccount);
        group.MapPut("/unassignDestinationAccount", UnassignDestinationAccount);


        return app;
    }

    private static async Task<IResult> Create(
        PayoutRequest request,
        IPayoutService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPayout( request );

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
        PayoutRequest request,
        IPayoutService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPayout( request );

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
        IPayoutService service,
        CancellationToken cancellationToken) {

        var payout = await service.Get(identifier, cancellationToken);
        return payout is null ? Results.NotFound() : Results.Ok( payout );
    }


    private static async Task<IResult> GetAll(
        IPayoutService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( PayoutResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IPayoutService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignMerchant(
        AssociationRequest request,
        IPayoutService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignMerchant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignMerchant(
    AssociationRequest request,
    IPayoutService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignMerchant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSettlementBatch(
        AssociationRequest request,
        IPayoutService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignSettlementBatch(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignSettlementBatch(
    AssociationRequest request,
    IPayoutService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignSettlementBatch(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDestinationAccount(
        AssociationRequest request,
        IPayoutService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignDestinationAccount(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignDestinationAccount(
    AssociationRequest request,
    IPayoutService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignDestinationAccount(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static Payout mapRequestToPayout( PayoutRequest request ) {
        var model = new Payout
        {
            Id = request.Id,
            PayoutReference = request.PayoutReference,
            Amount = request.Amount,
            Currency = request.Currency,
            ScheduledDate = request.ScheduledDate,
            PaidDate = request.PaidDate,
            Status = request.Status,
        };
        return model;
    }

}
