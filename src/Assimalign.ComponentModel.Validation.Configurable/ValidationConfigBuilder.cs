using System;
using System.Collections.Generic;

namespace Assimalign.ComponentModel.Validation.Configurable;

/// <summary>
/// 
/// </summary>
public sealed class ValidationConfigBuilder : IValidationConfigBuilder
{
    private readonly IList<IValidationConfigProvider> providers;


    private ValidationConfigBuilder()
    {
        this.providers = new List<IValidationConfigProvider>();
    }

    /// <summary>
    /// 
    /// </summary>
    public IList<IValidationConfigProvider> Providers => this.providers;

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IValidator Build()
    {
        return new Validator(configure =>
        {
            foreach (var provider in this.providers)
            {
                var profile = provider.GetProfile();
                configure.AddProfile(profile);
            }
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    public IValidationConfigBuilder Add(IValidationConfigProvider provider)
    {
        this.providers.Add(provider);
        return this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configure"></param>
    /// <returns></returns>
    public IValidationConfigBuilder Configure(Func<IValidationConfigProvider> configure)
    {
        var provider = configure.Invoke();
        return Add(provider);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configure"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public IValidationConfigBuilder Configure(Func<IValidationConfigSource> configure)
    {
        var provider = configure.Invoke().Build();
        return Add(provider);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static IValidationConfigBuilder Create() =>
        new ValidationConfigBuilder();    
}