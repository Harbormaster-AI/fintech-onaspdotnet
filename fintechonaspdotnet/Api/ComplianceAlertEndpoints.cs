
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class ComplianceAlertEndpoints
{
    public static IEndpointRouteBuilder MapComplianceAlertEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/complianceAlert").WithTags("ComplianceAlerts");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignScreening", AssignScreening);
        group.MapPut("/unassignScreening", UnassignScreening);
        group.MapPut("/assignTransaction", AssignTransaction);
        group.MapPut("/unassignTransaction", UnassignTransaction);


        return app;
    }

    private static async Task<IResult> Create(
        ComplianceAlertRequest request,
        IComplianceAlertService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToComplianceAlert( request );

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
        ComplianceAlertRequest request,
        IComplianceAlertService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToComplianceAlert( request );

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
        IComplianceAlertService service,
        CancellationToken cancellationToken) {

        var complianceAlert = await service.Get(identifier, cancellationToken);
        return complianceAlert is null ? Results.NotFound() : Results.Ok( complianceAlert );
    }


    private static async Task<IResult> GetAll(
        IComplianceAlertService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ComplianceAlertResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IComplianceAlertService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignScreening(
        AssociationRequest request,
        IComplianceAlertService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignScreening(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignScreening(
    AssociationRequest request,
    IComplianceAlertService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignScreening(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTransaction(
        AssociationRequest request,
        IComplianceAlertService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignTransaction(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignTransaction(
    AssociationRequest request,
    IComplianceAlertService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignTransaction(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static ComplianceAlert mapRequestToComplianceAlert( ComplianceAlertRequest request ) {
        var model = new ComplianceAlert
        {
            Id = request.Id,
            AlertCode = request.AlertCode,
            RaisedAt = request.RaisedAt,
            Notes = request.Notes,
            Severity = request.Severity,
            Status = request.Status,
        };
        return model;
    }

}
