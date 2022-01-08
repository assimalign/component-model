using System;
using System.Collections.Generic;

namespace Assimalign.ComponentModel.Validation.Configurable;

/// <summary>
/// 
/// </summary>
public interface IValidationConfigurableBuilder
{
    /// <summary>
    /// A collection of configuration providers.
    /// </summary>
    IList<IValidationConfigurableSource> Sources { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <returns>><see cref="IValidationConfigurableBuilder"/></returns>
    IValidationConfigurableBuilder Add(IValidationConfigurableSource source);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configure"></param>
    /// <returns>><see cref="IValidationConfigurableBuilder"/></returns>
    IValidationConfigurableBuilder Configure(Func<IValidationConfigurableSource> configure);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IValidationConfigurable Build();
}