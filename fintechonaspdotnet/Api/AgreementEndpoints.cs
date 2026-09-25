
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class AgreementEndpoints
{
    public static IEndpointRouteBuilder MapAgreementEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/agreement").WithTags("Agreements");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignCustomer", AssignCustomer);
        group.MapPut("/unassignCustomer", UnassignCustomer);
        group.MapPut("/assignProductOffering", AssignProductOffering);
        group.MapPut("/unassignProductOffering", UnassignProductOffering);


        return app;
    }

    private static async Task<IResult> Create(
        AgreementRequest request,
        IAgreementService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAgreement( request );

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
        AgreementRequest request,
        IAgreementService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAgreement( request );

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
        IAgreementService service,
        CancellationToken cancellationToken) {

        var agreement = await service.Get(identifier, cancellationToken);
        return agreement is null ? Results.NotFound() : Results.Ok( agreement );
    }


    private static async Task<IResult> GetAll(
        IAgreementService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( AgreementResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IAgreementService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCustomer(
        AssociationRequest request,
        IAgreementService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCustomer(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCustomer(
    AssociationRequest request,
    IAgreementService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCustomer(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignProductOffering(
        AssociationRequest request,
        IAgreementService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignProductOffering(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignProductOffering(
    AssociationRequest request,
    IAgreementService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignProductOffering(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static Agreement mapRequestToAgreement( AgreementRequest request ) {
        var model = new Agreement
        {
            Id = request.Id,
            AgreementNumber = request.AgreementNumber,
            EffectiveDate = request.EffectiveDate,
            AgreementType = request.AgreementType,
            Status = request.Status,
        };
        return model;
    }

}
