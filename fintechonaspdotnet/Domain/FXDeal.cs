
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class FXDeal
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? FxdealId { get; set; } 
 public virtual string? DealReference { get; set; } 
 public virtual string? BaseCurrency { get; set; } 
 public virtual string? QuoteCurrency { get; set; } 
 public virtual decimal? Rate { get; set; } 
 public virtual Money? Amount { get; set; } 
 public virtual DateOnly? SettlementDate { get; set; } 
public virtual FXQuote? Quote { get; set; } 
public virtual ICollection<PaymentOrder> PaymentOrders { get; set; } = new List<PaymentOrder>();
 public virtual FXDealStatus? Status { get; set; } 

    public static FXDeal FromRequest(FXDealRequest request) {
        return new FXDeal {
            Id = request.Id,
            DealReference = request.DealReference,
            BaseCurrency = request.BaseCurrency,
            QuoteCurrency = request.QuoteCurrency,
            Rate = request.Rate,
            Amount = request.Amount,
            SettlementDate = request.SettlementDate,
            Status = request.Status,
        };
    }
}
