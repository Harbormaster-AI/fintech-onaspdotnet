
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class DisputeEndpoints
{
    public static IEndpointRouteBuilder MapDisputeEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/dispute").WithTags("Disputes");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignTransaction", AssignTransaction);
        group.MapPut("/unassignTransaction", UnassignTransaction);
        group.MapPut("/assignCard", AssignCard);
        group.MapPut("/unassignCard", UnassignCard);
        group.MapPut("/assignMerchant", AssignMerchant);
        group.MapPut("/unassignMerchant", UnassignMerchant);

    group.MapPut("/addToChargebacks", AddToChargebacks);
    group.MapPut("/removeFromChargebacks", RemoveFromChargebacks);


        return app;
    }

    private static async Task<IResult> Create(
        DisputeRequest request,
        IDisputeService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDispute( request );

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
        DisputeRequest request,
        IDisputeService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDispute( request );

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
        IDisputeService service,
        CancellationToken cancellationToken) {

        var dispute = await service.Get(identifier, cancellationToken);
        return dispute is null ? Results.NotFound() : Results.Ok( dispute );
    }


    private static async Task<IResult> GetAll(
        IDisputeService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( DisputeResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IDisputeService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTransaction(
        AssociationRequest request,
        IDisputeService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignTransaction(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignTransaction(
    AssociationRequest request,
    IDisputeService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignTransaction(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCard(
        AssociationRequest request,
        IDisputeService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCard(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCard(
    AssociationRequest request,
    IDisputeService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCard(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignMerchant(
        AssociationRequest request,
        IDisputeService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignMerchant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignMerchant(
    AssociationRequest request,
    IDisputeService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignMerchant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToChargebacks(
        MultipleAssociationRequest request,
        IDisputeService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToChargebacks(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromChargebacks(
        MultipleAssociationRequest request,
        IDisputeService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromChargebacks(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Dispute mapRequestToDispute( DisputeRequest request ) {
        var model = new Dispute
        {
            Id = request.Id,
            DisputeReference = request.DisputeReference,
            OpenedAt = request.OpenedAt,
            ClosedAt = request.ClosedAt,
            Reason = request.Reason,
            Status = request.Status,
        };
        return model;
    }

}
