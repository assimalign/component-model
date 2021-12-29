namespace Assimalign.ComponentModel.Validation.Internal.Exceptions;

internal sealed class ValidationUnsupportedRuleException : ValidationException
{
    private const string message = "The following rule is unsupported internally: '{0}'.";

    public ValidationUnsupportedRuleException(IValidationRule unsupportedRule) : base(string.Format(message, unsupportedRule.Name))
    {
        base.Source = unsupportedRule.Name;
        base.ErrorCode = ValidationExceptionCode.UnsupportedValidationRule;
        base.HResult = typeof(ValidationUnsupportedRuleException).GetHashCode();
    }
}

