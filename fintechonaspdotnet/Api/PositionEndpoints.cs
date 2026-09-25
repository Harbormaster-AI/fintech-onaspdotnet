
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class PositionEndpoints
{
    public static IEndpointRouteBuilder MapPositionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/position").WithTags("Positions");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignPortfolio", AssignPortfolio);
        group.MapPut("/unassignPortfolio", UnassignPortfolio);
        group.MapPut("/assignSecurity", AssignSecurity);
        group.MapPut("/unassignSecurity", UnassignSecurity);


        return app;
    }

    private static async Task<IResult> Create(
        PositionRequest request,
        IPositionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPosition( request );

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
        PositionRequest request,
        IPositionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPosition( request );

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
        IPositionService service,
        CancellationToken cancellationToken) {

        var position = await service.Get(identifier, cancellationToken);
        return position is null ? Results.NotFound() : Results.Ok( position );
    }


    private static async Task<IResult> GetAll(
        IPositionService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( PositionResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IPositionService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPortfolio(
        AssociationRequest request,
        IPositionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPortfolio(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPortfolio(
    AssociationRequest request,
    IPositionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPortfolio(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSecurity(
        AssociationRequest request,
        IPositionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignSecurity(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignSecurity(
    AssociationRequest request,
    IPositionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignSecurity(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static Position mapRequestToPosition( PositionRequest request ) {
        var model = new Position
        {
            Id = request.Id,
            Quantity = request.Quantity,
            AverageCost = request.AverageCost,
            MarketValue = request.MarketValue,
        };
        return model;
    }

}
