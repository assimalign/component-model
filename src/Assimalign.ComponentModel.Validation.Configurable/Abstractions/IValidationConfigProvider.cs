namespace Assimalign.ComponentModel.Validation.Configurable;

/// <summary>
/// Configuration 
/// </summary>
public interface IValidationConfigProvider
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IValidationProfile GetProfile();
}