using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Configurable;

/// <summary>
/// 
/// </summary>
public interface IValidationConfigBuilder
{
    /// <summary>
    /// A collection of configuration providers.
    /// </summary>
    IList<IValidationConfigProvider> Providers { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="provider"></param>
    /// <returns>><see cref="IValidationConfigBuilder"/></returns>
    IValidationConfigBuilder Add(IValidationConfigProvider provider);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configure"></param>
    /// <returns><see cref="IValidationConfigBuilder"/></returns>
    IValidationConfigBuilder Configure(Func<IValidationConfigProvider> configure);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <returns>><see cref="IValidationConfigBuilder"/></returns>
    IValidationConfigBuilder Configure(Func<IValidationConfigSource, IValidationConfigProvider> source);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IValidator Build();
}