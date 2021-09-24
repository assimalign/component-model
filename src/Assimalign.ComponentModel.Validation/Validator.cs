using System;
using System.Text.Json;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Assimalign.ComponentModel.Validation
{
    using Assimalign.ComponentModel.Validation.Rules;
    using Assimalign.ComponentModel.Validation.Internals;
    using Assimalign.ComponentModel.Validation.Exceptions;
    using Assimalign.ComponentModel.Validation.Abstraction;
    
    /// <summary>
    /// 
    /// </summary>
    public sealed class Validator : IValidator
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IValidationRuleSet Rules => throw new NotImplementedException();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public ValidationResult Validate(IValidationContext context)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <param name="options"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IValidator BuildFromJson<T>(string json, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Validator<T> : IValidator<T>, IValidationRuleInitializer<T>
    {

        private readonly ValidationRuleSet rules = new ValidationRuleSet();



        /// <summary>
        /// 
        /// </summary>
        protected Validator(string name = "")
        {
            this.Name = name;
        }


        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IValidationRuleSet Rules => rules;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public ValidationResult Validate(T instance) =>
            Validate(new ValidationContext<T>(instance));


        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public ValidationResult Validate(IValidationContext context)
        {
            foreach (var rule in this.Rules)
            {
                rule.Evaluate(context);
            }

            return ValidationResult.Create(context);
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

            return new ValidationRuleBuilder<T, TMember>(rule);
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

            return new ValidationRuleBuilder<T, TCollection>(rule);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition">What condition is required</param>
        /// <param name="configure">The validation to </param>
        /// <returns></returns>
        public IValidationConditionRule<T> When(Expression<Func<T, bool>> condition, Action<IValidationConditionRule<T>> configure)
        {
            var rule = new ValidationConditionRule<T>()
            {
                Condition = condition
            };

            configure.Invoke(rule);

            Rules.Add(rule);

            return rule;
        }
    }
}
