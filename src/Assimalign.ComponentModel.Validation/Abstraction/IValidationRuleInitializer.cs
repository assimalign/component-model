using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Abstraction
{
    /// <summary>
    /// Initializes a validation rule builder.
    /// </summary>
    public interface IValidationRuleInitializer<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TValue">A member expression is either a field or property of a Type.</typeparam>
        /// <param name="expression"></param>
        IValidationRuleBuilder<T, TValue> RuleFor<TValue>(Expression<Func<T, TValue>> expression);

        /// <summary>
        /// Initializes
        /// </summary>
        /// <typeparam name="TValue">Must be a </typeparam>
        /// <param name="expression"></param>
        IValidationRuleBuilder<T, TValue> RuleForEach<TValue>(Expression<Func<T, TValue>> expression)
            where TValue : IEnumerable;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition">What condition is required</param>
        /// <param name="configure">The validation to </param>
        /// <returns></returns>
        IValidationConditionRule<T> When(Expression<Func<T, bool>> condition, Action<IValidationRuleInitializer<T>> configure);

    }
}
