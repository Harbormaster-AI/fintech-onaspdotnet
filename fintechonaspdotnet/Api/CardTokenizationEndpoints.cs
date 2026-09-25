
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class CardTokenizationEndpoints
{
    public static IEndpointRouteBuilder MapCardTokenizationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/cardTokenization").WithTags("CardTokenizations");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignCard", AssignCard);
        group.MapPut("/unassignCard", UnassignCard);


        return app;
    }

    private static async Task<IResult> Create(
        CardTokenizationRequest request,
        ICardTokenizationService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCardTokenization( request );

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
        CardTokenizationRequest request,
        ICardTokenizationService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCardTokenization( request );

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
        ICardTokenizationService service,
        CancellationToken cancellationToken) {

        var cardTokenization = await service.Get(identifier, cancellationToken);
        return cardTokenization is null ? Results.NotFound() : Results.Ok( cardTokenization );
    }


    private static async Task<IResult> GetAll(
        ICardTokenizationService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( CardTokenizationResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ICardTokenizationService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCard(
        AssociationRequest request,
        ICardTokenizationService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCard(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCard(
    AssociationRequest request,
    ICardTokenizationService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCard(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static CardTokenization mapRequestToCardTokenization( CardTokenizationRequest request ) {
        var model = new CardTokenization
        {
            Id = request.Id,
            TokenReference = request.TokenReference,
            CreatedAt = request.CreatedAt,
            WalletProvider = request.WalletProvider,
            Status = request.Status,
        };
        return model;
    }

}
