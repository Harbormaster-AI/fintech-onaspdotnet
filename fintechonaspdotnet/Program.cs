using fintechonaspdotnet.Api;
using fintechonaspdotnet.Persistence;
using fintechonaspdotnet.Service;
using fintechonaspdotnet.Telemetry;
using OpenTelemetry.Metrics;
using Microsoft.EntityFrameworkCore;
using Oracle.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddOpenApi();
builder.Services.AddHealthChecks()
    .AddDbContextCheck<ApplicationDbContext>();

var dbEngineEnvironment = builder.Configuration["DB_TYPE"];

string dbEngine;
string dbUserName;
string dbPassword;
string dbName;
string dbHost;
string dbPort;

if (string.IsNullOrWhiteSpace(dbEngineEnvironment))
{
    // Generation-time configuration
    dbEngine = "mysql";
    dbUserName = "postgres";
    dbPassword = "no_password";
    dbName = "testDb";
    dbHost = "localhost";
    dbPort = "5432";
}
else
{
    // Runtime environment configuration
    dbEngine = dbEngineEnvironment;
    dbUserName = builder.Configuration["DB_USER_NAME"]
        ?? throw new InvalidOperationException("DB_USER_NAME is required when DB_ENGINE is provided.");

    dbPassword = builder.Configuration["DB_PASSWORD"]
        ?? throw new InvalidOperationException("DB_PASSWORD is required when DB_ENGINE is provided.");

    dbName = builder.Configuration["DB_NAME"]
        ?? throw new InvalidOperationException("DB_NAME is required when DB_ENGINE is provided.");

    dbHost = builder.Configuration["DB_HOST"]
        ?? throw new InvalidOperationException("DB_HOST is required when DB_ENGINE is provided.");

    dbPort = builder.Configuration["DB_PORT"]
        ?? throw new InvalidOperationException("DB_PORT is required when DB_ENGINE is provided.");
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString =
        $"Host={dbHost};Port={dbPort};Database={dbName};Username={dbUserName};Password={dbPassword}";

    if (dbEngine.Equals("mysql", StringComparison.OrdinalIgnoreCase))
    {
        options.UseMySQL(connectionString);
    }
    else if (dbEngine.Equals("mariadb", StringComparison.OrdinalIgnoreCase))
    {
        options.UseMySQL(connectionString);
    }
    else if (dbEngine.Equals("sqlite", StringComparison.OrdinalIgnoreCase))
    {
        options.UseSqlite(connectionString);
    }
    else if (dbEngine.Equals("sqlserver", StringComparison.OrdinalIgnoreCase) ||
             dbEngine.Equals("azuresql", StringComparison.OrdinalIgnoreCase))
    {
        options.UseSqlServer(connectionString);
    }
    else if (dbEngine.Equals("oracle", StringComparison.OrdinalIgnoreCase))
    {
        options.UseOracle(connectionString);
    }
    else if (dbEngine.Equals("mongodb", StringComparison.OrdinalIgnoreCase))
    {
        options.UseMongoDB(connectionString);
    }
    else if (dbEngine.Equals("inmemory", StringComparison.OrdinalIgnoreCase))
    {
        options.UseInMemoryDatabase("fintechonaspdotnet");
    }
    else if (dbEngine.Equals("cosmosdb", StringComparison.OrdinalIgnoreCase))
    {
        var cosmosEndpoint = builder.Configuration["DB_ENDPOINT"]
            ?? throw new InvalidOperationException(
                "DB_ENDPOINT is required for Cosmos DB.");

        var cosmosKey = builder.Configuration["DB_KEY"]
            ?? throw new InvalidOperationException(
                "DB_KEY is required for Cosmos DB.");

        options.UseCosmos(
            cosmosEndpoint,
            cosmosKey,
            dbName);
    }
    else if (dbEngine.Equals("postgres", StringComparison.OrdinalIgnoreCase))
    {
        options.UseNpgsql(connectionString);
    }
    else
    {
        throw new InvalidOperationException(
            "Unsupported or missing database engine configuration.");
    }
    // etc.
});

    builder.Services.AddScoped<IFinancialInstitutionRepository, FinancialInstitutionRepository>();
    builder.Services.AddScoped<IBranchRepository, BranchRepository>();
    builder.Services.AddScoped<IProductOfferingRepository, ProductOfferingRepository>();
    builder.Services.AddScoped<IPricingPlanRepository, PricingPlanRepository>();
    builder.Services.AddScoped<IFeeScheduleRepository, FeeScheduleRepository>();
    builder.Services.AddScoped<IUsageLimitRepository, UsageLimitRepository>();
    builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
    builder.Services.AddScoped<IKYCProfileRepository, KYCProfileRepository>();
    builder.Services.AddScoped<IKYCDocumentRepository, KYCDocumentRepository>();
    builder.Services.AddScoped<IScreeningRepository, ScreeningRepository>();
    builder.Services.AddScoped<IVerifiedAddressRepository, VerifiedAddressRepository>();
    builder.Services.AddScoped<ICompliancePolicyRepository, CompliancePolicyRepository>();
    builder.Services.AddScoped<IComplianceAlertRepository, ComplianceAlertRepository>();
    builder.Services.AddScoped<IConsentRepository, ConsentRepository>();
    builder.Services.AddScoped<IAPIClientRepository, APIClientRepository>();
    builder.Services.AddScoped<IAgreementRepository, AgreementRepository>();
    builder.Services.AddScoped<IAccountRepository, AccountRepository>();
    builder.Services.AddScoped<IWalletRepository, WalletRepository>();
    builder.Services.AddScoped<IPaymentCardRepository, PaymentCardRepository>();
    builder.Services.AddScoped<ICardTokenizationRepository, CardTokenizationRepository>();
    builder.Services.AddScoped<IMerchantRepository, MerchantRepository>();
    builder.Services.AddScoped<ITerminalRepository, TerminalRepository>();
    builder.Services.AddScoped<IPaymentContractRepository, PaymentContractRepository>();
    builder.Services.AddScoped<IPaymentProcessorRepository, PaymentProcessorRepository>();
    builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
    builder.Services.AddScoped<IPaymentOrderRepository, PaymentOrderRepository>();
    builder.Services.AddScoped<IBeneficiaryRepository, BeneficiaryRepository>();
    builder.Services.AddScoped<IAppliedFeeRepository, AppliedFeeRepository>();
    builder.Services.AddScoped<IFXQuoteRepository, FXQuoteRepository>();
    builder.Services.AddScoped<IFXDealRepository, FXDealRepository>();
    builder.Services.AddScoped<ISettlementBatchRepository, SettlementBatchRepository>();
    builder.Services.AddScoped<IPayoutRepository, PayoutRepository>();
    builder.Services.AddScoped<IDisputeRepository, DisputeRepository>();
    builder.Services.AddScoped<IChargebackRepository, ChargebackRepository>();
    builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
    builder.Services.AddScoped<IAccountStatementRepository, AccountStatementRepository>();
    builder.Services.AddScoped<IDirectDebitMandateRepository, DirectDebitMandateRepository>();
    builder.Services.AddScoped<ICreditorRepository, CreditorRepository>();
    builder.Services.AddScoped<ILoanApplicationRepository, LoanApplicationRepository>();
    builder.Services.AddScoped<IRiskAssessmentRepository, RiskAssessmentRepository>();
    builder.Services.AddScoped<ILoanRepository, LoanRepository>();
    builder.Services.AddScoped<IRepaymentScheduleRepository, RepaymentScheduleRepository>();
    builder.Services.AddScoped<ICollateralRepository, CollateralRepository>();
    builder.Services.AddScoped<ILoanTransactionRepository, LoanTransactionRepository>();
    builder.Services.AddScoped<IInvestmentPortfolioRepository, InvestmentPortfolioRepository>();
    builder.Services.AddScoped<IInvestmentAccountRepository, InvestmentAccountRepository>();
    builder.Services.AddScoped<ISecurityRepository, SecurityRepository>();
    builder.Services.AddScoped<IPositionRepository, PositionRepository>();
    builder.Services.AddScoped<ITradeOrderRepository, TradeOrderRepository>();
    builder.Services.AddScoped<ITradeRepository, TradeRepository>();
    builder.Services.AddScoped<IExchangeRateRepository, ExchangeRateRepository>();

    builder.Services.AddScoped<IFinancialInstitutionService, FinancialInstitutionService>();
    builder.Services.AddScoped<IBranchService, BranchService>();
    builder.Services.AddScoped<IProductOfferingService, ProductOfferingService>();
    builder.Services.AddScoped<IPricingPlanService, PricingPlanService>();
    builder.Services.AddScoped<IFeeScheduleService, FeeScheduleService>();
    builder.Services.AddScoped<IUsageLimitService, UsageLimitService>();
    builder.Services.AddScoped<ICustomerService, CustomerService>();
    builder.Services.AddScoped<IKYCProfileService, KYCProfileService>();
    builder.Services.AddScoped<IKYCDocumentService, KYCDocumentService>();
    builder.Services.AddScoped<IScreeningService, ScreeningService>();
    builder.Services.AddScoped<IVerifiedAddressService, VerifiedAddressService>();
    builder.Services.AddScoped<ICompliancePolicyService, CompliancePolicyService>();
    builder.Services.AddScoped<IComplianceAlertService, ComplianceAlertService>();
    builder.Services.AddScoped<IConsentService, ConsentService>();
    builder.Services.AddScoped<IAPIClientService, APIClientService>();
    builder.Services.AddScoped<IAgreementService, AgreementService>();
    builder.Services.AddScoped<IAccountService, AccountService>();
    builder.Services.AddScoped<IWalletService, WalletService>();
    builder.Services.AddScoped<IPaymentCardService, PaymentCardService>();
    builder.Services.AddScoped<ICardTokenizationService, CardTokenizationService>();
    builder.Services.AddScoped<IMerchantService, MerchantService>();
    builder.Services.AddScoped<ITerminalService, TerminalService>();
    builder.Services.AddScoped<IPaymentContractService, PaymentContractService>();
    builder.Services.AddScoped<IPaymentProcessorService, PaymentProcessorService>();
    builder.Services.AddScoped<ITransactionService, TransactionService>();
    builder.Services.AddScoped<IPaymentOrderService, PaymentOrderService>();
    builder.Services.AddScoped<IBeneficiaryService, BeneficiaryService>();
    builder.Services.AddScoped<IAppliedFeeService, AppliedFeeService>();
    builder.Services.AddScoped<IFXQuoteService, FXQuoteService>();
    builder.Services.AddScoped<IFXDealService, FXDealService>();
    builder.Services.AddScoped<ISettlementBatchService, SettlementBatchService>();
    builder.Services.AddScoped<IPayoutService, PayoutService>();
    builder.Services.AddScoped<IDisputeService, DisputeService>();
    builder.Services.AddScoped<IChargebackService, ChargebackService>();
    builder.Services.AddScoped<IInvoiceService, InvoiceService>();
    builder.Services.AddScoped<IAccountStatementService, AccountStatementService>();
    builder.Services.AddScoped<IDirectDebitMandateService, DirectDebitMandateService>();
    builder.Services.AddScoped<ICreditorService, CreditorService>();
    builder.Services.AddScoped<ILoanApplicationService, LoanApplicationService>();
    builder.Services.AddScoped<IRiskAssessmentService, RiskAssessmentService>();
    builder.Services.AddScoped<ILoanService, LoanService>();
    builder.Services.AddScoped<IRepaymentScheduleService, RepaymentScheduleService>();
    builder.Services.AddScoped<ICollateralService, CollateralService>();
    builder.Services.AddScoped<ILoanTransactionService, LoanTransactionService>();
    builder.Services.AddScoped<IInvestmentPortfolioService, InvestmentPortfolioService>();
    builder.Services.AddScoped<IInvestmentAccountService, InvestmentAccountService>();
    builder.Services.AddScoped<ISecurityService, SecurityService>();
    builder.Services.AddScoped<IPositionService, PositionService>();
    builder.Services.AddScoped<ITradeOrderService, TradeOrderService>();
    builder.Services.AddScoped<ITradeService, TradeService>();
    builder.Services.AddScoped<IExchangeRateService, ExchangeRateService>();

// apply the service resolver
builder.Services.AddScoped<IServiceResolver, ServiceResolver>();

builder.Services.AddSingleton<ApplicationTelemetry>();

builder.Services
    .AddOpenTelemetry()
    .WithMetrics(metrics =>
    {
        metrics.AddMeter("Harbormaster.Application");

        metrics.AddPrometheusExporter();
    });

var app = builder.Build();

app.MapPrometheusScrapingEndpoint();


// Health endpoint
app.MapHealthChecks("/health");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    // Testing uses a shared SQLite file; wipe it so unique indexes (e.g. email) don't fail on re-runs.
    if (app.Environment.IsEnvironment("Testing"))
    {
        db.Database.EnsureDeleted();
    }

    // EnsureCreated does not alter an existing schema. If you changed relationships locally,
    // recreate the MySQL database (docker compose down -v && docker compose up -d).
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


    app.MapFinancialInstitutionEndpoints();
    app.MapBranchEndpoints();
    app.MapProductOfferingEndpoints();
    app.MapPricingPlanEndpoints();
    app.MapFeeScheduleEndpoints();
    app.MapUsageLimitEndpoints();
    app.MapCustomerEndpoints();
    app.MapKYCProfileEndpoints();
    app.MapKYCDocumentEndpoints();
    app.MapScreeningEndpoints();
    app.MapVerifiedAddressEndpoints();
    app.MapCompliancePolicyEndpoints();
    app.MapComplianceAlertEndpoints();
    app.MapConsentEndpoints();
    app.MapAPIClientEndpoints();
    app.MapAgreementEndpoints();
    app.MapAccountEndpoints();
    app.MapWalletEndpoints();
    app.MapPaymentCardEndpoints();
    app.MapCardTokenizationEndpoints();
    app.MapMerchantEndpoints();
    app.MapTerminalEndpoints();
    app.MapPaymentContractEndpoints();
    app.MapPaymentProcessorEndpoints();
    app.MapTransactionEndpoints();
    app.MapPaymentOrderEndpoints();
    app.MapBeneficiaryEndpoints();
    app.MapAppliedFeeEndpoints();
    app.MapFXQuoteEndpoints();
    app.MapFXDealEndpoints();
    app.MapSettlementBatchEndpoints();
    app.MapPayoutEndpoints();
    app.MapDisputeEndpoints();
    app.MapChargebackEndpoints();
    app.MapInvoiceEndpoints();
    app.MapAccountStatementEndpoints();
    app.MapDirectDebitMandateEndpoints();
    app.MapCreditorEndpoints();
    app.MapLoanApplicationEndpoints();
    app.MapRiskAssessmentEndpoints();
    app.MapLoanEndpoints();
    app.MapRepaymentScheduleEndpoints();
    app.MapCollateralEndpoints();
    app.MapLoanTransactionEndpoints();
    app.MapInvestmentPortfolioEndpoints();
    app.MapInvestmentAccountEndpoints();
    app.MapSecurityEndpoints();
    app.MapPositionEndpoints();
    app.MapTradeOrderEndpoints();
    app.MapTradeEndpoints();
    app.MapExchangeRateEndpoints();

app.Run();

public partial class Program { }

