
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class LoanApplication
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? LoanapplicationId { get; set; } 
 public virtual string? ApplicationNumber { get; set; } 
 public virtual Money? AmountRequested { get; set; } 
 public virtual int? TermMonths { get; set; } 
 public virtual DateTime? SubmittedAt { get; set; } 
public virtual Customer? Customer { get; set; } 
public virtual RiskAssessment? RiskAssessment { get; set; } 
public virtual Loan? Loan { get; set; } 
 public virtual LoanProductType? Product { get; set; } 
 public virtual LoanPurpose? Purpose { get; set; } 
 public virtual ApplicationStatus? Status { get; set; } 

    public static LoanApplication FromRequest(LoanApplicationRequest request) {
        return new LoanApplication {
            Id = request.Id,
            ApplicationNumber = request.ApplicationNumber,
            AmountRequested = request.AmountRequested,
            TermMonths = request.TermMonths,
            SubmittedAt = request.SubmittedAt,
            Product = request.Product,
            Purpose = request.Purpose,
            Status = request.Status,
        };
    }
}
