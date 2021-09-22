using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation
{
    using Assimalign.ComponentModel.Validation.Rules;
    using Assimalign.ComponentModel.Validation.Abstraction;
    using Assimalign.ComponentModel.Validation.Internals;

    /// <summary>
    /// 
    /// </summary>
    public static partial class ValidationMemberRuleExtensions
    {


        //public static IValidationRule<T> EmailAddress(this IValidationRule<T>)


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rules"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        //public static IValidationRuleSet Custom<T>(this IValidationRuleSet<T> rules, Action<T, IValidationContext<T>> action)
        //{
        //    rules.Add(new CustomValidationRule<T>()
        //    {
        //        Action = action
        //    });

        //    return rules;
        //}


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <typeparam name="TMember"></typeparam>
        ///// <param name="rule"></param>
        ///// <returns></returns>
        //public static IValidationMemberRule<T, TMember> NotEmpty<T, TMember>(this IValidationMemberRule<T, TMember> rule)
        //{
        //    rule.AddRule(new MemberNotEmptyValidationRule<T, TMember>());
        //    return rule;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <typeparam name="TMember"></typeparam>
        ///// <param name="rule"></param>
        ///// <returns></returns>
        //public static IValidationMemberRule<T, TMember> Empty<T, TMember>(this IValidationMemberRule<T, TMember> rule)
        //{
        //    rule.AddRule(new MemberNotEmptyValidationRule<T, TMember>());
        //    return rule;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TMember"></typeparam>
        /// <param name="rule"></param>
        /// <returns></returns>
        public static IValidationMemberRule<T, TMember> EmailAddress<T, TMember>(this IValidationMemberRule<T, TMember> rule)
        {
            rule.AddRule(new EmailValidationRule<T, TMember>(rule.MemberDelegate));
            return rule;
        }




        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rules"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static IValidationRuleSet WithMessage<T>(this IValidationRuleSet rules, string message)
        {
            if (rules.Count == 0)
            {
                throw new Exception("A message cannot be set without first specifying at least one rule.");
            }
            else
            {
                //rules[rules.Count].Error.Message = message;
            }

            return rules;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rules"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static IValidationRuleSet WithErrorCode<T>(this IValidationRuleSet rules, string code)
        {
            if (rules.Count == 0)
            {
                throw new Exception("A message cannot be set without first specifying at least one rule.");
            }
            else
            {
                //rules[rules.Count].Error.Code = code;
            }

            return rules;
        }

    }
}
