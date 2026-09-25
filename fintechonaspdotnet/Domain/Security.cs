
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class Security
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? SecurityId { get; set; } 
 public virtual string? Symbol { get; set; } 
 public virtual string? Isin { get; set; } 
 public virtual string? Cusip { get; set; } 
 public virtual string? Currency { get; set; } 
public virtual ICollection<Position> Positions { get; set; } = new List<Position>();
public virtual ICollection<Trade> Trades { get; set; } = new List<Trade>();
public virtual ICollection<TradeOrder> Orders { get; set; } = new List<TradeOrder>();
 public virtual SecurityType? SecurityType { get; set; } 

    public static Security FromRequest(SecurityRequest request) {
        return new Security {
            Id = request.Id,
            Symbol = request.Symbol,
            Isin = request.Isin,
            Cusip = request.Cusip,
            Currency = request.Currency,
            SecurityType = request.SecurityType,
        };
    }
}
