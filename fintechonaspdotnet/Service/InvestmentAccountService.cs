using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Service;

public interface IInvestmentAccountService {

    Task Create(InvestmentAccount model , CancellationToken cancellationToken);
    Task<bool> Update(InvestmentAccount model, CancellationToken cancellationToken);
    Task<InvestmentAccount?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<InvestmentAccount>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignPortfolio(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignPortfolio(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToTrades(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromTrades(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromOrders(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class InvestmentAccountService : IInvestmentAccountService
{
    private readonly IInvestmentAccountRepository _repository;
    private readonly ILogger<InvestmentAccountService> _logger;

    public InvestmentAccountService(
        IInvestmentAccountRepository repository, ILogger<InvestmentAccountService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(InvestmentAccount model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(InvestmentAccount model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.AccountNumber = model.AccountNumber;
            existing.BaseCurrency = model.BaseCurrency;
            existing.Balance = model.Balance;
            existing.AccountType = model.AccountType;
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

    public Task<InvestmentAccount?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<InvestmentAccount>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignPortfolio(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignPortfolio(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToTrades(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromTrades(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromOrders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
