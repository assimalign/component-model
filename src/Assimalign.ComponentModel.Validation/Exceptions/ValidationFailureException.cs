using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation;

/// <summary>
/// An Exception that is thrown when a validation failure occurs
/// </summary>
public sealed class ValidationFailureException : ValidationException
{

    internal ValidationFailureException(IValidationContext context)
    {

    }

    internal ValidationFailureException(IEnumerable<IValidationContext> errors)
    {

    }

}

