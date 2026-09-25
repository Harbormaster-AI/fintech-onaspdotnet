
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class KYCProfileEndpoints
{
    public static IEndpointRouteBuilder MapKYCProfileEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/kYCProfile").WithTags("KYCProfiles");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignCustomer", AssignCustomer);
        group.MapPut("/unassignCustomer", UnassignCustomer);

    group.MapPut("/addToDocuments", AddToDocuments);
    group.MapPut("/removeFromDocuments", RemoveFromDocuments);

    group.MapPut("/addToScreenings", AddToScreenings);
    group.MapPut("/removeFromScreenings", RemoveFromScreenings);

    group.MapPut("/addToAddresses", AddToAddresses);
    group.MapPut("/removeFromAddresses", RemoveFromAddresses);


        return app;
    }

    private static async Task<IResult> Create(
        KYCProfileRequest request,
        IKYCProfileService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToKYCProfile( request );

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
        KYCProfileRequest request,
        IKYCProfileService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToKYCProfile( request );

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
        IKYCProfileService service,
        CancellationToken cancellationToken) {

        var kYCProfile = await service.Get(identifier, cancellationToken);
        return kYCProfile is null ? Results.NotFound() : Results.Ok( kYCProfile );
    }


    private static async Task<IResult> GetAll(
        IKYCProfileService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( KYCProfileResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IKYCProfileService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCustomer(
        AssociationRequest request,
        IKYCProfileService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCustomer(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCustomer(
    AssociationRequest request,
    IKYCProfileService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCustomer(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToDocuments(
        MultipleAssociationRequest request,
        IKYCProfileService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDocuments(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDocuments(
        MultipleAssociationRequest request,
        IKYCProfileService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDocuments(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToScreenings(
        MultipleAssociationRequest request,
        IKYCProfileService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToScreenings(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromScreenings(
        MultipleAssociationRequest request,
        IKYCProfileService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromScreenings(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToAddresses(
        MultipleAssociationRequest request,
        IKYCProfileService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAddresses(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAddresses(
        MultipleAssociationRequest request,
        IKYCProfileService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAddresses(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static KYCProfile mapRequestToKYCProfile( KYCProfileRequest request ) {
        var model = new KYCProfile
        {
            Id = request.Id,
            ProfileId = request.ProfileId,
            CreatedAt = request.CreatedAt,
            Status = request.Status,
            VerificationLevel = request.VerificationLevel,
        };
        return model;
    }

}
