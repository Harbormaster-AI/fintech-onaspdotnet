using System.ComponentModel.DataAnnotations.Schema;

namespace fintechonaspdotnet.Domain;


    [ComplexType]
    public record Money(
    decimal Amount,
    string Currency
    );

    [ComplexType]
    public record Address(
    string Street,
    string City,
    string State,
    string PostalCode,
    string Country
    );

    [ComplexType]
    public record Email(
    string Value
    );

    [ComplexType]
    public record PhoneNumber(
    string Value
    );

    [ComplexType]
    public record TaxId(
    string Value
    );

    [ComplexType]
    public record RiskScore(
    int Value
    );

    [ComplexType]
    public record AccountNumber(
    string Value
    );

    [ComplexType]
    public record IBAN(
    string Value
    );

    [ComplexType]
    public record BIC(
    string Value
    );

    [ComplexType]
    public record AccountIdentifier(
    string Value
    );

    [ComplexType]
    public record TransactionId(
    string Value
    );

    [ComplexType]
    public record CardNumberToken(
    string Value
    );

    [ComplexType]
    public record DocumentReference(
    string Value
    );

    [ComplexType]
    public record DateTime(
    string Value
    );

