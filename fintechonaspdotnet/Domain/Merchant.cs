
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class Merchant
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? MerchantId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Mcc { get; set; } 
 public virtual string? Url { get; set; } 
 public virtual string? Country { get; set; } 
 public virtual string? SettlementCurrency { get; set; } 
public virtual ICollection<Terminal> Terminals { get; set; } = new List<Terminal>();
public virtual ICollection<PaymentContract> PaymentContracts { get; set; } = new List<PaymentContract>();
public virtual ICollection<Payout> Payouts { get; set; } = new List<Payout>();
public virtual ICollection<SettlementBatch> Settlements { get; set; } = new List<SettlementBatch>();
public virtual ICollection<Dispute> Disputes { get; set; } = new List<Dispute>();
public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public static Merchant FromRequest(MerchantRequest request) {
        return new Merchant {
            Id = request.Id,
            Name = request.Name,
            Mcc = request.Mcc,
            Url = request.Url,
            Country = request.Country,
            SettlementCurrency = request.SettlementCurrency,
        };
    }
}
