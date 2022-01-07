namespace Assimalign.ComponentModel.Validation.Configurable;

/// <summary>
/// 
/// </summary>
public interface IValidationConfigSource
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IValidationConfigProvider Build();
}