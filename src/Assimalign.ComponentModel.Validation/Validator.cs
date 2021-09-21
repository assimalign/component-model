using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Validation
{
    using Assimalign.ComponentModel.Validation.Rules;
    using Assimalign.ComponentModel.Validation.Exceptions;
    using System.Collections;

    public sealed class Validator : IValidator
    {


        public string Name { get; set; }

        public ValidationResult Validate(object instance)
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
    public abstract class Validator<T> : IValidator<T>
    {

        private readonly ValidationRuleSet rules = new ValidationRuleSet();



        /// <summary>
        /// 
        /// </summary>
        protected Validator() { }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public Validator(string name)
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
        public IValidationRuleSet ValidationRules => rules;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public ValidationResult Validate(T instance)
        {
            var context = new ValidationContext<T>(instance);

            foreach(var rule in this.ValidationRules)
            {
                rule.Evaluate(context);
            }

            return ValidationResult.Create(context);
        }

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
        /// <typeparam name="TMember"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IValidationMemberRule<T, TMember> RuleFor<TMember>(Expression<Func<T, TMember>> expression)
        {
            // Ensure that the body of the LambdaExpression is 
            // a valid Member Expression.
            if (expression.Body is MemberExpression member)
            {
                var rule =  new ValidationMemberRule<T, TMember>()
                { 
                    MemberDelegate = expression
                };

                ValidationRules.Add(rule);

                return rule;
            }
            else
            {
                throw new ValidatorMemberException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCollection"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IValidationCollectionRule<T, TCollection> RuleForEach<TCollection>(Expression<Func<T, TCollection>> expression)
            where TCollection : IEnumerable
        {
            throw new NotImplementedException();
        }

        

        public IValidationConditionRule<T> When(Expression<Func<T, bool>> condition, Action<IValidationConditionRule<T>> rules)
        {
           
            throw new NotImplementedException();
        }
    }
}
