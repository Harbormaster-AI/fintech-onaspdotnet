
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class RepaymentScheduleEndpoints
{
    public static IEndpointRouteBuilder MapRepaymentScheduleEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/repaymentSchedule").WithTags("RepaymentSchedules");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignLoan", AssignLoan);
        group.MapPut("/unassignLoan", UnassignLoan);

    group.MapPut("/addToPayments", AddToPayments);
    group.MapPut("/removeFromPayments", RemoveFromPayments);


        return app;
    }

    private static async Task<IResult> Create(
        RepaymentScheduleRequest request,
        IRepaymentScheduleService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToRepaymentSchedule( request );

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
        RepaymentScheduleRequest request,
        IRepaymentScheduleService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToRepaymentSchedule( request );

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
        IRepaymentScheduleService service,
        CancellationToken cancellationToken) {

        var repaymentSchedule = await service.Get(identifier, cancellationToken);
        return repaymentSchedule is null ? Results.NotFound() : Results.Ok( repaymentSchedule );
    }


    private static async Task<IResult> GetAll(
        IRepaymentScheduleService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( RepaymentScheduleResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IRepaymentScheduleService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignLoan(
        AssociationRequest request,
        IRepaymentScheduleService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignLoan(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignLoan(
    AssociationRequest request,
    IRepaymentScheduleService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignLoan(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToPayments(
        MultipleAssociationRequest request,
        IRepaymentScheduleService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPayments(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPayments(
        MultipleAssociationRequest request,
        IRepaymentScheduleService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPayments(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static RepaymentSchedule mapRequestToRepaymentSchedule( RepaymentScheduleRequest request ) {
        var model = new RepaymentSchedule
        {
            Id = request.Id,
            InstallmentNumber = request.InstallmentNumber,
            DueDate = request.DueDate,
            AmountDue = request.AmountDue,
            PrincipalDue = request.PrincipalDue,
            InterestDue = request.InterestDue,
            Status = request.Status,
        };
        return model;
    }

}
