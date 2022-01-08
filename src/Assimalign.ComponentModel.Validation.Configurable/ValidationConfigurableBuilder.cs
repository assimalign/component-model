using System;
using System.Collections.Generic;

namespace Assimalign.ComponentModel.Validation.Configurable;

/// <summary>
/// 
/// </summary>
public sealed class ValidationConfigurableBuilder : IValidationConfigurableBuilder
{
    private readonly IList<IValidationConfigurableSource> sources;

    private ValidationConfigurableBuilder()
    {
        this.sources = new List<IValidationConfigurableSource>();
    }

    /// <summary>
    /// 
    /// </summary>
    public IList<IValidationConfigurableSource> Sources => this.sources;

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IValidationConfigurable Build()
    {
        var providers = new IValidationConfigurableProvider[this.sources.Count];

        for (int i = 0; i < providers.Length; i++)
        {
            providers[i] = sources[i].Build();
        }

        return new ValidationConfigurable(providers);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public IValidationConfigurableBuilder Add(IValidationConfigurableSource source)
    {
        this.sources.Add(source);
        return this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configure"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public IValidationConfigurableBuilder Configure(Func<IValidationConfigurableSource> configure)
    {
        return Add(configure.Invoke());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static IValidationConfigurableBuilder Create() =>
        new ValidationConfigurableBuilder();
}