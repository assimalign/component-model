using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Exceptions
{
    internal sealed class ValidatorPredicateException : ValidatorException
    {
        public ValidatorPredicateException(string message) : base(message)
        {

        }
    }
}
