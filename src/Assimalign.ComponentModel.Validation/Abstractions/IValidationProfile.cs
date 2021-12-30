using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.ComponentModel.Validation;

/// <summary>
/// Defines the validation rules for the <see cref="IValidationProfile.ValidationType"/>.
/// </summary>
public interface IValidationProfile
{
    /// <summary>
    /// The type to be validated.
    /// </summary>
    [MemberNotNull]
    Type ValidationType { get; }

    /// <summary>
    /// A collection of validation rules to apply 
    /// to the context being validated.
    /// </summary>
    [MemberNotNull]
    IEnumerable<IValidationItem> ValidationItems { get; }

    /// <summary>
    /// Specifies whether the validator should continue or stop after the first validation failure.
    /// </summary>
    [MemberNotNull]
    ValidationMode ValidationMode { get; }

    /// <summary>
    /// Configures the validation rules for the specified type.
    /// </summary>
    void Configure();
}

/// <summary>
///  Configures the validation rules for the specified type.
/// </summary>
public interface IValidationProfile<T> : IValidationProfile
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="descriptor"></param>
    void Configure(IValidationRuleDescriptor<T> descriptor);
}