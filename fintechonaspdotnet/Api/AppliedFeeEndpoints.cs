
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class AppliedFeeEndpoints
{
    public static IEndpointRouteBuilder MapAppliedFeeEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/appliedFee").WithTags("AppliedFees");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignPaymentOrder", AssignPaymentOrder);
        group.MapPut("/unassignPaymentOrder", UnassignPaymentOrder);
        group.MapPut("/assignTransaction", AssignTransaction);
        group.MapPut("/unassignTransaction", UnassignTransaction);


        return app;
    }

    private static async Task<IResult> Create(
        AppliedFeeRequest request,
        IAppliedFeeService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAppliedFee( request );

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
        AppliedFeeRequest request,
        IAppliedFeeService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAppliedFee( request );

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
        IAppliedFeeService service,
        CancellationToken cancellationToken) {

        var appliedFee = await service.Get(identifier, cancellationToken);
        return appliedFee is null ? Results.NotFound() : Results.Ok( appliedFee );
    }


    private static async Task<IResult> GetAll(
        IAppliedFeeService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( AppliedFeeResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IAppliedFeeService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPaymentOrder(
        AssociationRequest request,
        IAppliedFeeService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPaymentOrder(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPaymentOrder(
    AssociationRequest request,
    IAppliedFeeService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPaymentOrder(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTransaction(
        AssociationRequest request,
        IAppliedFeeService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignTransaction(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignTransaction(
    AssociationRequest request,
    IAppliedFeeService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignTransaction(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static AppliedFee mapRequestToAppliedFee( AppliedFeeRequest request ) {
        var model = new AppliedFee
        {
            Id = request.Id,
            Amount = request.Amount,
            Description = request.Description,
            FeeType = request.FeeType,
        };
        return model;
    }

}
