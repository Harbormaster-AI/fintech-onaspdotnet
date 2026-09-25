using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Persistence;

public interface ICompliancePolicyRepository
{
    Task<CompliancePolicy?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<CompliancePolicy>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(CompliancePolicy compliancePolicy, CancellationToken cancellationToken);
    Task UpdateAsync(CompliancePolicy compliancePolicy, CancellationToken cancellationToken);
    Task DeleteAsync(CompliancePolicy compliancePolicy, CancellationToken cancellationToken);


}
