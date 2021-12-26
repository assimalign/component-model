using System;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Validation;

/// <summary>
/// Represents a validation rule for which other validation rules should be applied. 
/// </summary>
/// <remarks>
/// The logical flow is best described as if (Condition is true/false) then { Run Validation Rules }
/// </remarks>
/// <typeparam name="T"></typeparam>
public interface IValidationCondition<T> 
{
    /// <summary>
    /// The Condition in which the child validation rule collection should be applied.
    /// </summary>
    Expression<Func<T, bool>> Condition { get; }

    /// <summary>
    /// The rule set to run if condition predicate is 'true'. <see cref="IValidationCondition{T}.When(Expression{Func{T, bool}}, Action{IValidationRuleDescriptor{T}})"/>
    /// </summary>
    IValidationRuleStack ConditionRuleSet { get; }

    /// <summary>
    /// The rule set to run if condition predicate is 'false'. <see cref="IValidationCondition{T}.Otherwise(Action{IValidationRuleDescriptor{T}})"/>
    /// </summary>
    IValidationRuleStack ConditionDefaultRuleSet { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="condition">What condition is required</param>
    /// <param name="configure">The validation to </param>
    /// <returns></returns>
    IValidationCondition<T> When(Expression<Func<T, bool>> condition, Action<IValidationRuleDescriptor<T>> configure);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configure"></param>
    void Otherwise(Action<IValidationRuleDescriptor<T>> configure);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    void Evaluate(IValidationContext context)
}