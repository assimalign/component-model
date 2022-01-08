using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Configurable;

internal sealed class ValidationConfigurableJsonProvider<T> : IValidationConfigurableProvider
{
    private readonly ValidationConfigurableJsonProfile<T> profile;


    public ValidationConfigurableJsonProvider(ValidationConfigurableJsonProfile<T> profile)
    {
        this.profile = profile;
    }


    public IValidationProfile GetProfile() => this.profile;


    public bool TryGetProfile(Type type, out IValidationProfile profile)
    {
        if (this.profile.ValidationType == type)
        {
            profile = this.profile;
            return true;
        }
        else
        {
            profile=null;
            return false;
        }
    }
}