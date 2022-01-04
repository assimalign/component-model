using System;
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
    /// 
    /// </summary>
    /// <typeparam name="TProvider"></typeparam>
    /// <returns></returns>
    IValidationConfigBuilder AddConfigProvider<TProvider>()
        where TProvider : IValidationConfigProvider, new();

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TProvider"></typeparam>
    /// <returns></returns>
    IValidationConfigBuilder AddConfigProvider<TProvider>(TProvider provider)
        where TProvider : IValidationConfigProvider;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TProvider"></typeparam>
    /// <param name="configure"></param>
    /// <returns></returns>
    IValidationConfigBuilder AddConfigProvider<TProvider>(Func<TProvider> configure)
        where TProvider : IValidationConfigProvider;

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerable<IValidationProfile> Build();
}