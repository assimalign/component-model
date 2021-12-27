using System;
using System.Linq.Expressions;


namespace Assimalign.ComponentModel.Validation;

/// <summary>
/// 
/// </summary>
public interface IValidationRule
{
    /// <summary>
    /// A unique name for the rule to evaluate.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Runs the validation rule against the given context.
    /// </summary>
    /// <param name="context"></param>
    void Evaluate(IValidationContext context);
}



/// <summary>
/// Represents a validation rule for a property or field of a given type.
/// </summary>
/// <typeparam name="T">Is the type in which the property or field is a member of.</typeparam>
/// <typeparam name="TValue">Is either a property or member of type 'T'.</typeparam>
public interface IValidationRule<T, TValue> : IValidationRule
{
    /// <summary>
    /// 
    /// </summary>
    Expression<Func<T, TValue>> ValidationExpression { get; }

    /// <summary>
    /// The validation rules to apply to the member.
    /// </summary>
    IValidationRuleStack ValidationRules { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="rule"></param>
    IValidationRule<T, TValue> AddRule(IValidationRule rule);
}

