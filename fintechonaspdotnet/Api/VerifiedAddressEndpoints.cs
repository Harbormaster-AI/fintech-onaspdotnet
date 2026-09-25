
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class VerifiedAddressEndpoints
{
    public static IEndpointRouteBuilder MapVerifiedAddressEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/verifiedAddress").WithTags("VerifiedAddresss");

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
        VerifiedAddressRequest request,
        IVerifiedAddressService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToVerifiedAddress( request );

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
        VerifiedAddressRequest request,
        IVerifiedAddressService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToVerifiedAddress( request );

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
        IVerifiedAddressService service,
        CancellationToken cancellationToken) {

        var verifiedAddress = await service.Get(identifier, cancellationToken);
        return verifiedAddress is null ? Results.NotFound() : Results.Ok( verifiedAddress );
    }


    private static async Task<IResult> GetAll(
        IVerifiedAddressService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( VerifiedAddressResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IVerifiedAddressService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignKycProfile(
        AssociationRequest request,
        IVerifiedAddressService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignKycProfile(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignKycProfile(
    AssociationRequest request,
    IVerifiedAddressService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignKycProfile(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static VerifiedAddress mapRequestToVerifiedAddress( VerifiedAddressRequest request ) {
        var model = new VerifiedAddress
        {
            Id = request.Id,
            Address = request.Address,
            VerifiedAt = request.VerifiedAt,
            VerificationStatus = request.VerificationStatus,
        };
        return model;
    }

}
