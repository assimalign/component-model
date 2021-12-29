using System;
using System.Collections;
using System.Linq.Expressions;

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
    /// <param name="error"></param>
    /// <returns></returns>
    bool IsValid(object value, out IValidationError error);
}

/// <summary>
/// 
/// </summary>
/// <typeparam name="TValue"></typeparam>
public interface IValidationRule<in TValue>
{
    /// <summary>
    /// 
    /// </summary>
    Type ValueType { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    bool IsValid(TValue value, out IValidationError error);
}