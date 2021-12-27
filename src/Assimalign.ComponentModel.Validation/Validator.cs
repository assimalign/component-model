using System;
using System.Text.Json;
using System.Linq;
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
        var context = new ValidationContext<T>(instance) as IValidationContext;
        return Validate(context);
    }


    public Task<ValidationResult> ValidateAsync<T>(T instance, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="instance"></param>
    /// <param name="profile"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public ValidationResult Validate<T>(T instance, string profile)
    {
        return Validate(new ValidationContext<T>(instance), profile);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public ValidationResult Validate(IValidationContext context)
    {
        var profiles = this.options.Profiles
            .Where(x => x.Value.ValidationType == context.InstanceType)
            .Select(x=>x.Value);

        foreach(var profile in profiles)
        {
            var tokenSource = new CancellationTokenSource();
            var parallelOptions = new ParallelOptions()
            {
                CancellationToken = tokenSource.Token
            };

            var results = Parallel.ForEach(profile.ValidationRules, parallelOptions, rule =>
            {
                rule.Evaluate(context);

                if (profile.ValidationMode == ValidationMode.Stop && context.Errors.Any())
                {
                    tokenSource.Cancel();
                }
            });

            if (!results.IsCompleted)
            {
                throw new ValidationInternalException("");
            }
        }

        // Let's throw exception for any validation failure if requested.
        if (this.options.ThrowExceptionOnFailure && context.Errors.Any())
        {
            throw new ValidationFailureException(context.Errors);
        }

        return ValidationResult.Create(context);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="profile"></param>
    /// <returns></returns>
    public ValidationResult Validate(IValidationContext context, string profile)
    {
        var index = HashCode.Combine(profile, context.InstanceType);
        
        if (this.options.Profiles.TryGetValue(index, out var profile1))
        {
            foreach(var rule in profile1.ValidationRules)
            {
                rule.Evaluate(context);
            }
        }

        // Let's throw exception for any validation failure if requested.
        if (this.options.ThrowExceptionOnFailure && context.Errors.Any())
        {
            throw new ValidationFailureException(context.Errors);
        }

        return ValidationResult.Create(context);
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


    /// <summary>
    /// 
    /// </summary>
    /// <param name="json"></param>
    /// <param name="options"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IValidator CreateFromConfiguration<T>(string json, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public Task<ValidationResult> ValidateAsync<T>(T instance, string profileName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ValidationResult> ValidateAsync(IValidationContext context, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ValidationResult> ValidateAsync(IValidationContext context, string profileName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}