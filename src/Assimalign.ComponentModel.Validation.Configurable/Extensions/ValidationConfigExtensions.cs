using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation;


using Assimalign.ComponentModel.Validation.Configurable;
using Assimalign.ComponentModel.Validation.Configurable.Internal;

public static partial class ValidationConfigExtensions
{

    /// <summary>
    /// 
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public static IValidationConfigProfileBuilder AddJsonProvider<T>(this IValidationConfigProfileBuilder builder, string json)
        where T : class
    {


        return builder;
    }
    


   

    public static IValidator Create(this Validator validator, Func<IValidationConfigProfileBuilder, IValidationProfile[]> configure)
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


public sealed partial class ValidatorConfigurable
{

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