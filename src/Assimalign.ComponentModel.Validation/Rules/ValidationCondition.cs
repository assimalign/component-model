using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Rules
{
    using Assimalign.ComponentModel.Validation.Exceptions;

    internal sealed class ValidationCondition<T> : IValidationCondition<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Validator { get; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        public LambdaExpression Validation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<IValidationRule<T>> Rules { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="instance"></param>
        public void Evaluate(IValidatorContext<T> context, T instance)
        {
            if (Validation is Expression<Func<T, bool>> condition)
            {
                if (condition.Compile().Invoke(instance))
                {
                    Parallel.ForEach(this.Rules, rule =>
                    {
                        rule.Evaluate(context, instance);
                    });
                }
            }
            else
            {
                throw new ValidatorPredicateException("");
            }
        }
    }
}
