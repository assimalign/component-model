using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Validation
{
    using Assimalign.ComponentModel.Validation.Rules;
    using Assimalign.ComponentModel.Validation.Exceptions;

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

        private readonly ValidatorRuleSet rules = new ValidatorRuleSet();



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
        public ValidatorRuleSet ValidationRules => rules;


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
        /// <typeparam name="TMember"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IValidationMemberRule<T> RuleFor<TMember>(Expression<Func<T, TMember>> expression)
        {
            if (expression.Body is MemberExpression member)
            {
                var rule =  new ValidationMemberRule<T>()
                {
                    Member = member,
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

        public IValidationMemberRule<T, IEnumerable<TMember>> RuleForEach<TMember>(Expression<Func<T, IEnumerable<TMember>>> expression)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Validate(IValidationContext context)
        {
            throw new NotImplementedException();
        }
    }
}
