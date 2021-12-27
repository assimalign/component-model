using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Internal.Exceptions;

internal sealed class ValidationUnsupportedRuleException : ValidationException
{

    public ValidationUnsupportedRuleException(IValidationRule unsupportedRule)
    {

    }

}

