
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class DirectDebitMandateEndpoints
{
    public static IEndpointRouteBuilder MapDirectDebitMandateEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/directDebitMandate").WithTags("DirectDebitMandates");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignAccount", AssignAccount);
        group.MapPut("/unassignAccount", UnassignAccount);
        group.MapPut("/assignCreditor", AssignCreditor);
        group.MapPut("/unassignCreditor", UnassignCreditor);


        return app;
    }

    private static async Task<IResult> Create(
        DirectDebitMandateRequest request,
        IDirectDebitMandateService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDirectDebitMandate( request );

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
        DirectDebitMandateRequest request,
        IDirectDebitMandateService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToDirectDebitMandate( request );

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
        IDirectDebitMandateService service,
        CancellationToken cancellationToken) {

        var directDebitMandate = await service.Get(identifier, cancellationToken);
        return directDebitMandate is null ? Results.NotFound() : Results.Ok( directDebitMandate );
    }


    private static async Task<IResult> GetAll(
        IDirectDebitMandateService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( DirectDebitMandateResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IDirectDebitMandateService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAccount(
        AssociationRequest request,
        IDirectDebitMandateService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignAccount(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignAccount(
    AssociationRequest request,
    IDirectDebitMandateService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignAccount(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCreditor(
        AssociationRequest request,
        IDirectDebitMandateService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCreditor(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCreditor(
    AssociationRequest request,
    IDirectDebitMandateService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCreditor(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static DirectDebitMandate mapRequestToDirectDebitMandate( DirectDebitMandateRequest request ) {
        var model = new DirectDebitMandate
        {
            Id = request.Id,
            MandateId = request.MandateId,
            SignedAt = request.SignedAt,
            Scheme = request.Scheme,
            Status = request.Status,
        };
        return model;
    }

}
