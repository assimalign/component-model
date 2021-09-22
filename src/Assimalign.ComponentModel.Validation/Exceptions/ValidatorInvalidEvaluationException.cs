using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Exceptions
{
    internal sealed class ValidatorInvalidEvaluationException : ValidatorException
    {
        public ValidatorInvalidEvaluationException(string message, string source = null)
            : base(message)
        {
            base.HResult = HashCode.Combine(nameof(ValidatorInvalidEvaluationException));
            base.ErrorCode = ValidatorErrors.InvalidEvaluation;
            base.Source = source;
        }
    }
}
