
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class TransactionEndpoints
{
    public static IEndpointRouteBuilder MapTransactionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/transaction").WithTags("Transactions");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignAccount", AssignAccount);
        group.MapPut("/unassignAccount", UnassignAccount);
        group.MapPut("/assignWallet", AssignWallet);
        group.MapPut("/unassignWallet", UnassignWallet);
        group.MapPut("/assignPaymentOrder", AssignPaymentOrder);
        group.MapPut("/unassignPaymentOrder", UnassignPaymentOrder);
        group.MapPut("/assignMerchant", AssignMerchant);
        group.MapPut("/unassignMerchant", UnassignMerchant);
        group.MapPut("/assignCard", AssignCard);
        group.MapPut("/unassignCard", UnassignCard);

    group.MapPut("/addToRelatedTransactions", AddToRelatedTransactions);
    group.MapPut("/removeFromRelatedTransactions", RemoveFromRelatedTransactions);

    group.MapPut("/addToAlerts", AddToAlerts);
    group.MapPut("/removeFromAlerts", RemoveFromAlerts);


        return app;
    }

    private static async Task<IResult> Create(
        TransactionRequest request,
        ITransactionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToTransaction( request );

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
        TransactionRequest request,
        ITransactionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToTransaction( request );

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
        ITransactionService service,
        CancellationToken cancellationToken) {

        var transaction = await service.Get(identifier, cancellationToken);
        return transaction is null ? Results.NotFound() : Results.Ok( transaction );
    }


    private static async Task<IResult> GetAll(
        ITransactionService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( TransactionResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ITransactionService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAccount(
        AssociationRequest request,
        ITransactionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignAccount(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignAccount(
    AssociationRequest request,
    ITransactionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignAccount(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignWallet(
        AssociationRequest request,
        ITransactionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignWallet(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignWallet(
    AssociationRequest request,
    ITransactionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignWallet(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPaymentOrder(
        AssociationRequest request,
        ITransactionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPaymentOrder(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPaymentOrder(
    AssociationRequest request,
    ITransactionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPaymentOrder(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignMerchant(
        AssociationRequest request,
        ITransactionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignMerchant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignMerchant(
    AssociationRequest request,
    ITransactionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignMerchant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCard(
        AssociationRequest request,
        ITransactionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCard(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCard(
    AssociationRequest request,
    ITransactionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCard(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToRelatedTransactions(
        MultipleAssociationRequest request,
        ITransactionService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToRelatedTransactions(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromRelatedTransactions(
        MultipleAssociationRequest request,
        ITransactionService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromRelatedTransactions(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToAlerts(
        MultipleAssociationRequest request,
        ITransactionService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAlerts(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAlerts(
        MultipleAssociationRequest request,
        ITransactionService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAlerts(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Transaction mapRequestToTransaction( TransactionRequest request ) {
        var model = new Transaction
        {
            Id = request.Id,
            Amount = request.Amount,
            Fee = request.Fee,
            ExchangeRate = request.ExchangeRate,
            CreatedAt = request.CreatedAt,
            CompletedAt = request.CompletedAt,
            Narrative = request.Narrative,
            TransactionType = request.TransactionType,
            Status = request.Status,
        };
        return model;
    }

}
