
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class InvestmentAccountEndpoints
{
    public static IEndpointRouteBuilder MapInvestmentAccountEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/investmentAccount").WithTags("InvestmentAccounts");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignPortfolio", AssignPortfolio);
        group.MapPut("/unassignPortfolio", UnassignPortfolio);

    group.MapPut("/addToTrades", AddToTrades);
    group.MapPut("/removeFromTrades", RemoveFromTrades);

    group.MapPut("/addToOrders", AddToOrders);
    group.MapPut("/removeFromOrders", RemoveFromOrders);


        return app;
    }

    private static async Task<IResult> Create(
        InvestmentAccountRequest request,
        IInvestmentAccountService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToInvestmentAccount( request );

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
        InvestmentAccountRequest request,
        IInvestmentAccountService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToInvestmentAccount( request );

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
        IInvestmentAccountService service,
        CancellationToken cancellationToken) {

        var investmentAccount = await service.Get(identifier, cancellationToken);
        return investmentAccount is null ? Results.NotFound() : Results.Ok( investmentAccount );
    }


    private static async Task<IResult> GetAll(
        IInvestmentAccountService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( InvestmentAccountResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IInvestmentAccountService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPortfolio(
        AssociationRequest request,
        IInvestmentAccountService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPortfolio(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPortfolio(
    AssociationRequest request,
    IInvestmentAccountService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPortfolio(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToTrades(
        MultipleAssociationRequest request,
        IInvestmentAccountService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToTrades(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromTrades(
        MultipleAssociationRequest request,
        IInvestmentAccountService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromTrades(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToOrders(
        MultipleAssociationRequest request,
        IInvestmentAccountService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToOrders(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromOrders(
        MultipleAssociationRequest request,
        IInvestmentAccountService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromOrders(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static InvestmentAccount mapRequestToInvestmentAccount( InvestmentAccountRequest request ) {
        var model = new InvestmentAccount
        {
            Id = request.Id,
            AccountNumber = request.AccountNumber,
            BaseCurrency = request.BaseCurrency,
            Balance = request.Balance,
            AccountType = request.AccountType,
            Status = request.Status,
        };
        return model;
    }

}
