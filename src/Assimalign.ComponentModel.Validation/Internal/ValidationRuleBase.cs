using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Internal
{
    internal abstract class ValidationRuleBase<T, TValue> : IValidationRule
    {
        public Expression<Func<T, TValue>> Expression { get; set; }

        public abstract string Name { get; }

        public abstract void Evaluate(IValidationContext context);


    }
}
