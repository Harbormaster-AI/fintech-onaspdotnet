using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Persistence;

public interface IComplianceAlertRepository
{
    Task<ComplianceAlert?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ComplianceAlert>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ComplianceAlert complianceAlert, CancellationToken cancellationToken);
    Task UpdateAsync(ComplianceAlert complianceAlert, CancellationToken cancellationToken);
    Task DeleteAsync(ComplianceAlert complianceAlert, CancellationToken cancellationToken);


}
