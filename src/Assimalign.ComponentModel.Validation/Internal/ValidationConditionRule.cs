using System;
using System.Collections;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;


namespace Assimalign.ComponentModel.Validation.Internal
{
    using Assimalign.ComponentModel.Validation.Exceptions;
    using Assimalign.ComponentModel.Validation.Abstraction;
    

    internal sealed class ValidationConditionRule<T> : IValidationConditionRule<T>
    {

        public ValidationConditionRule()
        {
            this.ConditionDefaultRuleSet ??= new ValidationRuleStack();
        }


        /// <summary>
        /// 
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        public IValidationRuleStack ConditionRuleSet { get; set; } 

        /// <summary>
        /// The rule set to run if condition is not valid.
        /// </summary>
        public IValidationRuleStack ConditionDefaultRuleSet { get; set; } 

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
                if (this.Condition.Compile().Invoke(instance))
                {
                    Parallel.ForEach(this.ConditionRuleSet, rule =>
                    {
                        rule.Evaluate(context);
                    });
                }
                else if (this.ConditionDefaultRuleSet.Any())
                {
                    Parallel.ForEach(this.ConditionDefaultRuleSet, rule =>
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

        public void Otherwise(Action<IValidationRuleDescriptor<T>> configure)
        {
            var descriptor = new ValidationRuleDescriptor<T>();

            configure.Invoke(descriptor);

            ConditionDefaultRuleSet.Push(this);
        }

        public IValidationConditionRule<T> When(Expression<Func<T, bool>> condition, Action<IValidationRuleDescriptor<T>> configure)
        {
            var initializer = new ValidationRuleDescriptor<T>();
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
