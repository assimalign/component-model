using System;
using System.Collections.Generic;

namespace Assimalign.ComponentModel.Validation.Configurable;

/// <summary>
/// 
/// </summary>
public interface IValidationConfigBuilder
{
    /// <summary>
    /// A collection of configuration providers.
    /// </summary>
    IList<IValidationProfile> Providers { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="provider"></param>
    /// <returns>><see cref="IValidationConfigBuilder"/></returns>
    IValidationConfigBuilder Add(IValidationProfile provider);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configure"></param>
    /// <returns><see cref="IValidationConfigBuilder"/></returns>
    IValidationConfigBuilder Configure(Func<IValidationProfile> configure);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configure"></param>
    /// <returns>><see cref="IValidationConfigBuilder"/></returns>
    IValidationConfigBuilder Configure(Func<IValidationConfigSource> configure);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IValidator Build();
}