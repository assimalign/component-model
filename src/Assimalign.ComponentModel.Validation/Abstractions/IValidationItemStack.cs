using System;
using System.Collections;
using System.Collections.Generic;

namespace Assimalign.ComponentModel.Validation;

/// <summary>
/// 
/// </summary>
public interface IValidationItemStack :
    IEnumerable<IValidationItem>,
    ICollection,
    IReadOnlyCollection<IValidationItem>
{
    /// <summary>
    /// Returns the most recent validation item within the 
    /// stack by removing it.
    /// </summary>
    /// <returns><see cref="IValidationItem"/></returns>
    IValidationItem Pop();

    /// <summary>
    /// Attempts to return the most recent validation item within the 
    /// stack by removing it.
    /// </summary>
    /// <param name="item"></param>
    /// <returns><see cref="bool"/></returns>
    bool TryPop(out IValidationItem item);

    /// <summary>
    /// Returns the most recent validation item within the 
    /// stack without removing it.
    /// </summary>
    /// <returns><see cref="IValidationItem"/></returns>
    IValidationItem Peek();

    /// <summary>
    /// Attempts to return the most recent validation item within the 
    /// stack without removing it.
    /// </summary>
    /// <param name="item"></param>
    /// <returns><see cref="bool"/></returns>
    bool TryPeek(out IValidationItem item);

    /// <summary>
    /// Adds a new validation item to the stack.
    /// </summary>
    /// <param name="item"></param>
    void Push(IValidationItem item);
}
