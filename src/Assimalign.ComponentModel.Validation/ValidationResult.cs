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
        this.Invocations = context.Successes.Select(x => x.Name);
    }

    /// <summary>
    /// An indicator of whether the type validated was successful.
    /// </summary>
    public bool IsValid => Errors.Count() == 0;


    /// <summary>
    /// A collection of validation failures.
    /// </summary>
    public IEnumerable<IValidationError> Errors { get; internal set; }


    /// <summary>
    /// A collection of validation rules successfully invoked.
    /// </summary>
    public IEnumerable<string> Invocations { get; }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="context"></param>
    /// <returns></returns>
    public static ValidationResult Create(IValidationContext context) => new ValidationResult(context);
}
