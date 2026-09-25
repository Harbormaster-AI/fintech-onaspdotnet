
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class APIClientEndpoints
{
    public static IEndpointRouteBuilder MapAPIClientEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/aPIClient").WithTags("APIClients");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToConsents", AddToConsents);
    group.MapPut("/removeFromConsents", RemoveFromConsents);


        return app;
    }

    private static async Task<IResult> Create(
        APIClientRequest request,
        IAPIClientService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAPIClient( request );

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
        APIClientRequest request,
        IAPIClientService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAPIClient( request );

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
        IAPIClientService service,
        CancellationToken cancellationToken) {

        var aPIClient = await service.Get(identifier, cancellationToken);
        return aPIClient is null ? Results.NotFound() : Results.Ok( aPIClient );
    }


    private static async Task<IResult> GetAll(
        IAPIClientService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( APIClientResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IAPIClientService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToConsents(
        MultipleAssociationRequest request,
        IAPIClientService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToConsents(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromConsents(
        MultipleAssociationRequest request,
        IAPIClientService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromConsents(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static APIClient mapRequestToAPIClient( APIClientRequest request ) {
        var model = new APIClient
        {
            Id = request.Id,
            Name = request.Name,
            ClientId = request.ClientId,
            RedirectUri = request.RedirectUri,
            ClientType = request.ClientType,
        };
        return model;
    }

}
