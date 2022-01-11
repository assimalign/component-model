using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Configurable.Internal.Exceptions;


internal class ValidationConfigurableJsonException : ValidationConfigurableException
{
    public ValidationConfigurableJsonException(string message) : base(message)
    {
    }
}

