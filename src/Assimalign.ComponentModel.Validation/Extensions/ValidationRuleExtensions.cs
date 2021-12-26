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
    using Assimalign.ComponentModel.Validation.Internal;
    

    /// <summary>
    /// 
    /// </summary>
    public static partial class ValidationRuleExtensions
    {





        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IValidationRuleBuilder<T, TValue> EmailAddress<T, TValue>(this IValidationRuleBuilder<T, TValue> builder)
            where TValue : IEnumerable
        {
            if (builder.Current is ValidationMemberRule<T, TValue> member)
            {
                member.AddRule(new EmailValidationRule<T, TValue>(member.Member));
            }
            if (builder.Current is ValidationCollectionRule<T, TValue> collection)
            {
                collection.AddRule(new EmailValidationRule<T, TValue>(collection.Collection));
            }
            return builder;
        }
    }
}
