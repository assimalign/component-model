using System;


namespace Assimalign.ComponentModel.Validation;

/// <summary>
/// 
/// </summary>
public interface IValidationRule
{
    /// <summary>
    /// 
    /// </summary>
    string Name { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    bool TryValidate(object value, out IValidationContext context);
}

/// <summary>
/// 
/// </summary>
/// <typeparam name="TValue"></typeparam>
public interface IValidationRule<in TValue> : IValidationRule
{
    /// <summary>
    /// 
    /// </summary>
    Type ValueType { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    bool TryValidate(TValue value, out IValidationContext context);
}