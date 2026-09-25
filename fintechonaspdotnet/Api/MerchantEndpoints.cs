
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class MerchantEndpoints
{
    public static IEndpointRouteBuilder MapMerchantEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/merchant").WithTags("Merchants");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToTerminals", AddToTerminals);
    group.MapPut("/removeFromTerminals", RemoveFromTerminals);

    group.MapPut("/addToPaymentContracts", AddToPaymentContracts);
    group.MapPut("/removeFromPaymentContracts", RemoveFromPaymentContracts);

    group.MapPut("/addToPayouts", AddToPayouts);
    group.MapPut("/removeFromPayouts", RemoveFromPayouts);

    group.MapPut("/addToSettlements", AddToSettlements);
    group.MapPut("/removeFromSettlements", RemoveFromSettlements);

    group.MapPut("/addToDisputes", AddToDisputes);
    group.MapPut("/removeFromDisputes", RemoveFromDisputes);

    group.MapPut("/addToInvoices", AddToInvoices);
    group.MapPut("/removeFromInvoices", RemoveFromInvoices);


        return app;
    }

    private static async Task<IResult> Create(
        MerchantRequest request,
        IMerchantService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToMerchant( request );

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
        MerchantRequest request,
        IMerchantService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToMerchant( request );

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
        IMerchantService service,
        CancellationToken cancellationToken) {

        var merchant = await service.Get(identifier, cancellationToken);
        return merchant is null ? Results.NotFound() : Results.Ok( merchant );
    }


    private static async Task<IResult> GetAll(
        IMerchantService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( MerchantResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IMerchantService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToTerminals(
        MultipleAssociationRequest request,
        IMerchantService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToTerminals(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromTerminals(
        MultipleAssociationRequest request,
        IMerchantService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromTerminals(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToPaymentContracts(
        MultipleAssociationRequest request,
        IMerchantService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPaymentContracts(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPaymentContracts(
        MultipleAssociationRequest request,
        IMerchantService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPaymentContracts(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToPayouts(
        MultipleAssociationRequest request,
        IMerchantService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPayouts(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPayouts(
        MultipleAssociationRequest request,
        IMerchantService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPayouts(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToSettlements(
        MultipleAssociationRequest request,
        IMerchantService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToSettlements(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromSettlements(
        MultipleAssociationRequest request,
        IMerchantService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromSettlements(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToDisputes(
        MultipleAssociationRequest request,
        IMerchantService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDisputes(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDisputes(
        MultipleAssociationRequest request,
        IMerchantService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDisputes(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToInvoices(
        MultipleAssociationRequest request,
        IMerchantService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToInvoices(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromInvoices(
        MultipleAssociationRequest request,
        IMerchantService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromInvoices(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Merchant mapRequestToMerchant( MerchantRequest request ) {
        var model = new Merchant
        {
            Id = request.Id,
            Name = request.Name,
            Mcc = request.Mcc,
            Url = request.Url,
            Country = request.Country,
            SettlementCurrency = request.SettlementCurrency,
        };
        return model;
    }

}
