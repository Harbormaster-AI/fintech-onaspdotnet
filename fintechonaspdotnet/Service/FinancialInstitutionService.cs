
using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;
using fintechonaspdotnet.Telemetry;

namespace fintechonaspdotnet.Service;

public interface IFinancialInstitutionService {

    Task Create(FinancialInstitution model , CancellationToken cancellationToken);
    Task<bool> Update(FinancialInstitution model, CancellationToken cancellationToken);
    Task<FinancialInstitution?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<FinancialInstitution>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToBranches(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromBranches(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToCustomers(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCustomers(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToProductOfferings(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromProductOfferings(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToPaymentProcessors(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPaymentProcessors(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToCompliancePolicies(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCompliancePolicies(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class FinancialInstitutionService : IFinancialInstitutionService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IFinancialInstitutionRepository _repository;
    private readonly ILogger<FinancialInstitutionService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public FinancialInstitutionService(
        ApplicationTelemetry telemetry,
        IFinancialInstitutionRepository repository,
        ILogger<FinancialInstitutionService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(FinancialInstitution model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "FinancialInstitution",
                "CreateFinancialInstitution",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(FinancialInstitution model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.LegalName = model.LegalName;
            existing.CountryOfIncorporation = model.CountryOfIncorporation;
            existing.Bic = model.Bic;
            existing.Website = model.Website;

            await _telemetry.Execute(
                "FinancialInstitution",
                "UpdateFinancialInstitution",
                () => _repository.UpdateAsync(existing, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public Task<FinancialInstitution?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<FinancialInstitution>> GetAll(CancellationToken cancellationToken)
    => _repository.GetAllAsync(cancellationToken);

    public async Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(identifier.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        try
        {
            await _telemetry.Execute(
                "FinancialInstitution",
                "UpdateFinancialInstitution",
                () => _repository.DeleteAsync(existing, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }


    public async Task<bool> AddToBranches(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "FinancialInstitution",
                "AddToBranches",
                () => _repository.AddToBranchesAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
           _logger.LogError(
                   ex,
                   "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> RemoveFromBranches(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "FinancialInstitution",
                "RemoveFromBranches",
                () => _repository.RemoveFromBranchesAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> AddToCustomers(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "FinancialInstitution",
                "AddToCustomers",
                () => _repository.AddToCustomersAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
           _logger.LogError(
                   ex,
                   "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> RemoveFromCustomers(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "FinancialInstitution",
                "RemoveFromCustomers",
                () => _repository.RemoveFromCustomersAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> AddToProductOfferings(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "FinancialInstitution",
                "AddToProductOfferings",
                () => _repository.AddToProductOfferingsAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
           _logger.LogError(
                   ex,
                   "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> RemoveFromProductOfferings(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "FinancialInstitution",
                "RemoveFromProductOfferings",
                () => _repository.RemoveFromProductOfferingsAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> AddToPaymentProcessors(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "FinancialInstitution",
                "AddToPaymentProcessors",
                () => _repository.AddToPaymentProcessorsAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
           _logger.LogError(
                   ex,
                   "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> RemoveFromPaymentProcessors(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "FinancialInstitution",
                "RemoveFromPaymentProcessors",
                () => _repository.RemoveFromPaymentProcessorsAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> AddToCompliancePolicies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "FinancialInstitution",
                "AddToCompliancePolicies",
                () => _repository.AddToCompliancePoliciesAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
           _logger.LogError(
                   ex,
                   "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> RemoveFromCompliancePolicies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "FinancialInstitution",
                "RemoveFromCompliancePolicies",
                () => _repository.RemoveFromCompliancePoliciesAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }



}
