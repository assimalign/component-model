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

    internal ValidationResult(IValidationContext context)
    {
        this.Errors = context.Errors;
        this.Invocations = context.Invocations;
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
    public long? ValidationElapsedTicks
    {
        get
        {
            long ticks = 0;

            foreach(var invocation in Invocations)
            {
                if (invocation.ElapsedTicks is not null)
                {
                    ticks += invocation.ElapsedTicks ?? 0;
                }
            }

            return ticks;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public long? ValidationElapsedMilliseconds => this.ValidationElapsedTicks is null ? null : this.ValidationElapsedTicks / TimeSpan.TicksPerMillisecond;
    /// <summary>
    /// 
    /// </summary>
    public long? ValidationElapsedSeconds => this.ValidationElapsedTicks is null ? null : this.ValidationElapsedTicks / TimeSpan.TicksPerSecond;
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="context"></param>
    /// <returns></returns>
    public static ValidationResult Create(IValidationContext context) => new ValidationResult(context);
}
