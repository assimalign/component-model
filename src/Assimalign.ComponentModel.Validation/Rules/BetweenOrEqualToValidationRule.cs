using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Rules
{
    using Assimalign.ComponentModel.Validation.Abstraction;

    internal sealed class BetweenOrEqualToValidationRule<T, TValue, TLowerBound, TUpperBound> : IValidationRule
        where TLowerBound : struct, IComparable
        where TUpperBound : struct, IComparable
    {
        private readonly TLowerBound lower;
        private readonly TUpperBound upper;
        private readonly Expression<Func<T, TValue>> expression;

        public BetweenOrEqualToValidationRule(Expression<Func<T, TValue>> expression, TLowerBound lower, TUpperBound upper)
        {
            this.lower = lower;
            this.upper = upper;
            this.expression = expression;
        }


        public string Name { get; }

        public void Evaluate(IValidationContext context)
        {


            throw new NotImplementedException();
        }
    }
}
