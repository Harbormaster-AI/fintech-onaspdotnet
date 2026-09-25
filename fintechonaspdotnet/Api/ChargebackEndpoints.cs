
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class ChargebackEndpoints
{
    public static IEndpointRouteBuilder MapChargebackEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/chargeback").WithTags("Chargebacks");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignDispute", AssignDispute);
        group.MapPut("/unassignDispute", UnassignDispute);
        group.MapPut("/assignTransaction", AssignTransaction);
        group.MapPut("/unassignTransaction", UnassignTransaction);


        return app;
    }

    private static async Task<IResult> Create(
        ChargebackRequest request,
        IChargebackService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToChargeback( request );

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
        ChargebackRequest request,
        IChargebackService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToChargeback( request );

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
        IChargebackService service,
        CancellationToken cancellationToken) {

        var chargeback = await service.Get(identifier, cancellationToken);
        return chargeback is null ? Results.NotFound() : Results.Ok( chargeback );
    }


    private static async Task<IResult> GetAll(
        IChargebackService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ChargebackResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IChargebackService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDispute(
        AssociationRequest request,
        IChargebackService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignDispute(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignDispute(
    AssociationRequest request,
    IChargebackService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignDispute(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTransaction(
        AssociationRequest request,
        IChargebackService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignTransaction(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignTransaction(
    AssociationRequest request,
    IChargebackService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignTransaction(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static Chargeback mapRequestToChargeback( ChargebackRequest request ) {
        var model = new Chargeback
        {
            Id = request.Id,
            ChargebackReference = request.ChargebackReference,
            Amount = request.Amount,
            PostedAt = request.PostedAt,
            Stage = request.Stage,
            Status = request.Status,
        };
        return model;
    }

}
