
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class AccountEndpoints
{
    public static IEndpointRouteBuilder MapAccountEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/account").WithTags("Accounts");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignCustomer", AssignCustomer);
        group.MapPut("/unassignCustomer", UnassignCustomer);
        group.MapPut("/assignInstitution", AssignInstitution);
        group.MapPut("/unassignInstitution", UnassignInstitution);

    group.MapPut("/addToTransactions", AddToTransactions);
    group.MapPut("/removeFromTransactions", RemoveFromTransactions);

    group.MapPut("/addToCards", AddToCards);
    group.MapPut("/removeFromCards", RemoveFromCards);

    group.MapPut("/addToStatements", AddToStatements);
    group.MapPut("/removeFromStatements", RemoveFromStatements);

    group.MapPut("/addToMandates", AddToMandates);
    group.MapPut("/removeFromMandates", RemoveFromMandates);


        return app;
    }

    private static async Task<IResult> Create(
        AccountRequest request,
        IAccountService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAccount( request );

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
        AccountRequest request,
        IAccountService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAccount( request );

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
        IAccountService service,
        CancellationToken cancellationToken) {

        var account = await service.Get(identifier, cancellationToken);
        return account is null ? Results.NotFound() : Results.Ok( account );
    }


    private static async Task<IResult> GetAll(
        IAccountService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( AccountResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IAccountService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCustomer(
        AssociationRequest request,
        IAccountService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCustomer(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCustomer(
    AssociationRequest request,
    IAccountService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCustomer(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignInstitution(
        AssociationRequest request,
        IAccountService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignInstitution(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignInstitution(
    AssociationRequest request,
    IAccountService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignInstitution(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToTransactions(
        MultipleAssociationRequest request,
        IAccountService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToTransactions(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromTransactions(
        MultipleAssociationRequest request,
        IAccountService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromTransactions(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToCards(
        MultipleAssociationRequest request,
        IAccountService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCards(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCards(
        MultipleAssociationRequest request,
        IAccountService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCards(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToStatements(
        MultipleAssociationRequest request,
        IAccountService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToStatements(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromStatements(
        MultipleAssociationRequest request,
        IAccountService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromStatements(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToMandates(
        MultipleAssociationRequest request,
        IAccountService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToMandates(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromMandates(
        MultipleAssociationRequest request,
        IAccountService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromMandates(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Account mapRequestToAccount( AccountRequest request ) {
        var model = new Account
        {
            Id = request.Id,
            AccountNumber = request.AccountNumber,
            Iban = request.Iban,
            Bic = request.Bic,
            OpenedDate = request.OpenedDate,
            Currency = request.Currency,
            Balance = request.Balance,
            AvailableBalance = request.AvailableBalance,
            AccountType = request.AccountType,
            Status = request.Status,
        };
        return model;
    }

}
