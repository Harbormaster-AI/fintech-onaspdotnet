
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class CardTokenization
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? CardtokenizationId { get; set; } 
 public virtual string? TokenReference { get; set; } 
 public virtual DateTime? CreatedAt { get; set; } 
public virtual PaymentCard? Card { get; set; } 
 public virtual WalletProvider? WalletProvider { get; set; } 
 public virtual TokenizationStatus? Status { get; set; } 

    public static CardTokenization FromRequest(CardTokenizationRequest request) {
        return new CardTokenization {
            Id = request.Id,
            TokenReference = request.TokenReference,
            CreatedAt = request.CreatedAt,
            WalletProvider = request.WalletProvider,
            Status = request.Status,
        };
    }
}
