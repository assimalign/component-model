using System;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.ComponentModel.Validation;

/// <summary>
/// Defines the validation rules for the <see cref="IValidationProfile.ValidationType"/>.
/// </summary>
public interface IValidationProfile 
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// In many cases validation requirements can change based on a collection of 
    /// various inputs. For example, when property A and property B is set use -> Validator A, 
    /// but when property A and property C is set then use -> Validator B. Assigning a name can be useful
    /// by breaking out validation into multiple validators rather than stuffing all logic into one validator
    /// since it can represent one validation pass/failure of a larger whole.
    /// </remarks>
    [MemberNotNull]
    string Name { get; }

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
    IValidationRuleStack ValidationRules { get; }

    /// <summary>
    /// 
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