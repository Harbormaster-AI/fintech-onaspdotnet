
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class PaymentCard
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? PaymentcardId { get; set; } 
 public virtual CardNumberToken? CardToken { get; set; } 
 public virtual string? MaskedPan { get; set; } 
 public virtual int? ExpiryMonth { get; set; } 
 public virtual int? ExpiryYear { get; set; } 
 public virtual string? CardholderName { get; set; } 
public virtual Customer? Customer { get; set; } 
public virtual Account? Account { get; set; } 
public virtual ICollection<CardTokenization> Tokenizations { get; set; } = new List<CardTokenization>();
public virtual ICollection<Dispute> Disputes { get; set; } = new List<Dispute>();
 public virtual CardScheme? Scheme { get; set; } 
 public virtual CardStatus? Status { get; set; } 

    public static PaymentCard FromRequest(PaymentCardRequest request) {
        return new PaymentCard {
            Id = request.Id,
            CardToken = request.CardToken,
            MaskedPan = request.MaskedPan,
            ExpiryMonth = request.ExpiryMonth,
            ExpiryYear = request.ExpiryYear,
            CardholderName = request.CardholderName,
            Scheme = request.Scheme,
            Status = request.Status,
        };
    }
}
