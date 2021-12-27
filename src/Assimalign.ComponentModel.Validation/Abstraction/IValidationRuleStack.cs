using System;
using System.Collections;
using System.Collections.Generic;

namespace Assimalign.ComponentModel.Validation;

/// <summary>
/// 
/// </summary>
public interface IValidationRuleStack : 
    IEnumerable<IValidationRule>, 
    IEnumerable, 
    ICollection, 
    IReadOnlyCollection<IValidationRule>
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IValidationRule Pop();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    bool TryPop(out IValidationRule result);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IValidationRule Peek();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    bool TryPeek(out IValidationRule result);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    void Push(IValidationRule item);

    /// <summary>
    /// Evaluates the collection of
    /// </summary>
    void Evaluate(IValidationContext context);
}