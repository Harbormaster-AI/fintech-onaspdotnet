
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class LoanTransactionEndpoints
{
    public static IEndpointRouteBuilder MapLoanTransactionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/loanTransaction").WithTags("LoanTransactions");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignLoan", AssignLoan);
        group.MapPut("/unassignLoan", UnassignLoan);


        return app;
    }

    private static async Task<IResult> Create(
        LoanTransactionRequest request,
        ILoanTransactionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToLoanTransaction( request );

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
        LoanTransactionRequest request,
        ILoanTransactionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToLoanTransaction( request );

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
        ILoanTransactionService service,
        CancellationToken cancellationToken) {

        var loanTransaction = await service.Get(identifier, cancellationToken);
        return loanTransaction is null ? Results.NotFound() : Results.Ok( loanTransaction );
    }


    private static async Task<IResult> GetAll(
        ILoanTransactionService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( LoanTransactionResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ILoanTransactionService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignLoan(
        AssociationRequest request,
        ILoanTransactionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignLoan(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignLoan(
    AssociationRequest request,
    ILoanTransactionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignLoan(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static LoanTransaction mapRequestToLoanTransaction( LoanTransactionRequest request ) {
        var model = new LoanTransaction
        {
            Id = request.Id,
            TransactionId = request.TransactionId,
            Amount = request.Amount,
            PostingDate = request.PostingDate,
            Type = request.Type,
            Status = request.Status,
        };
        return model;
    }

}
