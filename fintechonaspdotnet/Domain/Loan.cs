
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class Loan
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? LoanId { get; set; } 
 public virtual string? LoanNumber { get; set; } 
 public virtual Money? Principal { get; set; } 
 public virtual decimal? InterestRate { get; set; } 
 public virtual DateOnly? OriginationDate { get; set; } 
 public virtual DateOnly? MaturityDate { get; set; } 
public virtual Customer? Customer { get; set; } 
public virtual ICollection<RepaymentSchedule> Schedule { get; set; } = new List<RepaymentSchedule>();
public virtual ICollection<Collateral> Collateral { get; set; } = new List<Collateral>();
public virtual ICollection<LoanTransaction> Transactions { get; set; } = new List<LoanTransaction>();
 public virtual InterestRateType? RateType { get; set; } 
 public virtual LoanStatus? Status { get; set; } 

    public static Loan FromRequest(LoanRequest request) {
        return new Loan {
            Id = request.Id,
            LoanNumber = request.LoanNumber,
            Principal = request.Principal,
            InterestRate = request.InterestRate,
            OriginationDate = request.OriginationDate,
            MaturityDate = request.MaturityDate,
            RateType = request.RateType,
            Status = request.Status,
        };
    }
}
