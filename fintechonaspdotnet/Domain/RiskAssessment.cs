
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class RiskAssessment
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? RiskassessmentId { get; set; } 
 public virtual RiskScore? Score { get; set; } 
 public virtual DateTime? AssessedAt { get; set; } 
 public virtual string? ModelVersion { get; set; } 
 public virtual string? Notes { get; set; } 
public virtual LoanApplication? Application { get; set; } 
 public virtual DecisionOutcome? Decision { get; set; } 

    public static RiskAssessment FromRequest(RiskAssessmentRequest request) {
        return new RiskAssessment {
            Id = request.Id,
            Score = request.Score,
            AssessedAt = request.AssessedAt,
            ModelVersion = request.ModelVersion,
            Notes = request.Notes,
            Decision = request.Decision,
        };
    }
}
