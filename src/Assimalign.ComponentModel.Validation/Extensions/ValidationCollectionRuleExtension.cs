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
        /// <returns></returns>
        public static IValidationCollectionRule<T, TCollection> NotEmpty<T, TCollection>(this IValidationCollectionRule<T, TCollection> rule)
            where TCollection : IEnumerable
        {
            if (rule.Collection.Body is MemberExpression ||
                rule.Collection.Body is MethodCallExpression)
            {
                rule.AddRule(new NotEmptyValidationRule<T, TCollection>(rule.Collection));
            }
            else
            {
                // TODO: Decide whether to throw an exception if any.
            }
            
            return rule;
        }
    }
}
