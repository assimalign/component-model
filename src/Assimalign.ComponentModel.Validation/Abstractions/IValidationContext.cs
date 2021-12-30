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
    /// A collection of invocation stats.
    /// </summary>
    IEnumerable<ValidationInvocation> Invocations { get; }

    /// <summary>
    /// A collection of validation failures.
    /// </summary>
    IEnumerable<IValidationError> Errors { get; }

    /// <summary>
    /// Adds a generic validation failure to <see cref="IValidationContext.Errors"/>
    /// </summary>
    /// <param name="message"></param>
    void AddFailure(string message);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="message"></param>
    void AddFailure(string source, string message);

    /// <summary>
    /// Adds a validation failure to <see cref="IValidationContext.Errors"/>
    /// </summary>
    /// <param name="error">A description of the validation error.</param>
    void AddFailure(IValidationError error);

    /// <summary>
    /// Adds a rule of a successful validation.
    /// </summary>
    /// <param name="invocation"></param>
    void AddInvocation(ValidationInvocation invocation);
}