
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class FeeScheduleEndpoints
{
    public static IEndpointRouteBuilder MapFeeScheduleEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/feeSchedule").WithTags("FeeSchedules");

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
        FeeScheduleRequest request,
        IFeeScheduleService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToFeeSchedule( request );

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
        FeeScheduleRequest request,
        IFeeScheduleService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToFeeSchedule( request );

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
        IFeeScheduleService service,
        CancellationToken cancellationToken) {

        var feeSchedule = await service.Get(identifier, cancellationToken);
        return feeSchedule is null ? Results.NotFound() : Results.Ok( feeSchedule );
    }


    private static async Task<IResult> GetAll(
        IFeeScheduleService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( FeeScheduleResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IFeeScheduleService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPricingPlan(
        AssociationRequest request,
        IFeeScheduleService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPricingPlan(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPricingPlan(
    AssociationRequest request,
    IFeeScheduleService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPricingPlan(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static FeeSchedule mapRequestToFeeSchedule( FeeScheduleRequest request ) {
        var model = new FeeSchedule
        {
            Id = request.Id,
            Name = request.Name,
            Amount = request.Amount,
            Percentage = request.Percentage,
            Minimum = request.Minimum,
            Maximum = request.Maximum,
            FeeType = request.FeeType,
            CalculationMethod = request.CalculationMethod,
        };
        return model;
    }

}
