
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class RepaymentSchedule
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? RepaymentscheduleId { get; set; } 
 public virtual int? InstallmentNumber { get; set; } 
 public virtual DateOnly? DueDate { get; set; } 
 public virtual Money? AmountDue { get; set; } 
 public virtual Money? PrincipalDue { get; set; } 
 public virtual Money? InterestDue { get; set; } 
public virtual Loan? Loan { get; set; } 
public virtual ICollection<Transaction> Payments { get; set; } = new List<Transaction>();
 public virtual InstallmentStatus? Status { get; set; } 

    public static RepaymentSchedule FromRequest(RepaymentScheduleRequest request) {
        return new RepaymentSchedule {
            Id = request.Id,
            InstallmentNumber = request.InstallmentNumber,
            DueDate = request.DueDate,
            AmountDue = request.AmountDue,
            PrincipalDue = request.PrincipalDue,
            InterestDue = request.InterestDue,
            Status = request.Status,
        };
    }
}
