using fintechonaspdotnet.Domain;

namespace fintechonaspdotnet.Contracts;

public class IdentifierRequest
{
    public Guid Id { get; set; }
}

public class AssociationRequest
{
    public Guid ParentId { get; set; }
    public Guid ChildId { get; set; }
}

public class MultipleAssociationRequest
{
    public Guid ParentId { get; set; }
    public List<Guid> ChildIds { get; set; } = new();
}

public class FinancialInstitutionRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? LegalName { get; set; } 
 public virtual string? CountryOfIncorporation { get; set; } 
 public virtual BIC? Bic { get; set; } 
 public virtual string? Website { get; set; } 
}

public class FinancialInstitutionResponse : FinancialInstitutionRequest {
    public static FinancialInstitutionResponse FromModel(FinancialInstitution model) {
        return new FinancialInstitutionResponse {
            Id = model.Id,
            Name = model.Name,
            LegalName = model.LegalName,
            CountryOfIncorporation = model.CountryOfIncorporation,
            Bic = model.Bic,
            Website = model.Website,
        };
    }
}

public class BranchRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? BranchCode { get; set; } 
 public virtual Address? Address { get; set; } 
}

public class BranchResponse : BranchRequest {
    public static BranchResponse FromModel(Branch model) {
        return new BranchResponse {
            Id = model.Id,
            Name = model.Name,
            BranchCode = model.BranchCode,
            Address = model.Address,
        };
    }
}

public class ProductOfferingRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? ProductCode { get; set; } 
 public virtual ProductCategory? Category { get; set; } 
}

public class ProductOfferingResponse : ProductOfferingRequest {
    public static ProductOfferingResponse FromModel(ProductOffering model) {
        return new ProductOfferingResponse {
            Id = model.Id,
            Name = model.Name,
            ProductCode = model.ProductCode,
            Category = model.Category,
        };
    }
}

public class PricingPlanRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? PlanCode { get; set; } 
 public virtual string? BaseCurrency { get; set; } 
 public virtual PlanStatus? Status { get; set; } 
}

public class PricingPlanResponse : PricingPlanRequest {
    public static PricingPlanResponse FromModel(PricingPlan model) {
        return new PricingPlanResponse {
            Id = model.Id,
            Name = model.Name,
            PlanCode = model.PlanCode,
            BaseCurrency = model.BaseCurrency,
            Status = model.Status,
        };
    }
}

public class FeeScheduleRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual Money? Amount { get; set; } 
 public virtual decimal? Percentage { get; set; } 
 public virtual Money? Minimum { get; set; } 
 public virtual Money? Maximum { get; set; } 
 public virtual FeeType? FeeType { get; set; } 
 public virtual FeeCalculationMethod? CalculationMethod { get; set; } 
}

public class FeeScheduleResponse : FeeScheduleRequest {
    public static FeeScheduleResponse FromModel(FeeSchedule model) {
        return new FeeScheduleResponse {
            Id = model.Id,
            Name = model.Name,
            Amount = model.Amount,
            Percentage = model.Percentage,
            Minimum = model.Minimum,
            Maximum = model.Maximum,
            FeeType = model.FeeType,
            CalculationMethod = model.CalculationMethod,
        };
    }
}

public class UsageLimitRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual Money? Amount { get; set; } 
 public virtual int? Count { get; set; } 
 public virtual LimitScope? Scope { get; set; } 
 public virtual LimitPeriod? Period { get; set; } 
}

public class UsageLimitResponse : UsageLimitRequest {
    public static UsageLimitResponse FromModel(UsageLimit model) {
        return new UsageLimitResponse {
            Id = model.Id,
            Name = model.Name,
            Amount = model.Amount,
            Count = model.Count,
            Scope = model.Scope,
            Period = model.Period,
        };
    }
}

public class CustomerRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? FirstName { get; set; } 
 public virtual string? LastName { get; set; } 
 public virtual DateOnly? DateOfBirth { get; set; } 
 public virtual Email? Email { get; set; } 
 public virtual PhoneNumber? Phone { get; set; } 
 public virtual Address? Address { get; set; } 
 public virtual TaxId? TaxId { get; set; } 
 public virtual RiskScore? RiskScore { get; set; } 
 public virtual CustomerType? CustomerType { get; set; } 
}

public class CustomerResponse : CustomerRequest {
    public static CustomerResponse FromModel(Customer model) {
        return new CustomerResponse {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            DateOfBirth = model.DateOfBirth,
            Email = model.Email,
            Phone = model.Phone,
            Address = model.Address,
            TaxId = model.TaxId,
            RiskScore = model.RiskScore,
            CustomerType = model.CustomerType,
        };
    }
}

public class KYCProfileRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? ProfileId { get; set; } 
 public virtual DateTime? CreatedAt { get; set; } 
 public virtual KYCStatus? Status { get; set; } 
 public virtual VerificationLevel? VerificationLevel { get; set; } 
}

public class KYCProfileResponse : KYCProfileRequest {
    public static KYCProfileResponse FromModel(KYCProfile model) {
        return new KYCProfileResponse {
            Id = model.Id,
            ProfileId = model.ProfileId,
            CreatedAt = model.CreatedAt,
            Status = model.Status,
            VerificationLevel = model.VerificationLevel,
        };
    }
}

public class KYCDocumentRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual DocumentReference? Reference { get; set; } 
 public virtual string? IssuedCountry { get; set; } 
 public virtual DateOnly? ExpirationDate { get; set; } 
 public virtual KYCDocumentType? DocumentType { get; set; } 
 public virtual DocumentStatus? Status { get; set; } 
}

public class KYCDocumentResponse : KYCDocumentRequest {
    public static KYCDocumentResponse FromModel(KYCDocument model) {
        return new KYCDocumentResponse {
            Id = model.Id,
            Reference = model.Reference,
            IssuedCountry = model.IssuedCountry,
            ExpirationDate = model.ExpirationDate,
            DocumentType = model.DocumentType,
            Status = model.Status,
        };
    }
}

public class ScreeningRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual RiskScore? Score { get; set; } 
 public virtual DateTime? ScreenedAt { get; set; } 
 public virtual ScreeningType? ScreeningType { get; set; } 
 public virtual ScreeningStatus? Status { get; set; } 
}

public class ScreeningResponse : ScreeningRequest {
    public static ScreeningResponse FromModel(Screening model) {
        return new ScreeningResponse {
            Id = model.Id,
            Score = model.Score,
            ScreenedAt = model.ScreenedAt,
            ScreeningType = model.ScreeningType,
            Status = model.Status,
        };
    }
}

public class VerifiedAddressRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual Address? Address { get; set; } 
 public virtual DateTime? VerifiedAt { get; set; } 
 public virtual VerificationStatus? VerificationStatus { get; set; } 
}

public class VerifiedAddressResponse : VerifiedAddressRequest {
    public static VerifiedAddressResponse FromModel(VerifiedAddress model) {
        return new VerifiedAddressResponse {
            Id = model.Id,
            Address = model.Address,
            VerifiedAt = model.VerifiedAt,
            VerificationStatus = model.VerificationStatus,
        };
    }
}

public class CompliancePolicyRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? PolicyCode { get; set; } 
 public virtual string? Description { get; set; } 
 public virtual PolicyStatus? Status { get; set; } 
}

public class CompliancePolicyResponse : CompliancePolicyRequest {
    public static CompliancePolicyResponse FromModel(CompliancePolicy model) {
        return new CompliancePolicyResponse {
            Id = model.Id,
            Name = model.Name,
            PolicyCode = model.PolicyCode,
            Description = model.Description,
            Status = model.Status,
        };
    }
}

public class ComplianceAlertRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? AlertCode { get; set; } 
 public virtual DateTime? RaisedAt { get; set; } 
 public virtual string? Notes { get; set; } 
 public virtual AlertSeverity? Severity { get; set; } 
 public virtual AlertStatus? Status { get; set; } 
}

public class ComplianceAlertResponse : ComplianceAlertRequest {
    public static ComplianceAlertResponse FromModel(ComplianceAlert model) {
        return new ComplianceAlertResponse {
            Id = model.Id,
            AlertCode = model.AlertCode,
            RaisedAt = model.RaisedAt,
            Notes = model.Notes,
            Severity = model.Severity,
            Status = model.Status,
        };
    }
}

public class ConsentRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual DateTime? GrantedAt { get; set; } 
 public virtual DateTime? ExpiresAt { get; set; } 
 public virtual string? Scope { get; set; } 
 public virtual ConsentType? ConsentType { get; set; } 
 public virtual ConsentStatus? Status { get; set; } 
}

public class ConsentResponse : ConsentRequest {
    public static ConsentResponse FromModel(Consent model) {
        return new ConsentResponse {
            Id = model.Id,
            GrantedAt = model.GrantedAt,
            ExpiresAt = model.ExpiresAt,
            Scope = model.Scope,
            ConsentType = model.ConsentType,
            Status = model.Status,
        };
    }
}

public class APIClientRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? ClientId { get; set; } 
 public virtual string? RedirectUri { get; set; } 
 public virtual ClientType? ClientType { get; set; } 
}

public class APIClientResponse : APIClientRequest {
    public static APIClientResponse FromModel(APIClient model) {
        return new APIClientResponse {
            Id = model.Id,
            Name = model.Name,
            ClientId = model.ClientId,
            RedirectUri = model.RedirectUri,
            ClientType = model.ClientType,
        };
    }
}

public class AgreementRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? AgreementNumber { get; set; } 
 public virtual DateOnly? EffectiveDate { get; set; } 
 public virtual AgreementType? AgreementType { get; set; } 
 public virtual AgreementStatus? Status { get; set; } 
}

public class AgreementResponse : AgreementRequest {
    public static AgreementResponse FromModel(Agreement model) {
        return new AgreementResponse {
            Id = model.Id,
            AgreementNumber = model.AgreementNumber,
            EffectiveDate = model.EffectiveDate,
            AgreementType = model.AgreementType,
            Status = model.Status,
        };
    }
}

public class AccountRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual AccountNumber? AccountNumber { get; set; } 
 public virtual IBAN? Iban { get; set; } 
 public virtual BIC? Bic { get; set; } 
 public virtual DateOnly? OpenedDate { get; set; } 
 public virtual string? Currency { get; set; } 
 public virtual Money? Balance { get; set; } 
 public virtual Money? AvailableBalance { get; set; } 
 public virtual AccountType? AccountType { get; set; } 
 public virtual AccountStatus? Status { get; set; } 
}

public class AccountResponse : AccountRequest {
    public static AccountResponse FromModel(Account model) {
        return new AccountResponse {
            Id = model.Id,
            AccountNumber = model.AccountNumber,
            Iban = model.Iban,
            Bic = model.Bic,
            OpenedDate = model.OpenedDate,
            Currency = model.Currency,
            Balance = model.Balance,
            AvailableBalance = model.AvailableBalance,
            AccountType = model.AccountType,
            Status = model.Status,
        };
    }
}

public class WalletRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Currency { get; set; } 
 public virtual Money? Balance { get; set; } 
 public virtual WalletStatus? Status { get; set; } 
}

public class WalletResponse : WalletRequest {
    public static WalletResponse FromModel(Wallet model) {
        return new WalletResponse {
            Id = model.Id,
            Currency = model.Currency,
            Balance = model.Balance,
            Status = model.Status,
        };
    }
}

public class PaymentCardRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual CardNumberToken? CardToken { get; set; } 
 public virtual string? MaskedPan { get; set; } 
 public virtual int? ExpiryMonth { get; set; } 
 public virtual int? ExpiryYear { get; set; } 
 public virtual string? CardholderName { get; set; } 
 public virtual CardScheme? Scheme { get; set; } 
 public virtual CardStatus? Status { get; set; } 
}

public class PaymentCardResponse : PaymentCardRequest {
    public static PaymentCardResponse FromModel(PaymentCard model) {
        return new PaymentCardResponse {
            Id = model.Id,
            CardToken = model.CardToken,
            MaskedPan = model.MaskedPan,
            ExpiryMonth = model.ExpiryMonth,
            ExpiryYear = model.ExpiryYear,
            CardholderName = model.CardholderName,
            Scheme = model.Scheme,
            Status = model.Status,
        };
    }
}

public class CardTokenizationRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? TokenReference { get; set; } 
 public virtual DateTime? CreatedAt { get; set; } 
 public virtual WalletProvider? WalletProvider { get; set; } 
 public virtual TokenizationStatus? Status { get; set; } 
}

public class CardTokenizationResponse : CardTokenizationRequest {
    public static CardTokenizationResponse FromModel(CardTokenization model) {
        return new CardTokenizationResponse {
            Id = model.Id,
            TokenReference = model.TokenReference,
            CreatedAt = model.CreatedAt,
            WalletProvider = model.WalletProvider,
            Status = model.Status,
        };
    }
}

public class MerchantRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? Mcc { get; set; } 
 public virtual string? Url { get; set; } 
 public virtual string? Country { get; set; } 
 public virtual string? SettlementCurrency { get; set; } 
}

public class MerchantResponse : MerchantRequest {
    public static MerchantResponse FromModel(Merchant model) {
        return new MerchantResponse {
            Id = model.Id,
            Name = model.Name,
            Mcc = model.Mcc,
            Url = model.Url,
            Country = model.Country,
            SettlementCurrency = model.SettlementCurrency,
        };
    }
}

public class TerminalRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual Address? Location { get; set; } 
 public virtual TerminalType? Type { get; set; } 
 public virtual TerminalStatus? Status { get; set; } 
}

public class TerminalResponse : TerminalRequest {
    public static TerminalResponse FromModel(Terminal model) {
        return new TerminalResponse {
            Id = model.Id,
            Location = model.Location,
            Type = model.Type,
            Status = model.Status,
        };
    }
}

public class PaymentContractRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? ContractNumber { get; set; } 
 public virtual string? PricingPlanCode { get; set; } 
 public virtual ContractStatus? Status { get; set; } 
}

public class PaymentContractResponse : PaymentContractRequest {
    public static PaymentContractResponse FromModel(PaymentContract model) {
        return new PaymentContractResponse {
            Id = model.Id,
            ContractNumber = model.ContractNumber,
            PricingPlanCode = model.PricingPlanCode,
            Status = model.Status,
        };
    }
}

public class PaymentProcessorRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? ProcessorCode { get; set; } 
 public virtual string? NetworkSupport { get; set; } 
}

public class PaymentProcessorResponse : PaymentProcessorRequest {
    public static PaymentProcessorResponse FromModel(PaymentProcessor model) {
        return new PaymentProcessorResponse {
            Id = model.Id,
            Name = model.Name,
            ProcessorCode = model.ProcessorCode,
            NetworkSupport = model.NetworkSupport,
        };
    }
}

public class TransactionRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual Money? Amount { get; set; } 
 public virtual Money? Fee { get; set; } 
 public virtual decimal? ExchangeRate { get; set; } 
 public virtual DateTime? CreatedAt { get; set; } 
 public virtual DateTime? CompletedAt { get; set; } 
 public virtual string? Narrative { get; set; } 
 public virtual TransactionType? TransactionType { get; set; } 
 public virtual TransactionStatus? Status { get; set; } 
}

public class TransactionResponse : TransactionRequest {
    public static TransactionResponse FromModel(Transaction model) {
        return new TransactionResponse {
            Id = model.Id,
            Amount = model.Amount,
            Fee = model.Fee,
            ExchangeRate = model.ExchangeRate,
            CreatedAt = model.CreatedAt,
            CompletedAt = model.CompletedAt,
            Narrative = model.Narrative,
            TransactionType = model.TransactionType,
            Status = model.Status,
        };
    }
}

public class PaymentOrderRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? OrderReference { get; set; } 
 public virtual DateOnly? RequestedExecutionDate { get; set; } 
 public virtual string? Purpose { get; set; } 
 public virtual PaymentMethod? PaymentMethod { get; set; } 
 public virtual PaymentOrderStatus? Status { get; set; } 
 public virtual PaymentPriority? Priority { get; set; } 
}

public class PaymentOrderResponse : PaymentOrderRequest {
    public static PaymentOrderResponse FromModel(PaymentOrder model) {
        return new PaymentOrderResponse {
            Id = model.Id,
            OrderReference = model.OrderReference,
            RequestedExecutionDate = model.RequestedExecutionDate,
            Purpose = model.Purpose,
            PaymentMethod = model.PaymentMethod,
            Status = model.Status,
            Priority = model.Priority,
        };
    }
}

public class BeneficiaryRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual AccountIdentifier? AccountIdentifier { get; set; } 
 public virtual IBAN? Iban { get; set; } 
 public virtual BIC? Bic { get; set; } 
 public virtual Address? Address { get; set; } 
}

public class BeneficiaryResponse : BeneficiaryRequest {
    public static BeneficiaryResponse FromModel(Beneficiary model) {
        return new BeneficiaryResponse {
            Id = model.Id,
            Name = model.Name,
            AccountIdentifier = model.AccountIdentifier,
            Iban = model.Iban,
            Bic = model.Bic,
            Address = model.Address,
        };
    }
}

public class AppliedFeeRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual Money? Amount { get; set; } 
 public virtual string? Description { get; set; } 
 public virtual FeeType? FeeType { get; set; } 
}

public class AppliedFeeResponse : AppliedFeeRequest {
    public static AppliedFeeResponse FromModel(AppliedFee model) {
        return new AppliedFeeResponse {
            Id = model.Id,
            Amount = model.Amount,
            Description = model.Description,
            FeeType = model.FeeType,
        };
    }
}

public class FXQuoteRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? BaseCurrency { get; set; } 
 public virtual string? QuoteCurrency { get; set; } 
 public virtual decimal? Rate { get; set; } 
 public virtual DateTime? QuotedAt { get; set; } 
 public virtual DateTime? ExpiresAt { get; set; } 
 public virtual FXPriceType? PriceType { get; set; } 
}

public class FXQuoteResponse : FXQuoteRequest {
    public static FXQuoteResponse FromModel(FXQuote model) {
        return new FXQuoteResponse {
            Id = model.Id,
            BaseCurrency = model.BaseCurrency,
            QuoteCurrency = model.QuoteCurrency,
            Rate = model.Rate,
            QuotedAt = model.QuotedAt,
            ExpiresAt = model.ExpiresAt,
            PriceType = model.PriceType,
        };
    }
}

public class FXDealRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? DealReference { get; set; } 
 public virtual string? BaseCurrency { get; set; } 
 public virtual string? QuoteCurrency { get; set; } 
 public virtual decimal? Rate { get; set; } 
 public virtual Money? Amount { get; set; } 
 public virtual DateOnly? SettlementDate { get; set; } 
 public virtual FXDealStatus? Status { get; set; } 
}

public class FXDealResponse : FXDealRequest {
    public static FXDealResponse FromModel(FXDeal model) {
        return new FXDealResponse {
            Id = model.Id,
            DealReference = model.DealReference,
            BaseCurrency = model.BaseCurrency,
            QuoteCurrency = model.QuoteCurrency,
            Rate = model.Rate,
            Amount = model.Amount,
            SettlementDate = model.SettlementDate,
            Status = model.Status,
        };
    }
}

public class SettlementBatchRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? BatchId { get; set; } 
 public virtual DateTime? PeriodStart { get; set; } 
 public virtual DateTime? PeriodEnd { get; set; } 
 public virtual Money? TotalVolume { get; set; } 
 public virtual int? TotalCount { get; set; } 
 public virtual SettlementStatus? Status { get; set; } 
}

public class SettlementBatchResponse : SettlementBatchRequest {
    public static SettlementBatchResponse FromModel(SettlementBatch model) {
        return new SettlementBatchResponse {
            Id = model.Id,
            BatchId = model.BatchId,
            PeriodStart = model.PeriodStart,
            PeriodEnd = model.PeriodEnd,
            TotalVolume = model.TotalVolume,
            TotalCount = model.TotalCount,
            Status = model.Status,
        };
    }
}

public class PayoutRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? PayoutReference { get; set; } 
 public virtual Money? Amount { get; set; } 
 public virtual string? Currency { get; set; } 
 public virtual DateOnly? ScheduledDate { get; set; } 
 public virtual DateOnly? PaidDate { get; set; } 
 public virtual PayoutStatus? Status { get; set; } 
}

public class PayoutResponse : PayoutRequest {
    public static PayoutResponse FromModel(Payout model) {
        return new PayoutResponse {
            Id = model.Id,
            PayoutReference = model.PayoutReference,
            Amount = model.Amount,
            Currency = model.Currency,
            ScheduledDate = model.ScheduledDate,
            PaidDate = model.PaidDate,
            Status = model.Status,
        };
    }
}

public class DisputeRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? DisputeReference { get; set; } 
 public virtual DateTime? OpenedAt { get; set; } 
 public virtual DateTime? ClosedAt { get; set; } 
 public virtual DisputeReason? Reason { get; set; } 
 public virtual DisputeStatus? Status { get; set; } 
}

public class DisputeResponse : DisputeRequest {
    public static DisputeResponse FromModel(Dispute model) {
        return new DisputeResponse {
            Id = model.Id,
            DisputeReference = model.DisputeReference,
            OpenedAt = model.OpenedAt,
            ClosedAt = model.ClosedAt,
            Reason = model.Reason,
            Status = model.Status,
        };
    }
}

public class ChargebackRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? ChargebackReference { get; set; } 
 public virtual Money? Amount { get; set; } 
 public virtual DateTime? PostedAt { get; set; } 
 public virtual ChargebackStage? Stage { get; set; } 
 public virtual ChargebackStatus? Status { get; set; } 
}

public class ChargebackResponse : ChargebackRequest {
    public static ChargebackResponse FromModel(Chargeback model) {
        return new ChargebackResponse {
            Id = model.Id,
            ChargebackReference = model.ChargebackReference,
            Amount = model.Amount,
            PostedAt = model.PostedAt,
            Stage = model.Stage,
            Status = model.Status,
        };
    }
}

public class InvoiceRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? InvoiceNumber { get; set; } 
 public virtual DateOnly? IssueDate { get; set; } 
 public virtual DateOnly? DueDate { get; set; } 
 public virtual Money? Total { get; set; } 
 public virtual string? Currency { get; set; } 
 public virtual InvoiceStatus? Status { get; set; } 
}

public class InvoiceResponse : InvoiceRequest {
    public static InvoiceResponse FromModel(Invoice model) {
        return new InvoiceResponse {
            Id = model.Id,
            InvoiceNumber = model.InvoiceNumber,
            IssueDate = model.IssueDate,
            DueDate = model.DueDate,
            Total = model.Total,
            Currency = model.Currency,
            Status = model.Status,
        };
    }
}

public class AccountStatementRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? StatementNumber { get; set; } 
 public virtual DateOnly? PeriodStart { get; set; } 
 public virtual DateOnly? PeriodEnd { get; set; } 
 public virtual Money? OpeningBalance { get; set; } 
 public virtual Money? ClosingBalance { get; set; } 
 public virtual DateTime? GeneratedAt { get; set; } 
}

public class AccountStatementResponse : AccountStatementRequest {
    public static AccountStatementResponse FromModel(AccountStatement model) {
        return new AccountStatementResponse {
            Id = model.Id,
            StatementNumber = model.StatementNumber,
            PeriodStart = model.PeriodStart,
            PeriodEnd = model.PeriodEnd,
            OpeningBalance = model.OpeningBalance,
            ClosingBalance = model.ClosingBalance,
            GeneratedAt = model.GeneratedAt,
        };
    }
}

public class DirectDebitMandateRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? MandateId { get; set; } 
 public virtual DateTime? SignedAt { get; set; } 
 public virtual DirectDebitScheme? Scheme { get; set; } 
 public virtual MandateStatus? Status { get; set; } 
}

public class DirectDebitMandateResponse : DirectDebitMandateRequest {
    public static DirectDebitMandateResponse FromModel(DirectDebitMandate model) {
        return new DirectDebitMandateResponse {
            Id = model.Id,
            MandateId = model.MandateId,
            SignedAt = model.SignedAt,
            Scheme = model.Scheme,
            Status = model.Status,
        };
    }
}

public class CreditorRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual BIC? Bic { get; set; } 
 public virtual Address? Address { get; set; } 
}

public class CreditorResponse : CreditorRequest {
    public static CreditorResponse FromModel(Creditor model) {
        return new CreditorResponse {
            Id = model.Id,
            Name = model.Name,
            Bic = model.Bic,
            Address = model.Address,
        };
    }
}

public class LoanApplicationRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? ApplicationNumber { get; set; } 
 public virtual Money? AmountRequested { get; set; } 
 public virtual int? TermMonths { get; set; } 
 public virtual DateTime? SubmittedAt { get; set; } 
 public virtual LoanProductType? Product { get; set; } 
 public virtual LoanPurpose? Purpose { get; set; } 
 public virtual ApplicationStatus? Status { get; set; } 
}

public class LoanApplicationResponse : LoanApplicationRequest {
    public static LoanApplicationResponse FromModel(LoanApplication model) {
        return new LoanApplicationResponse {
            Id = model.Id,
            ApplicationNumber = model.ApplicationNumber,
            AmountRequested = model.AmountRequested,
            TermMonths = model.TermMonths,
            SubmittedAt = model.SubmittedAt,
            Product = model.Product,
            Purpose = model.Purpose,
            Status = model.Status,
        };
    }
}

public class RiskAssessmentRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual RiskScore? Score { get; set; } 
 public virtual DateTime? AssessedAt { get; set; } 
 public virtual string? ModelVersion { get; set; } 
 public virtual string? Notes { get; set; } 
 public virtual DecisionOutcome? Decision { get; set; } 
}

public class RiskAssessmentResponse : RiskAssessmentRequest {
    public static RiskAssessmentResponse FromModel(RiskAssessment model) {
        return new RiskAssessmentResponse {
            Id = model.Id,
            Score = model.Score,
            AssessedAt = model.AssessedAt,
            ModelVersion = model.ModelVersion,
            Notes = model.Notes,
            Decision = model.Decision,
        };
    }
}

public class LoanRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? LoanNumber { get; set; } 
 public virtual Money? Principal { get; set; } 
 public virtual decimal? InterestRate { get; set; } 
 public virtual DateOnly? OriginationDate { get; set; } 
 public virtual DateOnly? MaturityDate { get; set; } 
 public virtual InterestRateType? RateType { get; set; } 
 public virtual LoanStatus? Status { get; set; } 
}

public class LoanResponse : LoanRequest {
    public static LoanResponse FromModel(Loan model) {
        return new LoanResponse {
            Id = model.Id,
            LoanNumber = model.LoanNumber,
            Principal = model.Principal,
            InterestRate = model.InterestRate,
            OriginationDate = model.OriginationDate,
            MaturityDate = model.MaturityDate,
            RateType = model.RateType,
            Status = model.Status,
        };
    }
}

public class RepaymentScheduleRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual int? InstallmentNumber { get; set; } 
 public virtual DateOnly? DueDate { get; set; } 
 public virtual Money? AmountDue { get; set; } 
 public virtual Money? PrincipalDue { get; set; } 
 public virtual Money? InterestDue { get; set; } 
 public virtual InstallmentStatus? Status { get; set; } 
}

public class RepaymentScheduleResponse : RepaymentScheduleRequest {
    public static RepaymentScheduleResponse FromModel(RepaymentSchedule model) {
        return new RepaymentScheduleResponse {
            Id = model.Id,
            InstallmentNumber = model.InstallmentNumber,
            DueDate = model.DueDate,
            AmountDue = model.AmountDue,
            PrincipalDue = model.PrincipalDue,
            InterestDue = model.InterestDue,
            Status = model.Status,
        };
    }
}

public class CollateralRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Description { get; set; } 
 public virtual Money? Value { get; set; } 
 public virtual CollateralType? CollateralType { get; set; } 
}

public class CollateralResponse : CollateralRequest {
    public static CollateralResponse FromModel(Collateral model) {
        return new CollateralResponse {
            Id = model.Id,
            Description = model.Description,
            Value = model.Value,
            CollateralType = model.CollateralType,
        };
    }
}

public class LoanTransactionRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual TransactionId? TransactionId { get; set; } 
 public virtual Money? Amount { get; set; } 
 public virtual DateOnly? PostingDate { get; set; } 
 public virtual LoanTransactionType? Type { get; set; } 
 public virtual PostingStatus? Status { get; set; } 
}

public class LoanTransactionResponse : LoanTransactionRequest {
    public static LoanTransactionResponse FromModel(LoanTransaction model) {
        return new LoanTransactionResponse {
            Id = model.Id,
            TransactionId = model.TransactionId,
            Amount = model.Amount,
            PostingDate = model.PostingDate,
            Type = model.Type,
            Status = model.Status,
        };
    }
}

public class InvestmentPortfolioRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? PortfolioCode { get; set; } 
 public virtual string? BaseCurrency { get; set; } 
 public virtual DateTime? CreatedAt { get; set; } 
 public virtual PortfolioStatus? Status { get; set; } 
}

public class InvestmentPortfolioResponse : InvestmentPortfolioRequest {
    public static InvestmentPortfolioResponse FromModel(InvestmentPortfolio model) {
        return new InvestmentPortfolioResponse {
            Id = model.Id,
            PortfolioCode = model.PortfolioCode,
            BaseCurrency = model.BaseCurrency,
            CreatedAt = model.CreatedAt,
            Status = model.Status,
        };
    }
}

public class InvestmentAccountRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual AccountNumber? AccountNumber { get; set; } 
 public virtual string? BaseCurrency { get; set; } 
 public virtual Money? Balance { get; set; } 
 public virtual InvestmentAccountType? AccountType { get; set; } 
 public virtual AccountStatus? Status { get; set; } 
}

public class InvestmentAccountResponse : InvestmentAccountRequest {
    public static InvestmentAccountResponse FromModel(InvestmentAccount model) {
        return new InvestmentAccountResponse {
            Id = model.Id,
            AccountNumber = model.AccountNumber,
            BaseCurrency = model.BaseCurrency,
            Balance = model.Balance,
            AccountType = model.AccountType,
            Status = model.Status,
        };
    }
}

public class SecurityRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Symbol { get; set; } 
 public virtual string? Isin { get; set; } 
 public virtual string? Cusip { get; set; } 
 public virtual string? Currency { get; set; } 
 public virtual SecurityType? SecurityType { get; set; } 
}

public class SecurityResponse : SecurityRequest {
    public static SecurityResponse FromModel(Security model) {
        return new SecurityResponse {
            Id = model.Id,
            Symbol = model.Symbol,
            Isin = model.Isin,
            Cusip = model.Cusip,
            Currency = model.Currency,
            SecurityType = model.SecurityType,
        };
    }
}

public class PositionRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual decimal? Quantity { get; set; } 
 public virtual Money? AverageCost { get; set; } 
 public virtual Money? MarketValue { get; set; } 
}

public class PositionResponse : PositionRequest {
    public static PositionResponse FromModel(Position model) {
        return new PositionResponse {
            Id = model.Id,
            Quantity = model.Quantity,
            AverageCost = model.AverageCost,
            MarketValue = model.MarketValue,
        };
    }
}

public class TradeOrderRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? OrderId { get; set; } 
 public virtual decimal? Quantity { get; set; } 
 public virtual Money? LimitPrice { get; set; } 
 public virtual DateTime? PlacedAt { get; set; } 
 public virtual OrderSide? Side { get; set; } 
 public virtual OrderType? Type { get; set; } 
 public virtual OrderStatus? Status { get; set; } 
 public virtual TimeInForce? TimeInForce { get; set; } 
}

public class TradeOrderResponse : TradeOrderRequest {
    public static TradeOrderResponse FromModel(TradeOrder model) {
        return new TradeOrderResponse {
            Id = model.Id,
            OrderId = model.OrderId,
            Quantity = model.Quantity,
            LimitPrice = model.LimitPrice,
            PlacedAt = model.PlacedAt,
            Side = model.Side,
            Type = model.Type,
            Status = model.Status,
            TimeInForce = model.TimeInForce,
        };
    }
}

public class TradeRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual DateTime? ExecutedAt { get; set; } 
 public virtual decimal? Quantity { get; set; } 
 public virtual Money? Price { get; set; } 
 public virtual Money? Fees { get; set; } 
 public virtual DateOnly? SettlementDate { get; set; } 
}

public class TradeResponse : TradeRequest {
    public static TradeResponse FromModel(Trade model) {
        return new TradeResponse {
            Id = model.Id,
            ExecutedAt = model.ExecutedAt,
            Quantity = model.Quantity,
            Price = model.Price,
            Fees = model.Fees,
            SettlementDate = model.SettlementDate,
        };
    }
}

public class ExchangeRateRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? BaseCurrency { get; set; } 
 public virtual string? QuoteCurrency { get; set; } 
 public virtual decimal? Rate { get; set; } 
 public virtual DateTime? AsOf { get; set; } 
 public virtual string? Source { get; set; } 
}

public class ExchangeRateResponse : ExchangeRateRequest {
    public static ExchangeRateResponse FromModel(ExchangeRate model) {
        return new ExchangeRateResponse {
            Id = model.Id,
            BaseCurrency = model.BaseCurrency,
            QuoteCurrency = model.QuoteCurrency,
            Rate = model.Rate,
            AsOf = model.AsOf,
            Source = model.Source,
        };
    }
}

