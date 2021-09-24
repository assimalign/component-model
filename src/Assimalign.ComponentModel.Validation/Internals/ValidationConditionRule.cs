using System;
using System.Collections;
using System.Threading.Tasks;
using System.Linq.Expressions;


namespace Assimalign.ComponentModel.Validation.Internals
{
    using Assimalign.ComponentModel.Validation.Exceptions;
    using Assimalign.ComponentModel.Validation.Abstraction;
    

    internal sealed class ValidationConditionRule<T> : IValidationConditionRule<T>
    {
        private readonly IValidationRuleSet rules = new ValidationRuleSet();


        /// <summary>
        /// 
        /// </summary>
        public string Name { get; }


        /// <summary>
        /// 
        /// </summary>
        public IValidationRuleSet Rules => rules;

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
            if (context.ValidationInstance is T instance && Condition.Compile().Invoke(instance))
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

        public IValidationRuleBuilder<T, TMember> RuleFor<TMember>(Expression<Func<T, TMember>> expression)
        {
            throw new NotImplementedException();
        }

        public IValidationRuleBuilder<T, TCollection> RuleForEach<TCollection>(Expression<Func<T, TCollection>> expression) 
            where TCollection : IEnumerable
        {
            throw new NotImplementedException();
        }
    }
}
