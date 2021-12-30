using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation;

/// <summary>
/// 
/// </summary>
public sealed class ValidationResult
{
    private ValidationResult() { }

    internal ValidationResult(IValidationContext context, double? elapsedTicks = null)
    {
        this.Errors = context.Errors;
        this.Invocations = context.Invocations;
        this.ValidationElapsedTicks = elapsedTicks;
    }

    /// <summary>
    /// An indicator of whether the type validated was successful.
    /// </summary>
    public bool IsValid => Errors.Count() == 0;
    /// <summary>
    /// A collection of validation failures.
    /// </summary>
    public IEnumerable<IValidationError> Errors { get; }
    /// <summary>
    /// A collection of validation rules successfully invoked.
    /// </summary>
    public IEnumerable<ValidationInvocation> Invocations { get; }
    /// <summary>
    /// 
    /// </summary>
    public double? ValidationElapsedTicks { get; }
    /// <summary>
    /// 
    /// </summary>
    public double? ValidationElapsedMilliseconds => this.ValidationElapsedTicks is null ? null : this.ValidationElapsedTicks / (double)TimeSpan.TicksPerMillisecond;
    /// <summary>
    /// 
    /// </summary>
    public double? ValidationElapsedSeconds => this.ValidationElapsedTicks is null ? null : this.ValidationElapsedTicks / (double)TimeSpan.TicksPerSecond;
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="context"></param>
    /// <returns></returns>
    public static ValidationResult Create(IValidationContext context) => new ValidationResult(context);
}
