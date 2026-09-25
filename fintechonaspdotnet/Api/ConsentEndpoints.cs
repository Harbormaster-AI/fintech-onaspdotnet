
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class ConsentEndpoints
{
    public static IEndpointRouteBuilder MapConsentEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/consent").WithTags("Consents");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignCustomer", AssignCustomer);
        group.MapPut("/unassignCustomer", UnassignCustomer);
        group.MapPut("/assignApiClient", AssignApiClient);
        group.MapPut("/unassignApiClient", UnassignApiClient);


        return app;
    }

    private static async Task<IResult> Create(
        ConsentRequest request,
        IConsentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToConsent( request );

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
        ConsentRequest request,
        IConsentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToConsent( request );

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
        IConsentService service,
        CancellationToken cancellationToken) {

        var consent = await service.Get(identifier, cancellationToken);
        return consent is null ? Results.NotFound() : Results.Ok( consent );
    }


    private static async Task<IResult> GetAll(
        IConsentService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ConsentResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IConsentService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCustomer(
        AssociationRequest request,
        IConsentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCustomer(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCustomer(
    AssociationRequest request,
    IConsentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCustomer(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignApiClient(
        AssociationRequest request,
        IConsentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignApiClient(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignApiClient(
    AssociationRequest request,
    IConsentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignApiClient(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static Consent mapRequestToConsent( ConsentRequest request ) {
        var model = new Consent
        {
            Id = request.Id,
            GrantedAt = request.GrantedAt,
            ExpiresAt = request.ExpiresAt,
            Scope = request.Scope,
            ConsentType = request.ConsentType,
            Status = request.Status,
        };
        return model;
    }

}
