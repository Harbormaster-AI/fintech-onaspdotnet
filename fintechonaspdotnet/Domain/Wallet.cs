
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class Wallet
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? WalletId { get; set; } 
 public virtual string? Currency { get; set; } 
 public virtual Money? Balance { get; set; } 
public virtual Customer? Customer { get; set; } 
public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
 public virtual WalletStatus? Status { get; set; } 

    public static Wallet FromRequest(WalletRequest request) {
        return new Wallet {
            Id = request.Id,
            Currency = request.Currency,
            Balance = request.Balance,
            Status = request.Status,
        };
    }
}
