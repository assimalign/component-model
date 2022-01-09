namespace Assimalign.ComponentModel.Validation.Configurable;

internal enum RuleType
{
    EqualTo,
    NotEqualTo,
    Between,
    BetweenOrEqualTo,
    Empty,
    NotEmpty,
    GreaterThan,
    GreaterThanOrEqualTo,
    LessThan,
    LessThanOrEqualTo,
    EmailAddress,
    Null,
    NotNull,
    Length,
    LengthBetween,
    LengthMax,
    LengthMin,
    Child,
    Matches
}