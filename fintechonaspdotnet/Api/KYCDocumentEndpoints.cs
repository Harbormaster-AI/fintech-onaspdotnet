
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class KYCDocumentEndpoints
{
    public static IEndpointRouteBuilder MapKYCDocumentEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/kYCDocument").WithTags("KYCDocuments");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignKycProfile", AssignKycProfile);
        group.MapPut("/unassignKycProfile", UnassignKycProfile);


        return app;
    }

    private static async Task<IResult> Create(
        KYCDocumentRequest request,
        IKYCDocumentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToKYCDocument( request );

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
        KYCDocumentRequest request,
        IKYCDocumentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToKYCDocument( request );

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
        IKYCDocumentService service,
        CancellationToken cancellationToken) {

        var kYCDocument = await service.Get(identifier, cancellationToken);
        return kYCDocument is null ? Results.NotFound() : Results.Ok( kYCDocument );
    }


    private static async Task<IResult> GetAll(
        IKYCDocumentService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( KYCDocumentResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IKYCDocumentService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignKycProfile(
        AssociationRequest request,
        IKYCDocumentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignKycProfile(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignKycProfile(
    AssociationRequest request,
    IKYCDocumentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignKycProfile(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static KYCDocument mapRequestToKYCDocument( KYCDocumentRequest request ) {
        var model = new KYCDocument
        {
            Id = request.Id,
            Reference = request.Reference,
            IssuedCountry = request.IssuedCountry,
            ExpirationDate = request.ExpirationDate,
            DocumentType = request.DocumentType,
            Status = request.Status,
        };
        return model;
    }

}
