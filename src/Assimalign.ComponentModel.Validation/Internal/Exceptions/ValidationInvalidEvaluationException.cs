using System;

namespace Assimalign.ComponentModel.Validation.Internal.Exceptions;

internal sealed class ValidationInvalidEvaluationException : ValidationException
{
    public ValidationInvalidEvaluationException(string message, string source = null)
        : base(message)
    {
        base.HResult = HashCode.Combine(nameof(ValidationInvalidEvaluationException));
        base.ErrorCode = ValidationErrors.InvalidEvaluation;
        base.Source = source;
    }
}
