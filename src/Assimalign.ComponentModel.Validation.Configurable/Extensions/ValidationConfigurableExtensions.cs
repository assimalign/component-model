using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}