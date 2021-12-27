
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation;


/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class ValidationContext<T> : IValidationContext
{
    private readonly Type type;
    private readonly ConcurrentStack<IValidationError> errors;
    private readonly ConcurrentStack<IValidationRule> successes;

    /// <summary>
    /// 
    /// </summary>
    public ValidationContext(T instance)
    {
        if (instance is null)
        {
            throw new ArgumentNullException(nameof(instance));
        }

        this.type = typeof(T);
        this.errors = new ConcurrentStack<IValidationError>();
        this.successes = new ConcurrentStack<IValidationRule>();

        Instance = instance;
    }

    /// <summary>
    /// The instance to validate.
    /// </summary>
    public T Instance { get; }

    object IValidationContext.Instance => this.Instance;

    /// <summary>
    /// 
    /// </summary>
    public Type InstanceType => this.type;

    /// <summary>
    /// A collection of validation failures that occurred.
    /// </summary>
    public IEnumerable<IValidationError> Errors => this.errors;

    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<IValidationRule> Successes => this.successes;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="error"></param>
    public void AddFailure(IValidationError error) => this.errors.Push(new ValidationError(error));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="failureMessage"></param>
    public void AddFailure(string failureMessage)
    {
        errors.Push(new ValidationError()
        {
            Message = failureMessage
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="failureSource"></param>
    /// <param name="failureMessage"></param>
    public void AddFailure(string failureSource, string failureMessage)
    {
        errors.Push(new ValidationError()
        {
            Message = failureMessage,
            Source = failureSource
        });
    }
}
