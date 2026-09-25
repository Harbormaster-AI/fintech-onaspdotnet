
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class ExchangeRate
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ExchangerateId { get; set; } 
 public virtual string? BaseCurrency { get; set; } 
 public virtual string? QuoteCurrency { get; set; } 
 public virtual decimal? Rate { get; set; } 
 public virtual DateTime? AsOf { get; set; } 
 public virtual string? Source { get; set; } 
public virtual ICollection<FXQuote> UsedByQuotes { get; set; } = new List<FXQuote>();

    public static ExchangeRate FromRequest(ExchangeRateRequest request) {
        return new ExchangeRate {
            Id = request.Id,
            BaseCurrency = request.BaseCurrency,
            QuoteCurrency = request.QuoteCurrency,
            Rate = request.Rate,
            AsOf = request.AsOf,
            Source = request.Source,
        };
    }
}
