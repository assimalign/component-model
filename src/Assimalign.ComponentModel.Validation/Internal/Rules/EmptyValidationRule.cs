using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Rules
{


    internal class EmptyValidationRule<T, TValue> : IValidationRule
    {
        private readonly string name;
        private readonly Expression<Func<T, TValue>> expression;

        public EmptyValidationRule(Expression<Func<T, TValue>> expression)
        {
            this.expression = expression;
        }


        public string Name => this.name;

        public void Evaluate(IValidationContext context)
        {
            if (context.Instance is T instance)
            {
                var value = expression.Compile().Invoke(instance);


            }
            else
            {

            }
        }
    }
}
