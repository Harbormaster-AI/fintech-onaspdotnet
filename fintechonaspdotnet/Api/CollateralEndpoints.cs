
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class CollateralEndpoints
{
    public static IEndpointRouteBuilder MapCollateralEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/collateral").WithTags("Collaterals");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignLoan", AssignLoan);
        group.MapPut("/unassignLoan", UnassignLoan);


        return app;
    }

    private static async Task<IResult> Create(
        CollateralRequest request,
        ICollateralService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCollateral( request );

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
        CollateralRequest request,
        ICollateralService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCollateral( request );

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
        ICollateralService service,
        CancellationToken cancellationToken) {

        var collateral = await service.Get(identifier, cancellationToken);
        return collateral is null ? Results.NotFound() : Results.Ok( collateral );
    }


    private static async Task<IResult> GetAll(
        ICollateralService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( CollateralResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ICollateralService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignLoan(
        AssociationRequest request,
        ICollateralService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignLoan(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignLoan(
    AssociationRequest request,
    ICollateralService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignLoan(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static Collateral mapRequestToCollateral( CollateralRequest request ) {
        var model = new Collateral
        {
            Id = request.Id,
            Description = request.Description,
            Value = request.Value,
            CollateralType = request.CollateralType,
        };
        return model;
    }

}
