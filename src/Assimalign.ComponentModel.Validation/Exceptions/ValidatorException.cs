using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Exceptions
{
    public abstract class ValidatorException : Exception
    {
        private const string messageDefault = "";

        public ValidatorException() : base(messageDefault)
        {

        }

        public ValidatorException(string message) : base(message)
        {

        }
    }
}
