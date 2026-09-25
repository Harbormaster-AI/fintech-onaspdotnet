
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class AccountStatementEndpoints
{
    public static IEndpointRouteBuilder MapAccountStatementEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/accountStatement").WithTags("AccountStatements");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignAccount", AssignAccount);
        group.MapPut("/unassignAccount", UnassignAccount);


        return app;
    }

    private static async Task<IResult> Create(
        AccountStatementRequest request,
        IAccountStatementService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAccountStatement( request );

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
        AccountStatementRequest request,
        IAccountStatementService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAccountStatement( request );

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
        IAccountStatementService service,
        CancellationToken cancellationToken) {

        var accountStatement = await service.Get(identifier, cancellationToken);
        return accountStatement is null ? Results.NotFound() : Results.Ok( accountStatement );
    }


    private static async Task<IResult> GetAll(
        IAccountStatementService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( AccountStatementResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IAccountStatementService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAccount(
        AssociationRequest request,
        IAccountStatementService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignAccount(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignAccount(
    AssociationRequest request,
    IAccountStatementService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignAccount(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static AccountStatement mapRequestToAccountStatement( AccountStatementRequest request ) {
        var model = new AccountStatement
        {
            Id = request.Id,
            StatementNumber = request.StatementNumber,
            PeriodStart = request.PeriodStart,
            PeriodEnd = request.PeriodEnd,
            OpeningBalance = request.OpeningBalance,
            ClosingBalance = request.ClosingBalance,
            GeneratedAt = request.GeneratedAt,
        };
        return model;
    }

}
