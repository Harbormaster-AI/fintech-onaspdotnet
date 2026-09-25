
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class CreditorEndpoints
{
    public static IEndpointRouteBuilder MapCreditorEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/creditor").WithTags("Creditors");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToMandates", AddToMandates);
    group.MapPut("/removeFromMandates", RemoveFromMandates);


        return app;
    }

    private static async Task<IResult> Create(
        CreditorRequest request,
        ICreditorService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCreditor( request );

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
        CreditorRequest request,
        ICreditorService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCreditor( request );

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
        ICreditorService service,
        CancellationToken cancellationToken) {

        var creditor = await service.Get(identifier, cancellationToken);
        return creditor is null ? Results.NotFound() : Results.Ok( creditor );
    }


    private static async Task<IResult> GetAll(
        ICreditorService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( CreditorResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ICreditorService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToMandates(
        MultipleAssociationRequest request,
        ICreditorService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToMandates(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromMandates(
        MultipleAssociationRequest request,
        ICreditorService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromMandates(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Creditor mapRequestToCreditor( CreditorRequest request ) {
        var model = new Creditor
        {
            Id = request.Id,
            Name = request.Name,
            Bic = request.Bic,
            Address = request.Address,
        };
        return model;
    }

}
