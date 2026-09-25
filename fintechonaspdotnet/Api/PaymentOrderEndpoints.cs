
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class PaymentOrderEndpoints
{
    public static IEndpointRouteBuilder MapPaymentOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/paymentOrder").WithTags("PaymentOrders");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignSourceAccount", AssignSourceAccount);
        group.MapPut("/unassignSourceAccount", UnassignSourceAccount);
        group.MapPut("/assignDestinationAccount", AssignDestinationAccount);
        group.MapPut("/unassignDestinationAccount", UnassignDestinationAccount);
        group.MapPut("/assignBeneficiary", AssignBeneficiary);
        group.MapPut("/unassignBeneficiary", UnassignBeneficiary);
        group.MapPut("/assignFxDeal", AssignFxDeal);
        group.MapPut("/unassignFxDeal", UnassignFxDeal);

    group.MapPut("/addToTransactions", AddToTransactions);
    group.MapPut("/removeFromTransactions", RemoveFromTransactions);

    group.MapPut("/addToFees", AddToFees);
    group.MapPut("/removeFromFees", RemoveFromFees);


        return app;
    }

    private static async Task<IResult> Create(
        PaymentOrderRequest request,
        IPaymentOrderService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPaymentOrder( request );

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
        PaymentOrderRequest request,
        IPaymentOrderService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPaymentOrder( request );

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
        IPaymentOrderService service,
        CancellationToken cancellationToken) {

        var paymentOrder = await service.Get(identifier, cancellationToken);
        return paymentOrder is null ? Results.NotFound() : Results.Ok( paymentOrder );
    }


    private static async Task<IResult> GetAll(
        IPaymentOrderService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( PaymentOrderResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IPaymentOrderService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSourceAccount(
        AssociationRequest request,
        IPaymentOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignSourceAccount(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignSourceAccount(
    AssociationRequest request,
    IPaymentOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignSourceAccount(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDestinationAccount(
        AssociationRequest request,
        IPaymentOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignDestinationAccount(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignDestinationAccount(
    AssociationRequest request,
    IPaymentOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignDestinationAccount(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignBeneficiary(
        AssociationRequest request,
        IPaymentOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignBeneficiary(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignBeneficiary(
    AssociationRequest request,
    IPaymentOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignBeneficiary(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignFxDeal(
        AssociationRequest request,
        IPaymentOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignFxDeal(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignFxDeal(
    AssociationRequest request,
    IPaymentOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignFxDeal(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToTransactions(
        MultipleAssociationRequest request,
        IPaymentOrderService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToTransactions(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromTransactions(
        MultipleAssociationRequest request,
        IPaymentOrderService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromTransactions(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToFees(
        MultipleAssociationRequest request,
        IPaymentOrderService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToFees(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromFees(
        MultipleAssociationRequest request,
        IPaymentOrderService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromFees(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static PaymentOrder mapRequestToPaymentOrder( PaymentOrderRequest request ) {
        var model = new PaymentOrder
        {
            Id = request.Id,
            OrderReference = request.OrderReference,
            RequestedExecutionDate = request.RequestedExecutionDate,
            Purpose = request.Purpose,
            PaymentMethod = request.PaymentMethod,
            Status = request.Status,
            Priority = request.Priority,
        };
        return model;
    }

}
