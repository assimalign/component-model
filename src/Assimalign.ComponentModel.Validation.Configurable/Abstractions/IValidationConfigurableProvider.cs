using System;

namespace Assimalign.ComponentModel.Validation.Configurable;

/// <summary>
/// Configuration 
/// </summary>
public interface IValidationConfigurableProvider
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    bool TryGetProfile(Type type, out IValidationProfile profile);

    /// <summary>
    /// 
    /// </summary>
    /// <returns><see cref="IValidationProfile"/></returns>
    IValidationProfile GetProfile();
}