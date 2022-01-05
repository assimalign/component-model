using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Validation;


/// <summary>
/// 
/// </summary>
public sealed class ValidatorFactoryBuilder
{
    internal readonly IDictionary<string, IValidator> validators;

    internal ValidatorFactoryBuilder()
    {
        this.validators = new Dictionary<string, IValidator>();
    }

    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<IValidator> Validators => this.validators.Values;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="validatorName"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public ValidatorFactoryBuilder AddValidator(string validatorName, Action<ValidationOptions> configure)
    {
        var validator = Validator.Create(configure);

        this.validators.Add(validatorName.ToLower(), validator);

        return this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name=""></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public ValidatorFactoryBuilder AddValidator<T>(Expression<Func<T, bool>> condition, Action<ValidationOptions> configure)
    {
        var validator = Validator.Create(configure);

        this.validators.Add(validatorName.ToLower(), validator);

        return this;
    }

}

