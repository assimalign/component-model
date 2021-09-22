using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation
{
    using Assimalign.ComponentModel.Validation.Rules;
    using Assimalign.ComponentModel.Validation.Abstraction;

    /// <summary>
    /// 
    /// </summary>
    public static class ValidationCollectionRuleExtension
    {

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TCollection"></typeparam>
        /// <param name="rule"></param>
        /// <param name="validation"></param>
        /// <returns></returns>
        public static IValidationCollectionRule<T, TCollection> Custom<T, TCollection>(this IValidationCollectionRule<T, TCollection> rule, Action<TCollection, IValidationContext> validation)
            where TCollection : IEnumerable
        {
            rule.AddRule(new CustomValidationRule<T, TCollection>(rule.Collection, validation));
            return rule;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TCollection"></typeparam>
        /// <param name="rule"></param>
        /// <param name="message"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static IValidationCollectionRule<T, TCollection> EmailAddress<T, TCollection>(this IValidationCollectionRule<T, TCollection> rule, string message = null, string code = null)
            where TCollection : IEnumerable
        {
            var validator = new EmailValidationRule<T, TCollection>(rule.Collection);
            if (code is not null)
            {
                validator.Code = code;
            }
            if (message is not null)
            {
                validator.Message = message;
            }
            rule.AddRule(validator);
            return rule;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TCollection"></typeparam>
        /// <param name="rule"></param>
        /// <param name="message"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static IValidationCollectionRule<T, TCollection> NotEmpty<T, TCollection>(this IValidationCollectionRule<T, TCollection> rule, string message = null, string code = null)
            where TCollection : IEnumerable
        {
            var validator = new NotEmptyValidationRule<T, TCollection>(rule.Collection);
            if (code is not null)
            {
                validator.Code = code;
            }
            if (message is not null)
            {
                validator.Message = message;
            }
            rule.AddRule(validator);
            return rule;
        }
    }
}
