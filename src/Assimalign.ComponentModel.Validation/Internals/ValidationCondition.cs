using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Rules
{
    using Assimalign.ComponentModel.Validation.Exceptions;
    using System.Collections;

    internal sealed class ValidationCondition<T> : IValidationConditionRule<T>
    {

        private 


        /// <summary>
        /// 
        /// </summary>
        public string Name { get; }


        /// <summary>
        /// 
        /// </summary>
        public IValidationRuleSet Rules { get; set; }

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

        public IValidationMemberRule<T, TMember> RuleFor<TMember>(Expression<Func<T, TMember>> expression)
        {
            throw new NotImplementedException();
        }

        public IValidationCollectionRule<T, TCollection> RuleForEach<TCollection>(Expression<Func<T, TCollection>> expression) where TCollection : IEnumerable
        {
            throw new NotImplementedException();
        }
    }
}
