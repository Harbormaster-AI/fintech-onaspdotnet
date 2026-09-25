
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class InvestmentPortfolioEndpoints
{
    public static IEndpointRouteBuilder MapInvestmentPortfolioEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/investmentPortfolio").WithTags("InvestmentPortfolios");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignCustomer", AssignCustomer);
        group.MapPut("/unassignCustomer", UnassignCustomer);

    group.MapPut("/addToAccounts", AddToAccounts);
    group.MapPut("/removeFromAccounts", RemoveFromAccounts);

    group.MapPut("/addToOrders", AddToOrders);
    group.MapPut("/removeFromOrders", RemoveFromOrders);

    group.MapPut("/addToHoldings", AddToHoldings);
    group.MapPut("/removeFromHoldings", RemoveFromHoldings);


        return app;
    }

    private static async Task<IResult> Create(
        InvestmentPortfolioRequest request,
        IInvestmentPortfolioService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToInvestmentPortfolio( request );

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
        InvestmentPortfolioRequest request,
        IInvestmentPortfolioService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToInvestmentPortfolio( request );

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
        IInvestmentPortfolioService service,
        CancellationToken cancellationToken) {

        var investmentPortfolio = await service.Get(identifier, cancellationToken);
        return investmentPortfolio is null ? Results.NotFound() : Results.Ok( investmentPortfolio );
    }


    private static async Task<IResult> GetAll(
        IInvestmentPortfolioService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( InvestmentPortfolioResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IInvestmentPortfolioService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCustomer(
        AssociationRequest request,
        IInvestmentPortfolioService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCustomer(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCustomer(
    AssociationRequest request,
    IInvestmentPortfolioService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCustomer(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToAccounts(
        MultipleAssociationRequest request,
        IInvestmentPortfolioService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAccounts(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAccounts(
        MultipleAssociationRequest request,
        IInvestmentPortfolioService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAccounts(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToOrders(
        MultipleAssociationRequest request,
        IInvestmentPortfolioService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToOrders(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromOrders(
        MultipleAssociationRequest request,
        IInvestmentPortfolioService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromOrders(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToHoldings(
        MultipleAssociationRequest request,
        IInvestmentPortfolioService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToHoldings(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromHoldings(
        MultipleAssociationRequest request,
        IInvestmentPortfolioService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromHoldings(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static InvestmentPortfolio mapRequestToInvestmentPortfolio( InvestmentPortfolioRequest request ) {
        var model = new InvestmentPortfolio
        {
            Id = request.Id,
            PortfolioCode = request.PortfolioCode,
            BaseCurrency = request.BaseCurrency,
            CreatedAt = request.CreatedAt,
            Status = request.Status,
        };
        return model;
    }

}
