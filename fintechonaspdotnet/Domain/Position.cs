
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class Position
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? PositionId { get; set; } 
 public virtual decimal? Quantity { get; set; } 
 public virtual Money? AverageCost { get; set; } 
 public virtual Money? MarketValue { get; set; } 
public virtual InvestmentPortfolio? Portfolio { get; set; } 
public virtual Security? Security { get; set; } 

    public static Position FromRequest(PositionRequest request) {
        return new Position {
            Id = request.Id,
            Quantity = request.Quantity,
            AverageCost = request.AverageCost,
            MarketValue = request.MarketValue,
        };
    }
}
