using System;
using System.Collections;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Validation;


/// <summary>
/// Initializes a validation rule builder.
/// </summary>
public interface IValidationRuleDescriptor<T>
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
    /// Encapsulates a set of validation rules that are evaluated if the condition <paramref name="condition"/> is true.
    /// </summary>
    /// <param name="condition">What condition is required</param>
    /// <param name="configure">The validation to </param>
    /// <returns></returns>
    IValidationRuleCondition<T> When(Expression<Func<T, bool>> condition, Action<IValidationRuleDescriptor<T>> configure);
}