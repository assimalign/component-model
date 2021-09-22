using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Rules
{
    using Assimalign.ComponentModel.Validation.Abstraction;


    internal class CustomValidationRule<T, TValue> : IValidationRule
    {
        private readonly Expression<Func<T, TValue>> expression;
        private readonly Action<TValue, IValidationContext> validation;

        public CustomValidationRule(
            Expression<Func<T, TValue>> expression, 
            Action<TValue, IValidationContext> validation)
        {
            this.expression = expression;
            this.validation = validation;
        }

        public string Name { get; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Evaluate(IValidationContext context)
        {
            if (context.ValidationInstance is T instance)
            {
                var member = this.expression.Compile().Invoke(instance);

                validation.Invoke(member, context);
            }
        }
    }
}
