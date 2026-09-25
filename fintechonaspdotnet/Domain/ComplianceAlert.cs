
using fintechonaspdotnet.Contracts;

namespace fintechonaspdotnet.Domain;

public class ComplianceAlert
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? CompliancealertId { get; set; } 
 public virtual string? AlertCode { get; set; } 
 public virtual DateTime? RaisedAt { get; set; } 
 public virtual string? Notes { get; set; } 
public virtual Screening? Screening { get; set; } 
public virtual Transaction? Transaction { get; set; } 
 public virtual AlertSeverity? Severity { get; set; } 
 public virtual AlertStatus? Status { get; set; } 

    public static ComplianceAlert FromRequest(ComplianceAlertRequest request) {
        return new ComplianceAlert {
            Id = request.Id,
            AlertCode = request.AlertCode,
            RaisedAt = request.RaisedAt,
            Notes = request.Notes,
            Severity = request.Severity,
            Status = request.Status,
        };
    }
}
