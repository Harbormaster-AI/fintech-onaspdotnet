
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class LoanEndpoints
{
    public static IEndpointRouteBuilder MapLoanEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/loan").WithTags("Loans");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignCustomer", AssignCustomer);
        group.MapPut("/unassignCustomer", UnassignCustomer);

    group.MapPut("/addToSchedule", AddToSchedule);
    group.MapPut("/removeFromSchedule", RemoveFromSchedule);

    group.MapPut("/addToCollateral", AddToCollateral);
    group.MapPut("/removeFromCollateral", RemoveFromCollateral);

    group.MapPut("/addToTransactions", AddToTransactions);
    group.MapPut("/removeFromTransactions", RemoveFromTransactions);


        return app;
    }

    private static async Task<IResult> Create(
        LoanRequest request,
        ILoanService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToLoan( request );

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
        LoanRequest request,
        ILoanService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToLoan( request );

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
        ILoanService service,
        CancellationToken cancellationToken) {

        var loan = await service.Get(identifier, cancellationToken);
        return loan is null ? Results.NotFound() : Results.Ok( loan );
    }


    private static async Task<IResult> GetAll(
        ILoanService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( LoanResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ILoanService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCustomer(
        AssociationRequest request,
        ILoanService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCustomer(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCustomer(
    AssociationRequest request,
    ILoanService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCustomer(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToSchedule(
        MultipleAssociationRequest request,
        ILoanService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToSchedule(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromSchedule(
        MultipleAssociationRequest request,
        ILoanService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromSchedule(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToCollateral(
        MultipleAssociationRequest request,
        ILoanService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCollateral(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCollateral(
        MultipleAssociationRequest request,
        ILoanService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCollateral(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToTransactions(
        MultipleAssociationRequest request,
        ILoanService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToTransactions(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromTransactions(
        MultipleAssociationRequest request,
        ILoanService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromTransactions(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Loan mapRequestToLoan( LoanRequest request ) {
        var model = new Loan
        {
            Id = request.Id,
            LoanNumber = request.LoanNumber,
            Principal = request.Principal,
            InterestRate = request.InterestRate,
            OriginationDate = request.OriginationDate,
            MaturityDate = request.MaturityDate,
            RateType = request.RateType,
            Status = request.Status,
        };
        return model;
    }

}
