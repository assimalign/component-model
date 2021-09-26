using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Rules
{
    using Assimalign.ComponentModel.Validation.Exceptions;
    using Assimalign.ComponentModel.Validation.Abstraction;

    internal sealed class CustomValidationRule<T, TValue> : IValidationRule, IValidationError
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
        public string Code { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Evaluate(IValidationContext context)
        {
            if (context.Instance is T instance)
            {
                var member = this.expression.Compile().Invoke(instance);

                validation.Invoke(member, context);
            }
            else
            {
                throw new ValidatorInternalException("");
            }
        }
    }
}
