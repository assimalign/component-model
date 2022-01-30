using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Configurable.Internal.Exceptions
{
    internal sealed class ValidationConfigurableJsonInternalException : ValidationConfigurableException
    {
        public ValidationConfigurableJsonInternalException(string message, Exception exception) : base(message, exception)
        {
        }


        public static ValidationConfigurableJsonInternalException FromException (string message, Exception exception) 
        {
            return new ValidationConfigurableJsonInternalException(message, exception);
        }
    }
}
