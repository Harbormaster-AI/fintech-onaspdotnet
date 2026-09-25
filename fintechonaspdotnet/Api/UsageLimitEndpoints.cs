
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class UsageLimitEndpoints
{
    public static IEndpointRouteBuilder MapUsageLimitEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/usageLimit").WithTags("UsageLimits");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignPricingPlan", AssignPricingPlan);
        group.MapPut("/unassignPricingPlan", UnassignPricingPlan);


        return app;
    }

    private static async Task<IResult> Create(
        UsageLimitRequest request,
        IUsageLimitService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToUsageLimit( request );

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
        UsageLimitRequest request,
        IUsageLimitService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToUsageLimit( request );

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
        IUsageLimitService service,
        CancellationToken cancellationToken) {

        var usageLimit = await service.Get(identifier, cancellationToken);
        return usageLimit is null ? Results.NotFound() : Results.Ok( usageLimit );
    }


    private static async Task<IResult> GetAll(
        IUsageLimitService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( UsageLimitResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IUsageLimitService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPricingPlan(
        AssociationRequest request,
        IUsageLimitService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPricingPlan(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPricingPlan(
    AssociationRequest request,
    IUsageLimitService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPricingPlan(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static UsageLimit mapRequestToUsageLimit( UsageLimitRequest request ) {
        var model = new UsageLimit
        {
            Id = request.Id,
            Name = request.Name,
            Amount = request.Amount,
            Count = request.Count,
            Scope = request.Scope,
            Period = request.Period,
        };
        return model;
    }

}
