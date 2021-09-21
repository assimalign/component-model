using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Rules
{
    using Assimalign.ComponentModel.Validation.Exceptions;

    internal sealed class ValidationCondition<T> : IValidationConditionRule<T>
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
        public IEnumerable<IValidationRule> Rules { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Func<T, bool> Condition { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Evaluate(IValidationContext context)
        {
            if (context.ValidationInstance is T instance && Condition.Invoke(instance))
            {
                Parallel.ForEach(this.Rules, rule =>
                {
                    rule.Evaluate(context);
                });
            }
            else
            {
                throw new ValidatorPredicateException("");
            }
        }
    }
}
