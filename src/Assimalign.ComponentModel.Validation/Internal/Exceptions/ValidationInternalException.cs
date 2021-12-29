namespace Assimalign.ComponentModel.Validation.Internal.Exceptions;

internal sealed class ValidationInternalException : ValidationException
{
    public ValidationInternalException(string message) : base(message)
    {
        base.ErrorCode = ValidationExceptionCode.UnknownError;
        base.HResult = nameof(ValidationInternalException).GetHashCode();
    }

    public ValidationInternalException(string message, string source) : this(message)
    {
        base.Source = source;
    }
}

