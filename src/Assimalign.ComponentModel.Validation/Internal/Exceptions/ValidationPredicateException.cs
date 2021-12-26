using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Internal.Exceptions;

internal sealed class ValidationPredicateException : ValidationException
{
    public ValidationPredicateException(string message) : base(message)
    {

    }
}

