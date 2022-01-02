namespace Assimalign.ComponentModel.Validation;

/// <summary>
/// 
/// </summary>
public interface IValidatorFactory
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="validatorName"></param>
    /// <returns></returns>
    IValidator Create(string validatorName);
}

