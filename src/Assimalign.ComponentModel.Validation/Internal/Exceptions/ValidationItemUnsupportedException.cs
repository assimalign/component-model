namespace Assimalign.ComponentModel.Validation.Internal.Exceptions;

internal sealed class ValidationItemUnsupportedException : ValidationException
{
    private const string message = "The following validation item is unsupported internally: '{0}'.";

    public ValidationItemUnsupportedException(IValidationItem unsupportedItem) : base(string.Format(message, unsupportedItem.GetType().Name))
    {
        base.Source = unsupportedItem.GetType().Name;
        base.ErrorCode = ValidationExceptionCode.UnsupportedValidationRule;
        base.HResult = typeof(ValidationItemUnsupportedException).GetHashCode();
    }
}