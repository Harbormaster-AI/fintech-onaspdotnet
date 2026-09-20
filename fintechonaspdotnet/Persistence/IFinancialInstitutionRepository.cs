using fintechonaspdotnet.Domain;

namespace fintechonaspdotnet.Persistence;

public interface IFinancialInstitutionRepository
{
    Task<FinancialInstitution?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<FinancialInstitution>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(FinancialInstitution financialInstitution, CancellationToken cancellationToken);
    Task UpdateAsync(FinancialInstitution financialInstitution, CancellationToken cancellationToken);
    Task DeleteAsync(FinancialInstitution financialInstitution, CancellationToken cancellationToken);
}
