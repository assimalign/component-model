using System;
using System.Collections;
using System.Collections.Generic;

namespace Assimalign.ComponentModel.Validation;

/// <summary>
/// An abstraction of FIFO (first in first out) for fluent validation.
/// </summary>
/// <remarks>
/// Validation rules that are chained together should be handled in 
/// the order they are chained which is why a <see cref="Stack"/> 
/// implementation was used.
/// </remarks>
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
}