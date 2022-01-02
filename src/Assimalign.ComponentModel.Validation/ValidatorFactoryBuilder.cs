using System;
using System.Collections.Generic;

namespace Assimalign.ComponentModel.Validation;


/// <summary>
/// 
/// </summary>
public sealed class ValidatorFactoryBuilder
{
    private readonly IDictionary<string, IValidator> validators;

    internal ValidatorFactoryBuilder()
    {
        this.validators = new Dictionary<string, IValidator>();
    }

    /// <summary>
    /// 
    /// </summary>
    public IDictionary<string, IValidator> Validators => this.validators;

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
}

