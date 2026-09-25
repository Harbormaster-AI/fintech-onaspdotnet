
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class LoanApplicationEndpoints
{
    public static IEndpointRouteBuilder MapLoanApplicationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/loanApplication").WithTags("LoanApplications");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignCustomer", AssignCustomer);
        group.MapPut("/unassignCustomer", UnassignCustomer);
        group.MapPut("/assignRiskAssessment", AssignRiskAssessment);
        group.MapPut("/unassignRiskAssessment", UnassignRiskAssessment);
        group.MapPut("/assignLoan", AssignLoan);
        group.MapPut("/unassignLoan", UnassignLoan);


        return app;
    }

    private static async Task<IResult> Create(
        LoanApplicationRequest request,
        ILoanApplicationService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToLoanApplication( request );

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
        LoanApplicationRequest request,
        ILoanApplicationService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToLoanApplication( request );

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
        ILoanApplicationService service,
        CancellationToken cancellationToken) {

        var loanApplication = await service.Get(identifier, cancellationToken);
        return loanApplication is null ? Results.NotFound() : Results.Ok( loanApplication );
    }


    private static async Task<IResult> GetAll(
        ILoanApplicationService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( LoanApplicationResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ILoanApplicationService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCustomer(
        AssociationRequest request,
        ILoanApplicationService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCustomer(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCustomer(
    AssociationRequest request,
    ILoanApplicationService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCustomer(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignRiskAssessment(
        AssociationRequest request,
        ILoanApplicationService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignRiskAssessment(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignRiskAssessment(
    AssociationRequest request,
    ILoanApplicationService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignRiskAssessment(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignLoan(
        AssociationRequest request,
        ILoanApplicationService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignLoan(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignLoan(
    AssociationRequest request,
    ILoanApplicationService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignLoan(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static LoanApplication mapRequestToLoanApplication( LoanApplicationRequest request ) {
        var model = new LoanApplication
        {
            Id = request.Id,
            ApplicationNumber = request.ApplicationNumber,
            AmountRequested = request.AmountRequested,
            TermMonths = request.TermMonths,
            SubmittedAt = request.SubmittedAt,
            Product = request.Product,
            Purpose = request.Purpose,
            Status = request.Status,
        };
        return model;
    }

}
