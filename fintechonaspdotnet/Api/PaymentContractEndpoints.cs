
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class PaymentContractEndpoints
{
    public static IEndpointRouteBuilder MapPaymentContractEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/paymentContract").WithTags("PaymentContracts");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignMerchant", AssignMerchant);
        group.MapPut("/unassignMerchant", UnassignMerchant);
        group.MapPut("/assignAcquirer", AssignAcquirer);
        group.MapPut("/unassignAcquirer", UnassignAcquirer);


        return app;
    }

    private static async Task<IResult> Create(
        PaymentContractRequest request,
        IPaymentContractService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPaymentContract( request );

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
        PaymentContractRequest request,
        IPaymentContractService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPaymentContract( request );

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
        IPaymentContractService service,
        CancellationToken cancellationToken) {

        var paymentContract = await service.Get(identifier, cancellationToken);
        return paymentContract is null ? Results.NotFound() : Results.Ok( paymentContract );
    }


    private static async Task<IResult> GetAll(
        IPaymentContractService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( PaymentContractResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IPaymentContractService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignMerchant(
        AssociationRequest request,
        IPaymentContractService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignMerchant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignMerchant(
    AssociationRequest request,
    IPaymentContractService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignMerchant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAcquirer(
        AssociationRequest request,
        IPaymentContractService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignAcquirer(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignAcquirer(
    AssociationRequest request,
    IPaymentContractService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignAcquirer(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static PaymentContract mapRequestToPaymentContract( PaymentContractRequest request ) {
        var model = new PaymentContract
        {
            Id = request.Id,
            ContractNumber = request.ContractNumber,
            PricingPlanCode = request.PricingPlanCode,
            Status = request.Status,
        };
        return model;
    }

}
