using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Exceptions
{
    internal sealed class ValidatorInternalException : ValidatorException
    {

        public ValidatorInternalException(string message) : base(message)
        {
            base.HResult = HashCode.Combine(nameof(ValidatorInvalidEvaluationException));
            base.ErrorCode = ValidatorErrors.UnknownError;
        }
    }
}
