
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class Trade
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? TradeId { get; set; } 
 public virtual DateTime? ExecutedAt { get; set; } 
 public virtual decimal? Quantity { get; set; } 
 public virtual Money? Price { get; set; } 
 public virtual Money? Fees { get; set; } 
 public virtual DateOnly? SettlementDate { get; set; } 
public virtual TradeOrder? Order { get; set; } 
public virtual Security? Security { get; set; } 
public virtual InvestmentAccount? InvestmentAccount { get; set; } 

    public static Trade FromRequest(TradeRequest request) {
        return new Trade {
            Id = request.Id,
            ExecutedAt = request.ExecutedAt,
            Quantity = request.Quantity,
            Price = request.Price,
            Fees = request.Fees,
            SettlementDate = request.SettlementDate,
        };
    }
}
