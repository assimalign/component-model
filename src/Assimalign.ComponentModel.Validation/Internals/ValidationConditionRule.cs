using System;
using System.Collections;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;


namespace Assimalign.ComponentModel.Validation.Internals
{
    using Assimalign.ComponentModel.Validation.Exceptions;
    using Assimalign.ComponentModel.Validation.Abstraction;
    

    internal sealed class ValidationConditionRule<T> : IValidationConditionRule<T>
    {

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        public IValidationRuleSet ConditionRuleSet { get; } = new ValidationRuleSet();

        /// <summary>
        /// The rule set to run if condition is not valid.
        /// </summary>
        public IValidationRuleSet DefaultRuleSet { get; } = new ValidationRuleSet();

        /// <summary>
        /// 
        /// </summary>
        public Expression<Func<T, bool>> Condition { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Evaluate(IValidationContext context)
        {
            if (context.Instance is T instance)
            {
                if (Condition.Compile().Invoke(instance))
                {
                    Parallel.ForEach(this.ConditionRuleSet, rule =>
                    {
                        rule.Evaluate(context);
                    });
                }
                else if (DefaultRuleSet.Any())
                {
                    Parallel.ForEach(this.DefaultRuleSet, rule =>
                    {
                        rule.Evaluate(context);
                    });
                }
            }
            else
            {
                throw new ValidatorPredicateException("");
            }
        }

        public void Otherwise(Action<IValidationRuleInitializer<T>> configure)
        {
            var initializer = new ValidationRuleInitializer<T>();

            configure.Invoke(initializer);

            DefaultRuleSet.Add(this);

            configure.Invoke(initializer);
        }

        public IValidationConditionRule<T> When(Expression<Func<T, bool>> condition, Action<IValidationRuleInitializer<T>> configure)
        {
            var initializer = new ValidationRuleInitializer<T>();
            var rule = new ValidationConditionRule<T>()
            {
                Condition = condition
            };

            ConditionRuleSet.Add(rule);

            configure.Invoke(initializer);

            return rule;
        }
    }
}
