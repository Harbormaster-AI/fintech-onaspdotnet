using Microsoft.EntityFrameworkCore;

using fintechonaspdotnet.Domain;

namespace fintechonaspdotnet.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<FinancialInstitution> FinancialInstitutions => Set<FinancialInstitution>();
    public DbSet<Branch> Branchs => Set<Branch>();
    public DbSet<ProductOffering> ProductOfferings => Set<ProductOffering>();
    public DbSet<PricingPlan> PricingPlans => Set<PricingPlan>();
    public DbSet<FeeSchedule> FeeSchedules => Set<FeeSchedule>();
    public DbSet<UsageLimit> UsageLimits => Set<UsageLimit>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<KYCProfile> KYCProfiles => Set<KYCProfile>();
    public DbSet<KYCDocument> KYCDocuments => Set<KYCDocument>();
    public DbSet<Screening> Screenings => Set<Screening>();
    public DbSet<VerifiedAddress> VerifiedAddresss => Set<VerifiedAddress>();
    public DbSet<CompliancePolicy> CompliancePolicys => Set<CompliancePolicy>();
    public DbSet<ComplianceAlert> ComplianceAlerts => Set<ComplianceAlert>();
    public DbSet<Consent> Consents => Set<Consent>();
    public DbSet<APIClient> APIClients => Set<APIClient>();
    public DbSet<Agreement> Agreements => Set<Agreement>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Wallet> Wallets => Set<Wallet>();
    public DbSet<PaymentCard> PaymentCards => Set<PaymentCard>();
    public DbSet<CardTokenization> CardTokenizations => Set<CardTokenization>();
    public DbSet<Merchant> Merchants => Set<Merchant>();
    public DbSet<Terminal> Terminals => Set<Terminal>();
    public DbSet<PaymentContract> PaymentContracts => Set<PaymentContract>();
    public DbSet<PaymentProcessor> PaymentProcessors => Set<PaymentProcessor>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<PaymentOrder> PaymentOrders => Set<PaymentOrder>();
    public DbSet<Beneficiary> Beneficiarys => Set<Beneficiary>();
    public DbSet<AppliedFee> AppliedFees => Set<AppliedFee>();
    public DbSet<FXQuote> FXQuotes => Set<FXQuote>();
    public DbSet<FXDeal> FXDeals => Set<FXDeal>();
    public DbSet<SettlementBatch> SettlementBatchs => Set<SettlementBatch>();
    public DbSet<Payout> Payouts => Set<Payout>();
    public DbSet<Dispute> Disputes => Set<Dispute>();
    public DbSet<Chargeback> Chargebacks => Set<Chargeback>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<AccountStatement> AccountStatements => Set<AccountStatement>();
    public DbSet<DirectDebitMandate> DirectDebitMandates => Set<DirectDebitMandate>();
    public DbSet<Creditor> Creditors => Set<Creditor>();
    public DbSet<LoanApplication> LoanApplications => Set<LoanApplication>();
    public DbSet<RiskAssessment> RiskAssessments => Set<RiskAssessment>();
    public DbSet<Loan> Loans => Set<Loan>();
    public DbSet<RepaymentSchedule> RepaymentSchedules => Set<RepaymentSchedule>();
    public DbSet<Collateral> Collaterals => Set<Collateral>();
    public DbSet<LoanTransaction> LoanTransactions => Set<LoanTransaction>();
    public DbSet<InvestmentPortfolio> InvestmentPortfolios => Set<InvestmentPortfolio>();
    public DbSet<InvestmentAccount> InvestmentAccounts => Set<InvestmentAccount>();
    public DbSet<Security> Securitys => Set<Security>();
    public DbSet<Position> Positions => Set<Position>();
    public DbSet<TradeOrder> TradeOrders => Set<TradeOrder>();
    public DbSet<Trade> Trades => Set<Trade>();
    public DbSet<ExchangeRate> ExchangeRates => Set<ExchangeRate>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        // FinancialInstitution has one or more Branches of type Branch
        modelBuilder.Entity<Branch>()
            .HasOne<FinancialInstitution>()
            .WithMany(parent => parent.Branches)
            .HasForeignKey("FinancialInstitution_Id");

        // FinancialInstitution has one or more Customers of type Customer
        modelBuilder.Entity<Customer>()
            .HasOne<FinancialInstitution>()
            .WithMany(parent => parent.Customers)
            .HasForeignKey("FinancialInstitution_Id");

        // FinancialInstitution has one or more ProductOfferings of type ProductOffering
        modelBuilder.Entity<ProductOffering>()
            .HasOne<FinancialInstitution>()
            .WithMany(parent => parent.ProductOfferings)
            .HasForeignKey("FinancialInstitution_Id");

        // FinancialInstitution has one or more PaymentProcessors of type PaymentProcessor
        modelBuilder.Entity<PaymentProcessor>()
            .HasOne<FinancialInstitution>()
            .WithMany(parent => parent.PaymentProcessors)
            .HasForeignKey("FinancialInstitution_Id");

        // FinancialInstitution has one or more CompliancePolicies of type CompliancePolicy
        modelBuilder.Entity<CompliancePolicy>()
            .HasOne<FinancialInstitution>()
            .WithMany(parent => parent.CompliancePolicies)
            .HasForeignKey("FinancialInstitution_Id");

        // Branch has one Institution of type FinancialInstitution
        modelBuilder.Entity<Branch>()
            .HasOne(x => x.Institution)
            .WithMany()
            .HasForeignKey("Institution_Id");


        // ProductOffering has one Institution of type FinancialInstitution
        modelBuilder.Entity<ProductOffering>()
            .HasOne(x => x.Institution)
            .WithMany()
            .HasForeignKey("Institution_Id");


        // ProductOffering has one or more PricingPlans of type PricingPlan
        modelBuilder.Entity<PricingPlan>()
            .HasOne<ProductOffering>()
            .WithMany(parent => parent.PricingPlans)
            .HasForeignKey("ProductOffering_Id");

        // PricingPlan has one ProductOffering of type ProductOffering
        modelBuilder.Entity<PricingPlan>()
            .HasOne(x => x.ProductOffering)
            .WithMany()
            .HasForeignKey("ProductOffering_Id");


        // PricingPlan has one or more FeeSchedules of type FeeSchedule
        modelBuilder.Entity<FeeSchedule>()
            .HasOne<PricingPlan>()
            .WithMany(parent => parent.FeeSchedules)
            .HasForeignKey("PricingPlan_Id");

        // PricingPlan has one or more Limits of type UsageLimit
        modelBuilder.Entity<UsageLimit>()
            .HasOne<PricingPlan>()
            .WithMany(parent => parent.Limits)
            .HasForeignKey("PricingPlan_Id");

        // FeeSchedule has one PricingPlan of type PricingPlan
        modelBuilder.Entity<FeeSchedule>()
            .HasOne(x => x.PricingPlan)
            .WithMany()
            .HasForeignKey("PricingPlan_Id");


        // UsageLimit has one PricingPlan of type PricingPlan
        modelBuilder.Entity<UsageLimit>()
            .HasOne(x => x.PricingPlan)
            .WithMany()
            .HasForeignKey("PricingPlan_Id");


        // Customer has one Institution of type FinancialInstitution
        modelBuilder.Entity<Customer>()
            .HasOne(x => x.Institution)
            .WithMany()
            .HasForeignKey("Institution_Id");


        // Customer has one or more Accounts of type Account
        modelBuilder.Entity<Account>()
            .HasOne<Customer>()
            .WithMany(parent => parent.Accounts)
            .HasForeignKey("Customer_Id");

        // Customer has one or more Wallets of type Wallet
        modelBuilder.Entity<Wallet>()
            .HasOne<Customer>()
            .WithMany(parent => parent.Wallets)
            .HasForeignKey("Customer_Id");

        // Customer has one or more Cards of type PaymentCard
        modelBuilder.Entity<PaymentCard>()
            .HasOne<Customer>()
            .WithMany(parent => parent.Cards)
            .HasForeignKey("Customer_Id");

        // Customer has one or more KycProfiles of type KYCProfile
        modelBuilder.Entity<KYCProfile>()
            .HasOne<Customer>()
            .WithMany(parent => parent.KycProfiles)
            .HasForeignKey("Customer_Id");

        // Customer has one or more Consents of type Consent
        modelBuilder.Entity<Consent>()
            .HasOne<Customer>()
            .WithMany(parent => parent.Consents)
            .HasForeignKey("Customer_Id");

        // Customer has one or more Agreements of type Agreement
        modelBuilder.Entity<Agreement>()
            .HasOne<Customer>()
            .WithMany(parent => parent.Agreements)
            .HasForeignKey("Customer_Id");

        // Customer has one or more LoanApplications of type LoanApplication
        modelBuilder.Entity<LoanApplication>()
            .HasOne<Customer>()
            .WithMany(parent => parent.LoanApplications)
            .HasForeignKey("Customer_Id");

        // Customer has one or more Loans of type Loan
        modelBuilder.Entity<Loan>()
            .HasOne<Customer>()
            .WithMany(parent => parent.Loans)
            .HasForeignKey("Customer_Id");

        // Customer has one or more Portfolios of type InvestmentPortfolio
        modelBuilder.Entity<InvestmentPortfolio>()
            .HasOne<Customer>()
            .WithMany(parent => parent.Portfolios)
            .HasForeignKey("Customer_Id");

        // Customer has one or more Disputes of type Dispute
        modelBuilder.Entity<Dispute>()
            .HasOne<Customer>()
            .WithMany(parent => parent.Disputes)
            .HasForeignKey("Customer_Id");

        // KYCProfile has one Customer of type Customer
        modelBuilder.Entity<KYCProfile>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("Customer_Id");


        // KYCProfile has one or more Documents of type KYCDocument
        modelBuilder.Entity<KYCDocument>()
            .HasOne<KYCProfile>()
            .WithMany(parent => parent.Documents)
            .HasForeignKey("KYCProfile_Id");

        // KYCProfile has one or more Screenings of type Screening
        modelBuilder.Entity<Screening>()
            .HasOne<KYCProfile>()
            .WithMany(parent => parent.Screenings)
            .HasForeignKey("KYCProfile_Id");

        // KYCProfile has one or more Addresses of type VerifiedAddress
        modelBuilder.Entity<VerifiedAddress>()
            .HasOne<KYCProfile>()
            .WithMany(parent => parent.Addresses)
            .HasForeignKey("KYCProfile_Id");

        // KYCDocument has one KycProfile of type KYCProfile
        modelBuilder.Entity<KYCDocument>()
            .HasOne(x => x.KycProfile)
            .WithMany()
            .HasForeignKey("KycProfile_Id");


        // Screening has one KycProfile of type KYCProfile
        modelBuilder.Entity<Screening>()
            .HasOne(x => x.KycProfile)
            .WithMany()
            .HasForeignKey("KycProfile_Id");


        // Screening has one or more Alerts of type ComplianceAlert
        modelBuilder.Entity<ComplianceAlert>()
            .HasOne<Screening>()
            .WithMany(parent => parent.Alerts)
            .HasForeignKey("Screening_Id");

        // VerifiedAddress has one KycProfile of type KYCProfile
        modelBuilder.Entity<VerifiedAddress>()
            .HasOne(x => x.KycProfile)
            .WithMany()
            .HasForeignKey("KycProfile_Id");


        // CompliancePolicy has one Institution of type FinancialInstitution
        modelBuilder.Entity<CompliancePolicy>()
            .HasOne(x => x.Institution)
            .WithMany()
            .HasForeignKey("Institution_Id");


        // ComplianceAlert has one Screening of type Screening
        modelBuilder.Entity<ComplianceAlert>()
            .HasOne(x => x.Screening)
            .WithMany()
            .HasForeignKey("Screening_Id");

        // ComplianceAlert has one Transaction of type Transaction
        modelBuilder.Entity<ComplianceAlert>()
            .HasOne(x => x.Transaction)
            .WithMany()
            .HasForeignKey("Transaction_Id");


        // Consent has one Customer of type Customer
        modelBuilder.Entity<Consent>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("Customer_Id");

        // Consent has one ApiClient of type APIClient
        modelBuilder.Entity<Consent>()
            .HasOne(x => x.ApiClient)
            .WithMany()
            .HasForeignKey("ApiClient_Id");



        // APIClient has one or more Consents of type Consent
        modelBuilder.Entity<Consent>()
            .HasOne<APIClient>()
            .WithMany(parent => parent.Consents)
            .HasForeignKey("APIClient_Id");

        // Agreement has one Customer of type Customer
        modelBuilder.Entity<Agreement>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("Customer_Id");

        // Agreement has one ProductOffering of type ProductOffering
        modelBuilder.Entity<Agreement>()
            .HasOne(x => x.ProductOffering)
            .WithMany()
            .HasForeignKey("ProductOffering_Id");


        // Account has one Customer of type Customer
        modelBuilder.Entity<Account>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("Customer_Id");

        // Account has one Institution of type FinancialInstitution
        modelBuilder.Entity<Account>()
            .HasOne(x => x.Institution)
            .WithMany()
            .HasForeignKey("Institution_Id");


        // Account has one or more Transactions of type Transaction
        modelBuilder.Entity<Transaction>()
            .HasOne<Account>()
            .WithMany(parent => parent.Transactions)
            .HasForeignKey("Account_Id");

        // Account has one or more Cards of type PaymentCard
        modelBuilder.Entity<PaymentCard>()
            .HasOne<Account>()
            .WithMany(parent => parent.Cards)
            .HasForeignKey("Account_Id");

        // Account has one or more Statements of type AccountStatement
        modelBuilder.Entity<AccountStatement>()
            .HasOne<Account>()
            .WithMany(parent => parent.Statements)
            .HasForeignKey("Account_Id");

        // Account has one or more Mandates of type DirectDebitMandate
        modelBuilder.Entity<DirectDebitMandate>()
            .HasOne<Account>()
            .WithMany(parent => parent.Mandates)
            .HasForeignKey("Account_Id");

        // Wallet has one Customer of type Customer
        modelBuilder.Entity<Wallet>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("Customer_Id");


        // Wallet has one or more Transactions of type Transaction
        modelBuilder.Entity<Transaction>()
            .HasOne<Wallet>()
            .WithMany(parent => parent.Transactions)
            .HasForeignKey("Wallet_Id");

        // PaymentCard has one Customer of type Customer
        modelBuilder.Entity<PaymentCard>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("Customer_Id");

        // PaymentCard has one Account of type Account
        modelBuilder.Entity<PaymentCard>()
            .HasOne(x => x.Account)
            .WithMany()
            .HasForeignKey("Account_Id");


        // PaymentCard has one or more Tokenizations of type CardTokenization
        modelBuilder.Entity<CardTokenization>()
            .HasOne<PaymentCard>()
            .WithMany(parent => parent.Tokenizations)
            .HasForeignKey("PaymentCard_Id");

        // PaymentCard has one or more Disputes of type Dispute
        modelBuilder.Entity<Dispute>()
            .HasOne<PaymentCard>()
            .WithMany(parent => parent.Disputes)
            .HasForeignKey("PaymentCard_Id");

        // CardTokenization has one Card of type PaymentCard
        modelBuilder.Entity<CardTokenization>()
            .HasOne(x => x.Card)
            .WithMany()
            .HasForeignKey("Card_Id");



        // Merchant has one or more Terminals of type Terminal
        modelBuilder.Entity<Terminal>()
            .HasOne<Merchant>()
            .WithMany(parent => parent.Terminals)
            .HasForeignKey("Merchant_Id");

        // Merchant has one or more PaymentContracts of type PaymentContract
        modelBuilder.Entity<PaymentContract>()
            .HasOne<Merchant>()
            .WithMany(parent => parent.PaymentContracts)
            .HasForeignKey("Merchant_Id");

        // Merchant has one or more Payouts of type Payout
        modelBuilder.Entity<Payout>()
            .HasOne<Merchant>()
            .WithMany(parent => parent.Payouts)
            .HasForeignKey("Merchant_Id");

        // Merchant has one or more Settlements of type SettlementBatch
        modelBuilder.Entity<SettlementBatch>()
            .HasOne<Merchant>()
            .WithMany(parent => parent.Settlements)
            .HasForeignKey("Merchant_Id");

        // Merchant has one or more Disputes of type Dispute
        modelBuilder.Entity<Dispute>()
            .HasOne<Merchant>()
            .WithMany(parent => parent.Disputes)
            .HasForeignKey("Merchant_Id");

        // Merchant has one or more Invoices of type Invoice
        modelBuilder.Entity<Invoice>()
            .HasOne<Merchant>()
            .WithMany(parent => parent.Invoices)
            .HasForeignKey("Merchant_Id");

        // Terminal has one Merchant of type Merchant
        modelBuilder.Entity<Terminal>()
            .HasOne(x => x.Merchant)
            .WithMany()
            .HasForeignKey("Merchant_Id");


        // PaymentContract has one Merchant of type Merchant
        modelBuilder.Entity<PaymentContract>()
            .HasOne(x => x.Merchant)
            .WithMany()
            .HasForeignKey("Merchant_Id");

        // PaymentContract has one Acquirer of type PaymentProcessor
        modelBuilder.Entity<PaymentContract>()
            .HasOne(x => x.Acquirer)
            .WithMany()
            .HasForeignKey("Acquirer_Id");



        // PaymentProcessor has one or more Institutions of type FinancialInstitution
        modelBuilder.Entity<FinancialInstitution>()
            .HasOne<PaymentProcessor>()
            .WithMany(parent => parent.Institutions)
            .HasForeignKey("PaymentProcessor_Id");

        // PaymentProcessor has one or more Contracts of type PaymentContract
        modelBuilder.Entity<PaymentContract>()
            .HasOne<PaymentProcessor>()
            .WithMany(parent => parent.Contracts)
            .HasForeignKey("PaymentProcessor_Id");

        // PaymentProcessor has one or more Settlements of type SettlementBatch
        modelBuilder.Entity<SettlementBatch>()
            .HasOne<PaymentProcessor>()
            .WithMany(parent => parent.Settlements)
            .HasForeignKey("PaymentProcessor_Id");

        // Transaction has one Account of type Account
        modelBuilder.Entity<Transaction>()
            .HasOne(x => x.Account)
            .WithMany()
            .HasForeignKey("Account_Id");

        // Transaction has one Wallet of type Wallet
        modelBuilder.Entity<Transaction>()
            .HasOne(x => x.Wallet)
            .WithMany()
            .HasForeignKey("Wallet_Id");

        // Transaction has one PaymentOrder of type PaymentOrder
        modelBuilder.Entity<Transaction>()
            .HasOne(x => x.PaymentOrder)
            .WithMany()
            .HasForeignKey("PaymentOrder_Id");

        // Transaction has one Merchant of type Merchant
        modelBuilder.Entity<Transaction>()
            .HasOne(x => x.Merchant)
            .WithMany()
            .HasForeignKey("Merchant_Id");

        // Transaction has one Card of type PaymentCard
        modelBuilder.Entity<Transaction>()
            .HasOne(x => x.Card)
            .WithMany()
            .HasForeignKey("Card_Id");


        // Transaction has one or more RelatedTransactions of type Transaction
        modelBuilder.Entity<Transaction>()
            .HasOne<Transaction>()
            .WithMany(parent => parent.RelatedTransactions)
            .HasForeignKey("Transaction_Id");

        // Transaction has one or more Alerts of type ComplianceAlert
        modelBuilder.Entity<ComplianceAlert>()
            .HasOne<Transaction>()
            .WithMany(parent => parent.Alerts)
            .HasForeignKey("Transaction_Id");

        // PaymentOrder has one SourceAccount of type Account
        modelBuilder.Entity<PaymentOrder>()
            .HasOne(x => x.SourceAccount)
            .WithMany()
            .HasForeignKey("SourceAccount_Id");

        // PaymentOrder has one DestinationAccount of type Account
        modelBuilder.Entity<PaymentOrder>()
            .HasOne(x => x.DestinationAccount)
            .WithMany()
            .HasForeignKey("DestinationAccount_Id");

        // PaymentOrder has one Beneficiary of type Beneficiary
        modelBuilder.Entity<PaymentOrder>()
            .HasOne(x => x.Beneficiary)
            .WithMany()
            .HasForeignKey("Beneficiary_Id");

        // PaymentOrder has one FxDeal of type FXDeal
        modelBuilder.Entity<PaymentOrder>()
            .HasOne(x => x.FxDeal)
            .WithMany()
            .HasForeignKey("FxDeal_Id");


        // PaymentOrder has one or more Transactions of type Transaction
        modelBuilder.Entity<Transaction>()
            .HasOne<PaymentOrder>()
            .WithMany(parent => parent.Transactions)
            .HasForeignKey("PaymentOrder_Id");

        // PaymentOrder has one or more Fees of type AppliedFee
        modelBuilder.Entity<AppliedFee>()
            .HasOne<PaymentOrder>()
            .WithMany(parent => parent.Fees)
            .HasForeignKey("PaymentOrder_Id");

        // Beneficiary has one Customer of type Customer
        modelBuilder.Entity<Beneficiary>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("Customer_Id");


        // AppliedFee has one PaymentOrder of type PaymentOrder
        modelBuilder.Entity<AppliedFee>()
            .HasOne(x => x.PaymentOrder)
            .WithMany()
            .HasForeignKey("PaymentOrder_Id");

        // AppliedFee has one Transaction of type Transaction
        modelBuilder.Entity<AppliedFee>()
            .HasOne(x => x.Transaction)
            .WithMany()
            .HasForeignKey("Transaction_Id");


        // FXQuote has one RequestedBy of type Customer
        modelBuilder.Entity<FXQuote>()
            .HasOne(x => x.RequestedBy)
            .WithMany()
            .HasForeignKey("RequestedBy_Id");


        // FXDeal has one Quote of type FXQuote
        modelBuilder.Entity<FXDeal>()
            .HasOne(x => x.Quote)
            .WithMany()
            .HasForeignKey("Quote_Id");


        // FXDeal has one or more PaymentOrders of type PaymentOrder
        modelBuilder.Entity<PaymentOrder>()
            .HasOne<FXDeal>()
            .WithMany(parent => parent.PaymentOrders)
            .HasForeignKey("FXDeal_Id");

        // SettlementBatch has one Processor of type PaymentProcessor
        modelBuilder.Entity<SettlementBatch>()
            .HasOne(x => x.Processor)
            .WithMany()
            .HasForeignKey("Processor_Id");

        // SettlementBatch has one Merchant of type Merchant
        modelBuilder.Entity<SettlementBatch>()
            .HasOne(x => x.Merchant)
            .WithMany()
            .HasForeignKey("Merchant_Id");


        // SettlementBatch has one or more Payouts of type Payout
        modelBuilder.Entity<Payout>()
            .HasOne<SettlementBatch>()
            .WithMany(parent => parent.Payouts)
            .HasForeignKey("SettlementBatch_Id");

        // SettlementBatch has one or more Transactions of type Transaction
        modelBuilder.Entity<Transaction>()
            .HasOne<SettlementBatch>()
            .WithMany(parent => parent.Transactions)
            .HasForeignKey("SettlementBatch_Id");

        // Payout has one Merchant of type Merchant
        modelBuilder.Entity<Payout>()
            .HasOne(x => x.Merchant)
            .WithMany()
            .HasForeignKey("Merchant_Id");

        // Payout has one SettlementBatch of type SettlementBatch
        modelBuilder.Entity<Payout>()
            .HasOne(x => x.SettlementBatch)
            .WithMany()
            .HasForeignKey("SettlementBatch_Id");

        // Payout has one DestinationAccount of type Account
        modelBuilder.Entity<Payout>()
            .HasOne(x => x.DestinationAccount)
            .WithMany()
            .HasForeignKey("DestinationAccount_Id");


        // Dispute has one Transaction of type Transaction
        modelBuilder.Entity<Dispute>()
            .HasOne(x => x.Transaction)
            .WithMany()
            .HasForeignKey("Transaction_Id");

        // Dispute has one Card of type PaymentCard
        modelBuilder.Entity<Dispute>()
            .HasOne(x => x.Card)
            .WithMany()
            .HasForeignKey("Card_Id");

        // Dispute has one Merchant of type Merchant
        modelBuilder.Entity<Dispute>()
            .HasOne(x => x.Merchant)
            .WithMany()
            .HasForeignKey("Merchant_Id");


        // Dispute has one or more Chargebacks of type Chargeback
        modelBuilder.Entity<Chargeback>()
            .HasOne<Dispute>()
            .WithMany(parent => parent.Chargebacks)
            .HasForeignKey("Dispute_Id");

        // Chargeback has one Dispute of type Dispute
        modelBuilder.Entity<Chargeback>()
            .HasOne(x => x.Dispute)
            .WithMany()
            .HasForeignKey("Dispute_Id");

        // Chargeback has one Transaction of type Transaction
        modelBuilder.Entity<Chargeback>()
            .HasOne(x => x.Transaction)
            .WithMany()
            .HasForeignKey("Transaction_Id");


        // Invoice has one Merchant of type Merchant
        modelBuilder.Entity<Invoice>()
            .HasOne(x => x.Merchant)
            .WithMany()
            .HasForeignKey("Merchant_Id");


        // Invoice has one or more Payments of type PaymentOrder
        modelBuilder.Entity<PaymentOrder>()
            .HasOne<Invoice>()
            .WithMany(parent => parent.Payments)
            .HasForeignKey("Invoice_Id");

        // AccountStatement has one Account of type Account
        modelBuilder.Entity<AccountStatement>()
            .HasOne(x => x.Account)
            .WithMany()
            .HasForeignKey("Account_Id");


        // DirectDebitMandate has one Account of type Account
        modelBuilder.Entity<DirectDebitMandate>()
            .HasOne(x => x.Account)
            .WithMany()
            .HasForeignKey("Account_Id");

        // DirectDebitMandate has one Creditor of type Creditor
        modelBuilder.Entity<DirectDebitMandate>()
            .HasOne(x => x.Creditor)
            .WithMany()
            .HasForeignKey("Creditor_Id");



        // Creditor has one or more Mandates of type DirectDebitMandate
        modelBuilder.Entity<DirectDebitMandate>()
            .HasOne<Creditor>()
            .WithMany(parent => parent.Mandates)
            .HasForeignKey("Creditor_Id");

        // LoanApplication has one Customer of type Customer
        modelBuilder.Entity<LoanApplication>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("Customer_Id");

        // LoanApplication has one RiskAssessment of type RiskAssessment
        modelBuilder.Entity<LoanApplication>()
            .HasOne(x => x.RiskAssessment)
            .WithMany()
            .HasForeignKey("RiskAssessment_Id");

        // LoanApplication has one Loan of type Loan
        modelBuilder.Entity<LoanApplication>()
            .HasOne(x => x.Loan)
            .WithMany()
            .HasForeignKey("Loan_Id");


        // RiskAssessment has one Application of type LoanApplication
        modelBuilder.Entity<RiskAssessment>()
            .HasOne(x => x.Application)
            .WithMany()
            .HasForeignKey("Application_Id");


        // Loan has one Customer of type Customer
        modelBuilder.Entity<Loan>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("Customer_Id");


        // Loan has one or more Schedule of type RepaymentSchedule
        modelBuilder.Entity<RepaymentSchedule>()
            .HasOne<Loan>()
            .WithMany(parent => parent.Schedule)
            .HasForeignKey("Loan_Id");

        // Loan has one or more Collateral of type Collateral
        modelBuilder.Entity<Collateral>()
            .HasOne<Loan>()
            .WithMany(parent => parent.Collateral)
            .HasForeignKey("Loan_Id");

        // Loan has one or more Transactions of type LoanTransaction
        modelBuilder.Entity<LoanTransaction>()
            .HasOne<Loan>()
            .WithMany(parent => parent.Transactions)
            .HasForeignKey("Loan_Id");

        // RepaymentSchedule has one Loan of type Loan
        modelBuilder.Entity<RepaymentSchedule>()
            .HasOne(x => x.Loan)
            .WithMany()
            .HasForeignKey("Loan_Id");


        // RepaymentSchedule has one or more Payments of type Transaction
        modelBuilder.Entity<Transaction>()
            .HasOne<RepaymentSchedule>()
            .WithMany(parent => parent.Payments)
            .HasForeignKey("RepaymentSchedule_Id");

        // Collateral has one Loan of type Loan
        modelBuilder.Entity<Collateral>()
            .HasOne(x => x.Loan)
            .WithMany()
            .HasForeignKey("Loan_Id");


        // LoanTransaction has one Loan of type Loan
        modelBuilder.Entity<LoanTransaction>()
            .HasOne(x => x.Loan)
            .WithMany()
            .HasForeignKey("Loan_Id");


        // InvestmentPortfolio has one Customer of type Customer
        modelBuilder.Entity<InvestmentPortfolio>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("Customer_Id");


        // InvestmentPortfolio has one or more Accounts of type InvestmentAccount
        modelBuilder.Entity<InvestmentAccount>()
            .HasOne<InvestmentPortfolio>()
            .WithMany(parent => parent.Accounts)
            .HasForeignKey("InvestmentPortfolio_Id");

        // InvestmentPortfolio has one or more Orders of type TradeOrder
        modelBuilder.Entity<TradeOrder>()
            .HasOne<InvestmentPortfolio>()
            .WithMany(parent => parent.Orders)
            .HasForeignKey("InvestmentPortfolio_Id");

        // InvestmentPortfolio has one or more Holdings of type Position
        modelBuilder.Entity<Position>()
            .HasOne<InvestmentPortfolio>()
            .WithMany(parent => parent.Holdings)
            .HasForeignKey("InvestmentPortfolio_Id");

        // InvestmentAccount has one Portfolio of type InvestmentPortfolio
        modelBuilder.Entity<InvestmentAccount>()
            .HasOne(x => x.Portfolio)
            .WithMany()
            .HasForeignKey("Portfolio_Id");


        // InvestmentAccount has one or more Trades of type Trade
        modelBuilder.Entity<Trade>()
            .HasOne<InvestmentAccount>()
            .WithMany(parent => parent.Trades)
            .HasForeignKey("InvestmentAccount_Id");

        // InvestmentAccount has one or more Orders of type TradeOrder
        modelBuilder.Entity<TradeOrder>()
            .HasOne<InvestmentAccount>()
            .WithMany(parent => parent.Orders)
            .HasForeignKey("InvestmentAccount_Id");


        // Security has one or more Positions of type Position
        modelBuilder.Entity<Position>()
            .HasOne<Security>()
            .WithMany(parent => parent.Positions)
            .HasForeignKey("Security_Id");

        // Security has one or more Trades of type Trade
        modelBuilder.Entity<Trade>()
            .HasOne<Security>()
            .WithMany(parent => parent.Trades)
            .HasForeignKey("Security_Id");

        // Security has one or more Orders of type TradeOrder
        modelBuilder.Entity<TradeOrder>()
            .HasOne<Security>()
            .WithMany(parent => parent.Orders)
            .HasForeignKey("Security_Id");

        // Position has one Portfolio of type InvestmentPortfolio
        modelBuilder.Entity<Position>()
            .HasOne(x => x.Portfolio)
            .WithMany()
            .HasForeignKey("Portfolio_Id");

        // Position has one Security of type Security
        modelBuilder.Entity<Position>()
            .HasOne(x => x.Security)
            .WithMany()
            .HasForeignKey("Security_Id");


        // TradeOrder has one Portfolio of type InvestmentPortfolio
        modelBuilder.Entity<TradeOrder>()
            .HasOne(x => x.Portfolio)
            .WithMany()
            .HasForeignKey("Portfolio_Id");

        // TradeOrder has one Security of type Security
        modelBuilder.Entity<TradeOrder>()
            .HasOne(x => x.Security)
            .WithMany()
            .HasForeignKey("Security_Id");


        // TradeOrder has one or more Trades of type Trade
        modelBuilder.Entity<Trade>()
            .HasOne<TradeOrder>()
            .WithMany(parent => parent.Trades)
            .HasForeignKey("TradeOrder_Id");

        // Trade has one Order of type TradeOrder
        modelBuilder.Entity<Trade>()
            .HasOne(x => x.Order)
            .WithMany()
            .HasForeignKey("Order_Id");

        // Trade has one Security of type Security
        modelBuilder.Entity<Trade>()
            .HasOne(x => x.Security)
            .WithMany()
            .HasForeignKey("Security_Id");

        // Trade has one InvestmentAccount of type InvestmentAccount
        modelBuilder.Entity<Trade>()
            .HasOne(x => x.InvestmentAccount)
            .WithMany()
            .HasForeignKey("InvestmentAccount_Id");



        // ExchangeRate has one or more UsedByQuotes of type FXQuote
        modelBuilder.Entity<FXQuote>()
            .HasOne<ExchangeRate>()
            .WithMany(parent => parent.UsedByQuotes)
            .HasForeignKey("ExchangeRate_Id");

    }
}
