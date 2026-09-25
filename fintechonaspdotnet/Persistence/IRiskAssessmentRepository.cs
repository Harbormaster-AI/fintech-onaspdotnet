using fintechonaspdotnet.Domain;
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Persistence;

public interface IRiskAssessmentRepository
{
    Task<RiskAssessment?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<RiskAssessment>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(RiskAssessment riskAssessment, CancellationToken cancellationToken);
    Task UpdateAsync(RiskAssessment riskAssessment, CancellationToken cancellationToken);
    Task DeleteAsync(RiskAssessment riskAssessment, CancellationToken cancellationToken);


}
