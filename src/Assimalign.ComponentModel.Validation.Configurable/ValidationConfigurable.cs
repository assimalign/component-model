using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Configurable;


internal sealed class ValidationConfigurable : IValidationConfigurable
{
    private readonly IEnumerable<IValidationConfigurableProvider> providers;

    public ValidationConfigurable(IEnumerable<IValidationConfigurableProvider> providers)
    {
        this.providers = providers;
    }

    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<IValidationConfigurableProvider> Providers { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public IValidationProfile GetProfile(Type type)
    {
        foreach (var provider in Providers)
        {
            if (provider.TryGetProfile(type, out var profile))
            {
                return profile;
            }
        }

        throw new Exception();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IEnumerable<IValidationProfile> GetProfiles()
    {
        foreach (var provider in this.providers)
        {
            yield return provider.GetProfile();
        }
    }
}