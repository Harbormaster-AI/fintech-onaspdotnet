
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class ScreeningEndpoints
{
    public static IEndpointRouteBuilder MapScreeningEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/screening").WithTags("Screenings");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignKycProfile", AssignKycProfile);
        group.MapPut("/unassignKycProfile", UnassignKycProfile);

    group.MapPut("/addToAlerts", AddToAlerts);
    group.MapPut("/removeFromAlerts", RemoveFromAlerts);


        return app;
    }

    private static async Task<IResult> Create(
        ScreeningRequest request,
        IScreeningService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToScreening( request );

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
        ScreeningRequest request,
        IScreeningService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToScreening( request );

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
        IScreeningService service,
        CancellationToken cancellationToken) {

        var screening = await service.Get(identifier, cancellationToken);
        return screening is null ? Results.NotFound() : Results.Ok( screening );
    }


    private static async Task<IResult> GetAll(
        IScreeningService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ScreeningResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IScreeningService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignKycProfile(
        AssociationRequest request,
        IScreeningService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignKycProfile(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignKycProfile(
    AssociationRequest request,
    IScreeningService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignKycProfile(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToAlerts(
        MultipleAssociationRequest request,
        IScreeningService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAlerts(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAlerts(
        MultipleAssociationRequest request,
        IScreeningService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAlerts(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Screening mapRequestToScreening( ScreeningRequest request ) {
        var model = new Screening
        {
            Id = request.Id,
            Score = request.Score,
            ScreenedAt = request.ScreenedAt,
            ScreeningType = request.ScreeningType,
            Status = request.Status,
        };
        return model;
    }

}
