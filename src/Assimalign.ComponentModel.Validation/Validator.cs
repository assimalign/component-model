using System;
using System.Text.Json;
using System.Linq;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Assimalign.ComponentModel.Validation;


using Assimalign.ComponentModel.Validation.Internal;
using Assimalign.ComponentModel.Validation.Internal.Exceptions;


/// <summary>
/// 
/// </summary>
public sealed class Validator : IValidator
{
    private readonly ValidationOptions options;

    internal Validator() { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    public Validator(ValidationOptions options)
    {
        this.options = options;
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
        return Validate(new ValidationContext<T>(instance) as IValidationContext);
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
                var tokenSource = new CancellationTokenSource();
                var parallelOptions = new ParallelOptions()
                {
                    CancellationToken = tokenSource.Token
                };

                var results = Parallel.ForEach(profile.ValidationItems, parallelOptions, item =>
                {
                    if (profile.ValidationMode == ValidationMode.Stop && context.Errors.Any())
                    {
                        tokenSource.Cancel();
                    }

                    item.Evaluate(context);
                });

                if (!results.IsCompleted)
                {
                    throw new ValidationInternalException("Unable to complete validation");
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
        return ValidateAsync(new ValidationContext<T>(instance) as IValidationContext, cancellationToken);
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
                    var tokenSource = cancellationToken == default ?
                        new CancellationTokenSource() :
                        CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

                    var parallelOptions = new ParallelOptions()
                    {
                        CancellationToken = tokenSource.Token
                    };

                    var results = Parallel.ForEach(profile.ValidationItems, parallelOptions, item =>
                    {
                        if (profile.ValidationMode == ValidationMode.Stop && context.Errors.Any())
                        {
                            tokenSource.Cancel();
                        }

                        item.Evaluate(context);
                    });
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