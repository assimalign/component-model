using System;
using System.Collections.Generic;

namespace Assimalign.ComponentModel.Validation;

/// <summary>
/// 
/// </summary>
public interface IValidationContext
{
    /// <summary>
    /// The instance to apply the validation rules that match the instance type.
    /// </summary>
    object Instance { get; }

    /// <summary>
    /// The instance type being validated.
    /// </summary>
    Type InstanceType { get; }

    /// <summary>
    /// A collection of successful validations.
    /// </summary>
    IEnumerable<IValidationRule> Successes { get; }

    /// <summary>
    /// A collection of validation failures.
    /// </summary>
    IEnumerable<IValidationError> Errors { get; }

    /// <summary>
    /// Adds a validation failure to <see cref="IValidationContext.Errors"/>
    /// </summary>
    /// <param name="error">A description of the validation error.</param>
    void AddFailure(IValidationError error);

    /// <summary>
    /// Adds a generic validation failure to <see cref="IValidationContext.Errors"/>
    /// </summary>
    /// <param name="failureMessage"></param>
    void AddFailure(string failureMessage);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="failureSource"></param>
    /// <param name="failureMessage"></param>
    void AddFailure(string failureSource, string failureMessage);

    /// <summary>
    /// Adds a rule of a successful validation.
    /// </summary>
    /// <param name="rule"></param>
    void AddSuccess(IValidationRule rule);
}