
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class CustomerEndpoints
{
    public static IEndpointRouteBuilder MapCustomerEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/customer").WithTags("Customers");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignInstitution", AssignInstitution);
        group.MapPut("/unassignInstitution", UnassignInstitution);

    group.MapPut("/addToAccounts", AddToAccounts);
    group.MapPut("/removeFromAccounts", RemoveFromAccounts);

    group.MapPut("/addToWallets", AddToWallets);
    group.MapPut("/removeFromWallets", RemoveFromWallets);

    group.MapPut("/addToCards", AddToCards);
    group.MapPut("/removeFromCards", RemoveFromCards);

    group.MapPut("/addToKycProfiles", AddToKycProfiles);
    group.MapPut("/removeFromKycProfiles", RemoveFromKycProfiles);

    group.MapPut("/addToConsents", AddToConsents);
    group.MapPut("/removeFromConsents", RemoveFromConsents);

    group.MapPut("/addToAgreements", AddToAgreements);
    group.MapPut("/removeFromAgreements", RemoveFromAgreements);

    group.MapPut("/addToLoanApplications", AddToLoanApplications);
    group.MapPut("/removeFromLoanApplications", RemoveFromLoanApplications);

    group.MapPut("/addToLoans", AddToLoans);
    group.MapPut("/removeFromLoans", RemoveFromLoans);

    group.MapPut("/addToPortfolios", AddToPortfolios);
    group.MapPut("/removeFromPortfolios", RemoveFromPortfolios);

    group.MapPut("/addToDisputes", AddToDisputes);
    group.MapPut("/removeFromDisputes", RemoveFromDisputes);


        return app;
    }

    private static async Task<IResult> Create(
        CustomerRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCustomer( request );

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
        CustomerRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCustomer( request );

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
        ICustomerService service,
        CancellationToken cancellationToken) {

        var customer = await service.Get(identifier, cancellationToken);
        return customer is null ? Results.NotFound() : Results.Ok( customer );
    }


    private static async Task<IResult> GetAll(
        ICustomerService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( CustomerResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignInstitution(
        AssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignInstitution(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignInstitution(
    AssociationRequest request,
    ICustomerService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignInstitution(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToAccounts(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAccounts(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAccounts(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAccounts(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToWallets(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToWallets(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromWallets(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromWallets(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToCards(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCards(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCards(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCards(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToKycProfiles(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToKycProfiles(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromKycProfiles(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromKycProfiles(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToConsents(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToConsents(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromConsents(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromConsents(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToAgreements(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAgreements(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAgreements(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAgreements(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToLoanApplications(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToLoanApplications(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromLoanApplications(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromLoanApplications(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToLoans(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToLoans(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromLoans(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromLoans(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToPortfolios(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPortfolios(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPortfolios(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPortfolios(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToDisputes(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDisputes(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDisputes(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDisputes(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Customer mapRequestToCustomer( CustomerRequest request ) {
        var model = new Customer
        {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            Email = request.Email,
            Phone = request.Phone,
            Address = request.Address,
            TaxId = request.TaxId,
            RiskScore = request.RiskScore,
            CustomerType = request.CustomerType,
        };
        return model;
    }

}
