using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Configurable;

public sealed class ValidationConfigBuilder : IValidationConfigBuilder
{
    public readonly IList<IValidationConfigProvider> providers;


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
            foreach (var provider in providers)
            {
                var profile = provider.Compile();
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
        this.providers.Add(provider);
        return this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configure"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public IValidationConfigBuilder Configure(Func<IValidationConfigSource, IValidationConfigProvider> configure)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static IValidationConfigBuilder Create() =>
        new ValidationConfigBuilder();    
}