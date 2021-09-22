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





        
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TMember"></typeparam>
        /// <param name="rule"></param>
        /// <param name="validation"></param>
        /// <returns></returns>
        public static IValidationMemberRule<T, TMember>  Custom<T, TMember>(this IValidationMemberRule<T, TMember> rule, Action<TMember, IValidationContext> validation)
        {
            rule.AddRule(new CustomValidationRule<T, TMember>(rule.Member, validation));
            return rule;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TMember"></typeparam>
        /// <param name="rule"></param>
        /// <param name="message"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static IValidationMemberRule<T, TMember> NotEmpty<T, TMember>(this IValidationMemberRule<T, TMember> rule, string message = null, string code = null)
        {
            var validator = new NotEmptyValidationRule<T, TMember>(rule.Member);
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
        /// <param name="message"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static IValidationMemberRule<T, TMember> EmailAddress<T, TMember>(this IValidationMemberRule<T, TMember> rule, string message = null, string code = null)
        {
            var validator = new EmailValidationRule<T, TMember>(rule.Member);
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
