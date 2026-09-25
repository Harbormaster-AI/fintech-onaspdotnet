
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class FinancialInstitutionEndpoints
{
    public static IEndpointRouteBuilder MapFinancialInstitutionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/financialInstitution").WithTags("FinancialInstitutions");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToBranches", AddToBranches);
    group.MapPut("/removeFromBranches", RemoveFromBranches);

    group.MapPut("/addToCustomers", AddToCustomers);
    group.MapPut("/removeFromCustomers", RemoveFromCustomers);

    group.MapPut("/addToProductOfferings", AddToProductOfferings);
    group.MapPut("/removeFromProductOfferings", RemoveFromProductOfferings);

    group.MapPut("/addToPaymentProcessors", AddToPaymentProcessors);
    group.MapPut("/removeFromPaymentProcessors", RemoveFromPaymentProcessors);

    group.MapPut("/addToCompliancePolicies", AddToCompliancePolicies);
    group.MapPut("/removeFromCompliancePolicies", RemoveFromCompliancePolicies);


        return app;
    }

    private static async Task<IResult> Create(
        FinancialInstitutionRequest request,
        IFinancialInstitutionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToFinancialInstitution( request );

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
        FinancialInstitutionRequest request,
        IFinancialInstitutionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToFinancialInstitution( request );

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
        IFinancialInstitutionService service,
        CancellationToken cancellationToken) {

        var financialInstitution = await service.Get(identifier, cancellationToken);
        return financialInstitution is null ? Results.NotFound() : Results.Ok( financialInstitution );
    }


    private static async Task<IResult> GetAll(
        IFinancialInstitutionService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( FinancialInstitutionResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IFinancialInstitutionService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToBranches(
        MultipleAssociationRequest request,
        IFinancialInstitutionService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToBranches(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromBranches(
        MultipleAssociationRequest request,
        IFinancialInstitutionService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromBranches(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToCustomers(
        MultipleAssociationRequest request,
        IFinancialInstitutionService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCustomers(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCustomers(
        MultipleAssociationRequest request,
        IFinancialInstitutionService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCustomers(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToProductOfferings(
        MultipleAssociationRequest request,
        IFinancialInstitutionService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToProductOfferings(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromProductOfferings(
        MultipleAssociationRequest request,
        IFinancialInstitutionService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromProductOfferings(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToPaymentProcessors(
        MultipleAssociationRequest request,
        IFinancialInstitutionService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPaymentProcessors(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPaymentProcessors(
        MultipleAssociationRequest request,
        IFinancialInstitutionService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPaymentProcessors(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToCompliancePolicies(
        MultipleAssociationRequest request,
        IFinancialInstitutionService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCompliancePolicies(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCompliancePolicies(
        MultipleAssociationRequest request,
        IFinancialInstitutionService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCompliancePolicies(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static FinancialInstitution mapRequestToFinancialInstitution( FinancialInstitutionRequest request ) {
        var model = new FinancialInstitution
        {
            Id = request.Id,
            Name = request.Name,
            LegalName = request.LegalName,
            CountryOfIncorporation = request.CountryOfIncorporation,
            Bic = request.Bic,
            Website = request.Website,
        };
        return model;
    }

}
