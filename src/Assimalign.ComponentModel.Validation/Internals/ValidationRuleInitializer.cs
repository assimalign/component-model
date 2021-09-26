using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Internals
{
    using Assimalign.ComponentModel.Validation.Abstraction;
    

    internal sealed class ValidationRuleInitializer<T> : IValidationRuleInitializer<T>
    {

        public IValidationRule Current { get; set; }


        public IValidationRuleBuilder<T, TValue> RuleFor<TValue>(Expression<Func<T, TValue>> expression)
        {
            Current = new ValidationMemberRule<T, TValue>()
            {
                Member = expression
            };

            return new ValidationMemberRuleBuilder<T, TValue>((IValidationMemberRule<T, TValue>)Current);
        }

        public IValidationRuleBuilder<T, TValue> RuleForEach<TValue>(Expression<Func<T, TValue>> expression) where TValue : IEnumerable
        {
            Current = new ValidationCollectionRule<T, TValue>()
            {
                Collection = expression
            };

            return new ValidationCollectionRuleBuilder<T, TValue>((IValidationCollectionRule<T, TValue>)Current);
        }

        public IValidationConditionRule<T> When(Expression<Func<T, bool>> condition, Action<IValidationRuleInitializer<T>> configure)
        {
            var initializer = new ValidationRuleInitializer<T>();
            var rule = new ValidationConditionRule<T>()
            {
                Condition = condition
            };

            configure.Invoke(initializer);

            return rule;
        }
    }
}
