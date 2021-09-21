using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Rules
{
    /// <summary>
    /// 
    /// </summary>
    public static class ValidationRuleBuilder
    {


        //public static IValidationRule<T> EmailAddress(this IValidationRule<T>)


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rules"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IValidationRuleSet<T> Custom<T>(this IValidationRuleSet<T> rules, Action<T, IValidationContext<T>> action)
        {
            rules.Add(new CustomValidationRule<T>()
            {
                Action = action
            });

            return rules;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="memberRule"></param>
        /// <returns></returns>
        public static IValidationMemberRule<T, TMember> EmailAddress<T, TMember>(this IValidationMemberRule<T, TMember> memberRule)
        {
            if (memberRule.MemberDelegate is Expression<Func<T, string>> lambda)
            {
                memberRule.AddMemberRule(new MemberEmailValidationRule<T>(lambda));
            }

            return memberRule;
        }




        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rules"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static IValidationRuleSet<T> WithMessage<T>(this IValidationRuleSet<T> rules, string message)
        {
            if (rules.Count == 0)
            {
                throw new Exception("A message cannot be set without first specifying at least one rule.");
            }
            else
            {
                rules[rules.Count].Error.Message = message;
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
        public static IValidationRuleSet<T> WithErrorCode<T>(this IValidationRuleSet<T> rules, string code)
        {
            if (rules.Count == 0)
            {
                throw new Exception("A message cannot be set without first specifying at least one rule.");
            }
            else
            {
                rules[rules.Count].Error.Code = code;
            }

            return rules;
        }

    }
}
