using System;
using System.Text.Json;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Assimalign.ComponentModel.Validation;


using Assimalign.ComponentModel.Validation.Rules;
using Assimalign.ComponentModel.Validation.Internal;
using Assimalign.ComponentModel.Validation.Exceptions;


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
    /// 
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
        return Validate(new ValidationContext<T>(instance));
    }


    public Task<ValidationResult> ValidateAsync<T>(T instance)
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
            .Where(x => x.Value.ValidationType == context.Type)
            .Select(x=>x.Value);

        foreach(var profile in profiles)
        {
            foreach(var rule in profile.ValidationRules)
            {
                rule.Evaluate(context);
            }
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
        var index = HashCode.Combine(profile, context.Type);
        
        if (this.options.Profiles.TryGetValue(index, out var profile1))
        {
            foreach(var rule in profile1.ValidationRules)
            {
                rule.Evaluate(context);
            }
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


}