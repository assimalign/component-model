using System;

namespace Assimalign.ComponentModel.Validation.Internal.Exceptions;

internal sealed class ValidationInternalException : ValidationException
{

    public ValidationInternalException(string message) : base(message)
    {
        base.HResult = HashCode.Combine(nameof(ValidationInvalidEvaluationException));
        base.ErrorCode = ValidationExceptionCode.UnknownError;
    }
}

