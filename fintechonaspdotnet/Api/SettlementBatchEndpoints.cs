
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class SettlementBatchEndpoints
{
    public static IEndpointRouteBuilder MapSettlementBatchEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/settlementBatch").WithTags("SettlementBatchs");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignProcessor", AssignProcessor);
        group.MapPut("/unassignProcessor", UnassignProcessor);
        group.MapPut("/assignMerchant", AssignMerchant);
        group.MapPut("/unassignMerchant", UnassignMerchant);

    group.MapPut("/addToPayouts", AddToPayouts);
    group.MapPut("/removeFromPayouts", RemoveFromPayouts);

    group.MapPut("/addToTransactions", AddToTransactions);
    group.MapPut("/removeFromTransactions", RemoveFromTransactions);


        return app;
    }

    private static async Task<IResult> Create(
        SettlementBatchRequest request,
        ISettlementBatchService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToSettlementBatch( request );

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
        SettlementBatchRequest request,
        ISettlementBatchService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToSettlementBatch( request );

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
        ISettlementBatchService service,
        CancellationToken cancellationToken) {

        var settlementBatch = await service.Get(identifier, cancellationToken);
        return settlementBatch is null ? Results.NotFound() : Results.Ok( settlementBatch );
    }


    private static async Task<IResult> GetAll(
        ISettlementBatchService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( SettlementBatchResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ISettlementBatchService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignProcessor(
        AssociationRequest request,
        ISettlementBatchService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignProcessor(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignProcessor(
    AssociationRequest request,
    ISettlementBatchService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignProcessor(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignMerchant(
        AssociationRequest request,
        ISettlementBatchService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignMerchant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignMerchant(
    AssociationRequest request,
    ISettlementBatchService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignMerchant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToPayouts(
        MultipleAssociationRequest request,
        ISettlementBatchService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPayouts(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPayouts(
        MultipleAssociationRequest request,
        ISettlementBatchService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPayouts(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToTransactions(
        MultipleAssociationRequest request,
        ISettlementBatchService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToTransactions(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromTransactions(
        MultipleAssociationRequest request,
        ISettlementBatchService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromTransactions(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static SettlementBatch mapRequestToSettlementBatch( SettlementBatchRequest request ) {
        var model = new SettlementBatch
        {
            Id = request.Id,
            BatchId = request.BatchId,
            PeriodStart = request.PeriodStart,
            PeriodEnd = request.PeriodEnd,
            TotalVolume = request.TotalVolume,
            TotalCount = request.TotalCount,
            Status = request.Status,
        };
        return model;
    }

}
