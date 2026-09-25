
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class FXQuoteEndpoints
{
    public static IEndpointRouteBuilder MapFXQuoteEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/fXQuote").WithTags("FXQuotes");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignRequestedBy", AssignRequestedBy);
        group.MapPut("/unassignRequestedBy", UnassignRequestedBy);


        return app;
    }

    private static async Task<IResult> Create(
        FXQuoteRequest request,
        IFXQuoteService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToFXQuote( request );

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
        FXQuoteRequest request,
        IFXQuoteService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToFXQuote( request );

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
        IFXQuoteService service,
        CancellationToken cancellationToken) {

        var fXQuote = await service.Get(identifier, cancellationToken);
        return fXQuote is null ? Results.NotFound() : Results.Ok( fXQuote );
    }


    private static async Task<IResult> GetAll(
        IFXQuoteService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( FXQuoteResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IFXQuoteService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignRequestedBy(
        AssociationRequest request,
        IFXQuoteService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignRequestedBy(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignRequestedBy(
    AssociationRequest request,
    IFXQuoteService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignRequestedBy(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static FXQuote mapRequestToFXQuote( FXQuoteRequest request ) {
        var model = new FXQuote
        {
            Id = request.Id,
            BaseCurrency = request.BaseCurrency,
            QuoteCurrency = request.QuoteCurrency,
            Rate = request.Rate,
            QuotedAt = request.QuotedAt,
            ExpiresAt = request.ExpiresAt,
            PriceType = request.PriceType,
        };
        return model;
    }

}
