
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Api;

public static class ProductOfferingEndpoints
{
    public static IEndpointRouteBuilder MapProductOfferingEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/productOffering").WithTags("ProductOfferings");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignInstitution", AssignInstitution);
        group.MapPut("/unassignInstitution", UnassignInstitution);

    group.MapPut("/addToPricingPlans", AddToPricingPlans);
    group.MapPut("/removeFromPricingPlans", RemoveFromPricingPlans);


        return app;
    }

    private static async Task<IResult> Create(
        ProductOfferingRequest request,
        IProductOfferingService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToProductOffering( request );

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
        ProductOfferingRequest request,
        IProductOfferingService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToProductOffering( request );

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
        IProductOfferingService service,
        CancellationToken cancellationToken) {

        var productOffering = await service.Get(identifier, cancellationToken);
        return productOffering is null ? Results.NotFound() : Results.Ok( productOffering );
    }


    private static async Task<IResult> GetAll(
        IProductOfferingService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ProductOfferingResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IProductOfferingService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignInstitution(
        AssociationRequest request,
        IProductOfferingService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignInstitution(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignInstitution(
    AssociationRequest request,
    IProductOfferingService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignInstitution(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToPricingPlans(
        MultipleAssociationRequest request,
        IProductOfferingService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPricingPlans(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPricingPlans(
        MultipleAssociationRequest request,
        IProductOfferingService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPricingPlans(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static ProductOffering mapRequestToProductOffering( ProductOfferingRequest request ) {
        var model = new ProductOffering
        {
            Id = request.Id,
            Name = request.Name,
            ProductCode = request.ProductCode,
            Category = request.Category,
        };
        return model;
    }

}
