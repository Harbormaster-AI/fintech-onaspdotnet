
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class BeneficiaryEndpoints
{
    public static IEndpointRouteBuilder MapBeneficiaryEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/beneficiary").WithTags("Beneficiarys");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignCustomer", AssignCustomer);
        group.MapPut("/unassignCustomer", UnassignCustomer);


        return app;
    }

    private static async Task<IResult> Create(
        BeneficiaryRequest request,
        IBeneficiaryService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToBeneficiary( request );

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
        BeneficiaryRequest request,
        IBeneficiaryService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToBeneficiary( request );

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
        IBeneficiaryService service,
        CancellationToken cancellationToken) {

        var beneficiary = await service.Get(identifier, cancellationToken);
        return beneficiary is null ? Results.NotFound() : Results.Ok( beneficiary );
    }


    private static async Task<IResult> GetAll(
        IBeneficiaryService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( BeneficiaryResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IBeneficiaryService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCustomer(
        AssociationRequest request,
        IBeneficiaryService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCustomer(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCustomer(
    AssociationRequest request,
    IBeneficiaryService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCustomer(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static Beneficiary mapRequestToBeneficiary( BeneficiaryRequest request ) {
        var model = new Beneficiary
        {
            Id = request.Id,
            Name = request.Name,
            AccountIdentifier = request.AccountIdentifier,
            Iban = request.Iban,
            Bic = request.Bic,
            Address = request.Address,
        };
        return model;
    }

}
