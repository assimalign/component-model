using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation;

using Assimalign.ComponentModel.Validation.Rules;

/// <summary>
/// 
/// </summary>
public interface IValidationRuleStack : IEnumerable<IValidationRule>, IEnumerable, ICollection, IReadOnlyCollection<IValidationRule>
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    internal IValidationRule Pop();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    internal bool TryPop(out IValidationRule result);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    internal IValidationRule Peek();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    internal bool TryPeek(out IValidationRule result);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    internal void Push(IValidationRule item);

    /// <summary>
    /// Evaluates the collection of
    /// </summary>
    void Evaluate(IValidationContext context);
}

