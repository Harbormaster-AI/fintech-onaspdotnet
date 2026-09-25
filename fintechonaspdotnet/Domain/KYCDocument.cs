
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class KYCDocument
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? KycdocumentId { get; set; } 
 public virtual DocumentReference? Reference { get; set; } 
 public virtual string? IssuedCountry { get; set; } 
 public virtual DateOnly? ExpirationDate { get; set; } 
public virtual KYCProfile? KycProfile { get; set; } 
 public virtual KYCDocumentType? DocumentType { get; set; } 
 public virtual DocumentStatus? Status { get; set; } 

    public static KYCDocument FromRequest(KYCDocumentRequest request) {
        return new KYCDocument {
            Id = request.Id,
            Reference = request.Reference,
            IssuedCountry = request.IssuedCountry,
            ExpirationDate = request.ExpirationDate,
            DocumentType = request.DocumentType,
            Status = request.Status,
        };
    }
}
