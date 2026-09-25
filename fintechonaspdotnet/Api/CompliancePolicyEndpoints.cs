
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class CompliancePolicyEndpoints
{
    public static IEndpointRouteBuilder MapCompliancePolicyEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/compliancePolicy").WithTags("CompliancePolicys");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignInstitution", AssignInstitution);
        group.MapPut("/unassignInstitution", UnassignInstitution);


        return app;
    }

    private static async Task<IResult> Create(
        CompliancePolicyRequest request,
        ICompliancePolicyService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCompliancePolicy( request );

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
        CompliancePolicyRequest request,
        ICompliancePolicyService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCompliancePolicy( request );

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
        ICompliancePolicyService service,
        CancellationToken cancellationToken) {

        var compliancePolicy = await service.Get(identifier, cancellationToken);
        return compliancePolicy is null ? Results.NotFound() : Results.Ok( compliancePolicy );
    }


    private static async Task<IResult> GetAll(
        ICompliancePolicyService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( CompliancePolicyResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ICompliancePolicyService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignInstitution(
        AssociationRequest request,
        ICompliancePolicyService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignInstitution(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignInstitution(
    AssociationRequest request,
    ICompliancePolicyService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignInstitution(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static CompliancePolicy mapRequestToCompliancePolicy( CompliancePolicyRequest request ) {
        var model = new CompliancePolicy
        {
            Id = request.Id,
            Name = request.Name,
            PolicyCode = request.PolicyCode,
            Description = request.Description,
            Status = request.Status,
        };
        return model;
    }

}
