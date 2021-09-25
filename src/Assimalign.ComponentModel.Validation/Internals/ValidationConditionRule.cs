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

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TMember"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IValidationRuleBuilder<T, TMember> RuleFor<TMember>(Expression<Func<T, TMember>> expression)
        {
            var rule = new ValidationMemberRule<T, TMember>()
            {
                Member = expression
            };

            Rules.Add(rule);

            return new ValidationMemberRuleBuilder<T, TMember>(rule);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCollection"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IValidationRuleBuilder<T, TCollection> RuleForEach<TCollection>(Expression<Func<T, TCollection>> expression) 
            where TCollection : IEnumerable
        {
            var rule = new ValidationCollectionRule<T, TCollection>()
            {
                Collection = expression
            };

            Rules.Add(rule);

            return new ValidationCollectionRuleBuilder<T, TCollection>(rule);
        }
    }
}
