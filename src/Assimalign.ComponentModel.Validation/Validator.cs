using System;
using System.Linq;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Assimalign.ComponentModel.Validation;


/// <summary>
/// 
/// </summary>
public partial class Validator : IValidator
{
    private readonly ValidationOptions options;
    private readonly IDictionary<string, object> contextOptions;

    private Validator() { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="options">Required for setup</param>
    public Validator(ValidationOptions options)
    {
        this.options = options;
        this.contextOptions = new Dictionary<string, object>()
        {
            { "ThrowExceptionOnFailure", options.ThrowExceptionOnFailure },
            { "ContinueThroughValidationChain", options.ContinueThroughValidationChain }
        };
    }

    /// <summary>
    /// A fluent constructor for configuring the validator options.
    /// </summary>
    /// <param name="configure"></param>
    public Validator(Action<ValidationOptions> configure)
    {
        var options = new ValidationOptions();

        configure.Invoke(options);

        this.options = options;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="instance"></param>
    /// <returns></returns>
    public ValidationResult Validate<T>(T instance)
    {
        return Validate(new ValidationContext<T>(instance, true) 
        { 
            Options = this.contextOptions
        } as IValidationContext);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public ValidationResult Validate(IValidationContext context)
    {
        var stopwatch = new Stopwatch();

        stopwatch.Start();
        foreach (var profile in this.options.Profiles)
        {
            if (profile.ValidationType == context.InstanceType)
            {
                var isModeStop = profile.ValidationMode == ValidationMode.Stop;

                foreach (var item in profile.ValidationItems)
                {
                    if (isModeStop && context.Errors.Any())
                    {
                        break;
                    }

                    item.Evaluate(context);
                }
            }
        }

        // Let's throw exception for any validation failure if requested.
        if (this.options.ThrowExceptionOnFailure && context.Errors.Any())
        {
            throw new ValidationFailureException(context);
        }

        return new ValidationResult(context, stopwatch.ElapsedTicks);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="instance"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<ValidationResult> ValidateAsync<T>(T instance, CancellationToken cancellationToken = default)
    {
        return ValidateAsync(new ValidationContext<T>(instance, true)
        {
            Options = this.contextOptions
        } as IValidationContext, cancellationToken);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ValidationFailureException"></exception>
    public Task<ValidationResult> ValidateAsync(IValidationContext context, CancellationToken cancellationToken = default)
    {
        return Task.Run<ValidationResult>(() =>
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            foreach (var profile in this.options.Profiles)
            {
                if (profile.ValidationType == context.InstanceType)
                {
                    var isModeStop = profile.ValidationMode == ValidationMode.Stop;
                    var tokenSource = cancellationToken == default ?
                        new CancellationTokenSource() :
                        CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

                    foreach(var item in profile.ValidationItems)
                    {
                        if (tokenSource.IsCancellationRequested)
                        {
                            return default;
                        }
                        if (isModeStop && context.Errors.Any())
                        {
                            break;
                        }

                        item.Evaluate(context);
                    }
                }
            }

            if (this.options.ThrowExceptionOnFailure && context.Errors.Any())
            {
                throw new ValidationFailureException(context);
            }

            stopwatch.Stop();

            return new ValidationResult(context, stopwatch.ElapsedTicks);
        });
    }
    

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IValidator Create(Action<ValidationOptions> configure)
    {
        var options = new ValidationOptions();

        configure.Invoke(options);

        return new Validator(options);
    }
}