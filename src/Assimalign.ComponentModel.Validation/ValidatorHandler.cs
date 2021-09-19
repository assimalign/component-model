using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation
{
    public sealed class ValidatorHandler : IValidatorHandler
    {

        private readonly IDictionary<Type, IValidator> validators;


        public ValidatorHandler()
        {

        }


        public ValidationResults Validate(object instance)
        {

        }
    }
}
