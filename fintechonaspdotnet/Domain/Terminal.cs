
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class Terminal
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? TerminalId { get; set; } 
 public virtual Address? Location { get; set; } 
public virtual Merchant? Merchant { get; set; } 
 public virtual TerminalType? Type { get; set; } 
 public virtual TerminalStatus? Status { get; set; } 

    public static Terminal FromRequest(TerminalRequest request) {
        return new Terminal {
            Id = request.Id,
            Location = request.Location,
            Type = request.Type,
            Status = request.Status,
        };
    }
}
