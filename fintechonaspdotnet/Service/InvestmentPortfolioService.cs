using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Service;

public interface IInvestmentPortfolioService {

    Task Create(InvestmentPortfolio model , CancellationToken cancellationToken);
    Task<bool> Update(InvestmentPortfolio model, CancellationToken cancellationToken);
    Task<InvestmentPortfolio?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<InvestmentPortfolio>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignCustomer(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCustomer(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToAccounts(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromAccounts(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToHoldings(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromHoldings(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class InvestmentPortfolioService : IInvestmentPortfolioService
{
    private readonly IInvestmentPortfolioRepository _repository;
    private readonly ILogger<InvestmentPortfolioService> _logger;

    public InvestmentPortfolioService(
        IInvestmentPortfolioRepository repository, ILogger<InvestmentPortfolioService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(InvestmentPortfolio model, CancellationToken cancellationToken)
    {

         try
        {
            await _repository.AddAsync(model, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
        }
    }

    public async Task<bool> Update(InvestmentPortfolio model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.PortfolioCode = model.PortfolioCode;
            existing.BaseCurrency = model.BaseCurrency;
            existing.CreatedAt = model.CreatedAt;
            existing.Status = model.Status;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<InvestmentPortfolio?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<InvestmentPortfolio>> GetAll(CancellationToken cancellationToken)
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
            await _repository.DeleteAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;

    }

    public async Task<bool> AssignCustomer(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignCustomer(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToAccounts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromAccounts(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToHoldings(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromHoldings(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
