
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class FinancialInstitution
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? FinancialinstitutionId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? LegalName { get; set; } 
 public virtual string? CountryOfIncorporation { get; set; } 
 public virtual BIC? Bic { get; set; } 
 public virtual string? Website { get; set; } 
public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();
public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
public virtual ICollection<ProductOffering> ProductOfferings { get; set; } = new List<ProductOffering>();
public virtual ICollection<PaymentProcessor> PaymentProcessors { get; set; } = new List<PaymentProcessor>();
public virtual ICollection<CompliancePolicy> CompliancePolicies { get; set; } = new List<CompliancePolicy>();

    public static FinancialInstitution FromRequest(FinancialInstitutionRequest request) {
        return new FinancialInstitution {
            Id = request.Id,
            Name = request.Name,
            LegalName = request.LegalName,
            CountryOfIncorporation = request.CountryOfIncorporation,
            Bic = request.Bic,
            Website = request.Website,
        };
    }
}
