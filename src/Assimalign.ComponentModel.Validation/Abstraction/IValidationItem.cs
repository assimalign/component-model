using System;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Validation;

/// <summary>
/// 
/// </summary>
public interface IValidationItem
{
    /// <summary>
    /// 
    /// </summary>
    IValidationRuleStack ItemRuleStack { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="rule"></param>
    /// <returns></returns>
    IValidationItem AddRule(IValidationRule rule);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    void Evaluate(IValidationContext context);
}


/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="TValue"></typeparam>
public interface IValidationItem<T, TValue> : IValidationItem
{
    /// <summary>
    /// 
    /// </summary>
    Expression<Func<T, TValue>> ItemExpression { get; }
}