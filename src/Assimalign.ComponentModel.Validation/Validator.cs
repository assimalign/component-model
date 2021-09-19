using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Validation
{
    using Assimalign.ComponentModel.Validation.Rules;

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


    public abstract class Validator<T> : IValidator<T>
    {

        private readonly IList<IValidationRule> rules = new List<IValidationRule>();

        /// <summary>
        /// 
        /// </summary>
        protected Validator()
        {
            
        }

        public Validator(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        public ValidatorRuleSet<T> Rules => throw new NotImplementedException();


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TMember"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IValidatorRuleSet<TMember> RuleFor<TMember>(Expression<Func<T, TMember>> expression)
        {
            if (expression is MemberExpression member)
            {
                
            }
            else
            {

            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TMember"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IValidatorRuleSet<IEnumerable<TMember>> RuleForEach<TMember>(Expression<Func<T, IEnumerable<TMember>>> expression)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Validate(T instance)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Validate(object instance)
        {
            throw new NotImplementedException();
        }

        IValidationMemberRule<TMember> IValidator<T>.RuleFor<TMember>(Expression<Func<T, TMember>> expression)
        {
            throw new NotImplementedException();
        }

        IValidationMemberRule<IEnumerable<TMember>> IValidator<T>.RuleForEach<TMember>(Expression<Func<T, IEnumerable<TMember>>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
