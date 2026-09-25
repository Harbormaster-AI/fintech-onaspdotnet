
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class PricingPlanEndpoints
{
    public static IEndpointRouteBuilder MapPricingPlanEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/pricingPlan").WithTags("PricingPlans");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignProductOffering", AssignProductOffering);
        group.MapPut("/unassignProductOffering", UnassignProductOffering);

    group.MapPut("/addToFeeSchedules", AddToFeeSchedules);
    group.MapPut("/removeFromFeeSchedules", RemoveFromFeeSchedules);

    group.MapPut("/addToLimits", AddToLimits);
    group.MapPut("/removeFromLimits", RemoveFromLimits);


        return app;
    }

    private static async Task<IResult> Create(
        PricingPlanRequest request,
        IPricingPlanService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPricingPlan( request );

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
        PricingPlanRequest request,
        IPricingPlanService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPricingPlan( request );

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
        IPricingPlanService service,
        CancellationToken cancellationToken) {

        var pricingPlan = await service.Get(identifier, cancellationToken);
        return pricingPlan is null ? Results.NotFound() : Results.Ok( pricingPlan );
    }


    private static async Task<IResult> GetAll(
        IPricingPlanService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( PricingPlanResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IPricingPlanService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignProductOffering(
        AssociationRequest request,
        IPricingPlanService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignProductOffering(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignProductOffering(
    AssociationRequest request,
    IPricingPlanService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignProductOffering(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToFeeSchedules(
        MultipleAssociationRequest request,
        IPricingPlanService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToFeeSchedules(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromFeeSchedules(
        MultipleAssociationRequest request,
        IPricingPlanService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromFeeSchedules(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToLimits(
        MultipleAssociationRequest request,
        IPricingPlanService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToLimits(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromLimits(
        MultipleAssociationRequest request,
        IPricingPlanService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromLimits(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static PricingPlan mapRequestToPricingPlan( PricingPlanRequest request ) {
        var model = new PricingPlan
        {
            Id = request.Id,
            Name = request.Name,
            PlanCode = request.PlanCode,
            BaseCurrency = request.BaseCurrency,
            Status = request.Status,
        };
        return model;
    }

}
