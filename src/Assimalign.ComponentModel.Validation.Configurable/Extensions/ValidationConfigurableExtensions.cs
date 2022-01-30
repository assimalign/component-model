using System;

namespace Assimalign.ComponentModel.Validation.Configurable;

public static class ValidationConfigurableExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="configurable"></param>
    /// <returns></returns>
    public static IValidator ToValidator(this IValidationConfigurable configurable)
    {
        return Validator.Create(options =>
        {
            foreach (var profile in configurable.GetProfiles())
            {
                options.AddProfile(profile);
            }
        });
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="configurable"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IValidator ToValidator(this IValidationConfigurable configurable, Action<ValidationOptions> configure)
    {
        return Validator.Create(options =>
        {
            configure.Invoke(options);
            foreach (var profile in configurable.GetProfiles())
            {
                options.AddProfile(profile);
            }
        });
    }
}