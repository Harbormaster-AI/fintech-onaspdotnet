
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class InvestmentPortfolio
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? InvestmentportfolioId { get; set; } 
 public virtual string? PortfolioCode { get; set; } 
 public virtual string? BaseCurrency { get; set; } 
 public virtual DateTime? CreatedAt { get; set; } 
public virtual Customer? Customer { get; set; } 
public virtual ICollection<InvestmentAccount> Accounts { get; set; } = new List<InvestmentAccount>();
public virtual ICollection<TradeOrder> Orders { get; set; } = new List<TradeOrder>();
public virtual ICollection<Position> Holdings { get; set; } = new List<Position>();
 public virtual PortfolioStatus? Status { get; set; } 

    public static InvestmentPortfolio FromRequest(InvestmentPortfolioRequest request) {
        return new InvestmentPortfolio {
            Id = request.Id,
            PortfolioCode = request.PortfolioCode,
            BaseCurrency = request.BaseCurrency,
            CreatedAt = request.CreatedAt,
            Status = request.Status,
        };
    }
}
