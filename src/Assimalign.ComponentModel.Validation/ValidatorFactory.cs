using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation;


/// <summary>
/// 
/// </summary>
internal sealed class ValidatorFactory : IValidatorFactory
{

    private readonly IDictionary<string, IValidator> validators;

    private ValidatorFactory() { }

    internal ValidatorFactory(IDictionary<string, IValidator> validators)
    {
        this.validators = validators;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="validatorName"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public IValidator Create(string validatorName)
    {
        if (validators.TryGetValue(validatorName.ToLower(), out var validator)) 
        {
            return validator;
        }
        else
        {
            throw new Exception();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IValidatorFactory Configure(Action<ValidatorFactoryBuilder> configure)
    {
        var builder = new ValidatorFactoryBuilder();

        configure.Invoke(builder);

        return new ValidatorFactory(builder.Validators);
    }



}