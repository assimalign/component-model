using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Configurable;

public sealed class ValidationConfigBuilder : IValidationConfigProfileBuilder
{
    public readonly IList<IValidationConfigProvider> profiles;


    public ValidationConfigBuilder()
    {
        this.profiles = new List<IValidationConfigProvider>();
    }

    public IEnumerable<IValidationProfile> Build()
    {
        foreach(var profile in this.profiles)
        {
            yield return profile.Compile();
        }
    }

    public IValidationConfigProfileBuilder AddConfigProvider<TProvider>() 
        where TProvider : IValidationConfigProvider, new()
    {
        this.profiles.Add(new TProvider());
        return this;
    }

    public IValidationConfigProfileBuilder AddConfigProvider<TProvider>(TProvider provider) 
        where TProvider : IValidationConfigProvider
    {
        this.profiles.Add(provider);
        return this;
    }

    public IValidationConfigProfileBuilder AddConfigProvider<TProvider>(Func<TProvider> configure) 
        where TProvider : IValidationConfigProvider
    {
        this.profiles.Add(configure.Invoke());
        return this;
    }



    public static IValidator Create(Func<IValidationConfigProfileBuilder, IValidationProfile[]> configure)
    {
        var builder = new ValidationConfigBuilder();
        var profiles = configure.Invoke(builder);


        return new Validator(configure =>
        {
            foreach (var profile in profiles)
            {
                configure.AddProfile(profile);
            }
        });
    }
}