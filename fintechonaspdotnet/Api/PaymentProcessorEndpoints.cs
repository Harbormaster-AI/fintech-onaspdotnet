
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class PaymentProcessorEndpoints
{
    public static IEndpointRouteBuilder MapPaymentProcessorEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/paymentProcessor").WithTags("PaymentProcessors");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToInstitutions", AddToInstitutions);
    group.MapPut("/removeFromInstitutions", RemoveFromInstitutions);

    group.MapPut("/addToContracts", AddToContracts);
    group.MapPut("/removeFromContracts", RemoveFromContracts);

    group.MapPut("/addToSettlements", AddToSettlements);
    group.MapPut("/removeFromSettlements", RemoveFromSettlements);


        return app;
    }

    private static async Task<IResult> Create(
        PaymentProcessorRequest request,
        IPaymentProcessorService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPaymentProcessor( request );

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
        PaymentProcessorRequest request,
        IPaymentProcessorService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPaymentProcessor( request );

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
        IPaymentProcessorService service,
        CancellationToken cancellationToken) {

        var paymentProcessor = await service.Get(identifier, cancellationToken);
        return paymentProcessor is null ? Results.NotFound() : Results.Ok( paymentProcessor );
    }


    private static async Task<IResult> GetAll(
        IPaymentProcessorService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( PaymentProcessorResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IPaymentProcessorService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToInstitutions(
        MultipleAssociationRequest request,
        IPaymentProcessorService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToInstitutions(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromInstitutions(
        MultipleAssociationRequest request,
        IPaymentProcessorService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromInstitutions(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToContracts(
        MultipleAssociationRequest request,
        IPaymentProcessorService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToContracts(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromContracts(
        MultipleAssociationRequest request,
        IPaymentProcessorService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromContracts(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToSettlements(
        MultipleAssociationRequest request,
        IPaymentProcessorService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToSettlements(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromSettlements(
        MultipleAssociationRequest request,
        IPaymentProcessorService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromSettlements(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static PaymentProcessor mapRequestToPaymentProcessor( PaymentProcessorRequest request ) {
        var model = new PaymentProcessor
        {
            Id = request.Id,
            Name = request.Name,
            ProcessorCode = request.ProcessorCode,
            NetworkSupport = request.NetworkSupport,
        };
        return model;
    }

}
