
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class TradeOrder
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? TradeorderId { get; set; } 
 public virtual string? OrderId { get; set; } 
 public virtual decimal? Quantity { get; set; } 
 public virtual Money? LimitPrice { get; set; } 
 public virtual DateTime? PlacedAt { get; set; } 
public virtual InvestmentPortfolio? Portfolio { get; set; } 
public virtual Security? Security { get; set; } 
public virtual ICollection<Trade> Trades { get; set; } = new List<Trade>();
 public virtual OrderSide? Side { get; set; } 
 public virtual OrderType? Type { get; set; } 
 public virtual OrderStatus? Status { get; set; } 
 public virtual TimeInForce? TimeInForce { get; set; } 

    public static TradeOrder FromRequest(TradeOrderRequest request) {
        return new TradeOrder {
            Id = request.Id,
            OrderId = request.OrderId,
            Quantity = request.Quantity,
            LimitPrice = request.LimitPrice,
            PlacedAt = request.PlacedAt,
            Side = request.Side,
            Type = request.Type,
            Status = request.Status,
            TimeInForce = request.TimeInForce,
        };
    }
}
