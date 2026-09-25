
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class FXQuote
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? FxquoteId { get; set; } 
 public virtual string? BaseCurrency { get; set; } 
 public virtual string? QuoteCurrency { get; set; } 
 public virtual decimal? Rate { get; set; } 
 public virtual DateTime? QuotedAt { get; set; } 
 public virtual DateTime? ExpiresAt { get; set; } 
public virtual Customer? RequestedBy { get; set; } 
 public virtual FXPriceType? PriceType { get; set; } 

    public static FXQuote FromRequest(FXQuoteRequest request) {
        return new FXQuote {
            Id = request.Id,
            BaseCurrency = request.BaseCurrency,
            QuoteCurrency = request.QuoteCurrency,
            Rate = request.Rate,
            QuotedAt = request.QuotedAt,
            ExpiresAt = request.ExpiresAt,
            PriceType = request.PriceType,
        };
    }
}
