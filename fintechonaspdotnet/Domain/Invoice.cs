
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class Invoice
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? InvoiceId { get; set; } 
 public virtual string? InvoiceNumber { get; set; } 
 public virtual DateOnly? IssueDate { get; set; } 
 public virtual DateOnly? DueDate { get; set; } 
 public virtual Money? Total { get; set; } 
 public virtual string? Currency { get; set; } 
public virtual Merchant? Merchant { get; set; } 
public virtual ICollection<PaymentOrder> Payments { get; set; } = new List<PaymentOrder>();
 public virtual InvoiceStatus? Status { get; set; } 

    public static Invoice FromRequest(InvoiceRequest request) {
        return new Invoice {
            Id = request.Id,
            InvoiceNumber = request.InvoiceNumber,
            IssueDate = request.IssueDate,
            DueDate = request.DueDate,
            Total = request.Total,
            Currency = request.Currency,
            Status = request.Status,
        };
    }
}
