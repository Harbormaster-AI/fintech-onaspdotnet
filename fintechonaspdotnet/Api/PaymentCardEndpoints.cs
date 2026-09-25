
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class PaymentCardEndpoints
{
    public static IEndpointRouteBuilder MapPaymentCardEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/paymentCard").WithTags("PaymentCards");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignCustomer", AssignCustomer);
        group.MapPut("/unassignCustomer", UnassignCustomer);
        group.MapPut("/assignAccount", AssignAccount);
        group.MapPut("/unassignAccount", UnassignAccount);

    group.MapPut("/addToTokenizations", AddToTokenizations);
    group.MapPut("/removeFromTokenizations", RemoveFromTokenizations);

    group.MapPut("/addToDisputes", AddToDisputes);
    group.MapPut("/removeFromDisputes", RemoveFromDisputes);


        return app;
    }

    private static async Task<IResult> Create(
        PaymentCardRequest request,
        IPaymentCardService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPaymentCard( request );

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
        PaymentCardRequest request,
        IPaymentCardService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPaymentCard( request );

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
        IPaymentCardService service,
        CancellationToken cancellationToken) {

        var paymentCard = await service.Get(identifier, cancellationToken);
        return paymentCard is null ? Results.NotFound() : Results.Ok( paymentCard );
    }


    private static async Task<IResult> GetAll(
        IPaymentCardService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( PaymentCardResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IPaymentCardService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCustomer(
        AssociationRequest request,
        IPaymentCardService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCustomer(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCustomer(
    AssociationRequest request,
    IPaymentCardService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCustomer(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAccount(
        AssociationRequest request,
        IPaymentCardService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignAccount(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignAccount(
    AssociationRequest request,
    IPaymentCardService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignAccount(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToTokenizations(
        MultipleAssociationRequest request,
        IPaymentCardService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToTokenizations(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromTokenizations(
        MultipleAssociationRequest request,
        IPaymentCardService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromTokenizations(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToDisputes(
        MultipleAssociationRequest request,
        IPaymentCardService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDisputes(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDisputes(
        MultipleAssociationRequest request,
        IPaymentCardService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDisputes(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static PaymentCard mapRequestToPaymentCard( PaymentCardRequest request ) {
        var model = new PaymentCard
        {
            Id = request.Id,
            CardToken = request.CardToken,
            MaskedPan = request.MaskedPan,
            ExpiryMonth = request.ExpiryMonth,
            ExpiryYear = request.ExpiryYear,
            CardholderName = request.CardholderName,
            Scheme = request.Scheme,
            Status = request.Status,
        };
        return model;
    }

}
