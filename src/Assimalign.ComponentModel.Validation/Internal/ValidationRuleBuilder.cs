using System;
using System.Linq;

namespace Assimalign.ComponentModel.Validation.Internal;

using Assimalign.ComponentModel.Validation.Internal.Rules;
using Assimalign.ComponentModel.Validation.Internal.Exceptions;

internal sealed class ValidationRuleBuilder<T, TValue> : IValidationRuleBuilder<T, TValue>
{

    public ValidationRuleBuilder(IValidationRule validationRule)
    {
        this.ValidationRule = validationRule;
    }

    /// <summary>
    /// 
    /// </summary>
    public IValidationRule ValidationRule { get; }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TBound"></typeparam>
    /// <param name="lowerBound"></param>
    /// <param name="upperBound"></param>
    /// <returns></returns>
    public IValidationRuleBuilder<T, TValue> Between<TBound>(TBound lowerBound, TBound upperBound)
        where TBound : IComparable
    {
        if (this.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            return Between<TBound>(lowerBound, upperBound, configure =>
            {
                configure.Message = $"One of the following items in '{string.Join('.', validationRule.ValidationExpression.Body.ToString().Split('.').Skip(1))}' " +
                                    $"is not within bounds of: {lowerBound} and {upperBound}.";
                configure.Source = validationRule.ValidationExpression.Body.ToString();
            });
        }
        else
        {
            throw new ValidationUnsupportedRuleException(this.ValidationRule);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TBound"></typeparam>
    /// <param name="lowerBound"></param>
    /// <param name="upperBound"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public IValidationRuleBuilder<T, TValue> Between<TBound>(TBound lowerBound, TBound upperBound, Action<IValidationError> configure) 
        where TBound : IComparable
    {
        if (this.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            var error = new ValidationError();

            configure.Invoke(error);

            validationRule.AddRule(new BetweenValidationRule<T, TValue, TBound>(validationRule.ValidationExpression, lowerBound, upperBound)
            {
                Error = error
            });

            return this;
        }
        else
        {
            throw new ValidationUnsupportedRuleException(this.ValidationRule);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TBound"></typeparam>
    /// <param name="lowerBound"></param>
    /// <param name="upperBound"></param>
    /// <returns></returns>
    public IValidationRuleBuilder<T, TValue> BetweenOrEqualTo<TBound>(TBound lowerBound, TBound upperBound)
        where TBound : IComparable
    {
        if (this.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            return BetweenOrEqualTo<TBound>(lowerBound, upperBound, configure =>
            {
                configure.Message = $"One of the following items in '{string.Join('.', validationRule.ValidationExpression.Body.ToString().Split('.').Skip(1))}' " +
                                    $"is not within bounds of: {lowerBound} and {upperBound}.";
                configure.Source = validationRule.ValidationExpression.Body.ToString();
            });
        }
        else
        {
            throw new ValidationUnsupportedRuleException(this.ValidationRule);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TBound"></typeparam>
    /// <param name="lowerBound"></param>
    /// <param name="upperBound"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public IValidationRuleBuilder<T, TValue> BetweenOrEqualTo<TBound>(TBound lowerBound, TBound upperBound, Action<IValidationError> configure)
        where TBound : IComparable
    {
        if (this.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            var error = new ValidationError();

            configure.Invoke(error);

            validationRule.AddRule(new BetweenOrEqualToValidationRule<T, TValue, TBound>(validationRule.ValidationExpression, lowerBound, upperBound)
            {
                Error = error
            });

            return this;
        }
        else
        {
            throw new ValidationUnsupportedRuleException(this.ValidationRule);
        }
    }


    public IValidationRuleBuilder<T, TValue> ChildRules(Action<IValidationRuleDescriptor<TValue>> child)
    {
        
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="validation"></param>
    /// <returns></returns>
    public IValidationRuleBuilder<T, TValue> Custom(Action<TValue, IValidationContext> validation)
    {
        if (this.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            validationRule.AddRule(new CustomValidationRule<T, TValue>(validationRule.ValidationExpression, validation));

            return this;
        }
        else
        {
            throw new ValidationUnsupportedRuleException(this.ValidationRule);
        }
    }

    public IValidationRuleBuilder<T, TValue> Empty()
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> Empty(Action<IValidationError> configure)
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> Equal<TValueCompare>(TValueCompare value) 
    {
        return this.Equal<TValueCompare>(value, configure =>
        {

        });
    }

    public IValidationRuleBuilder<T, TValue> Equal<TValueCompare>(TValueCompare value, Action<IValidationError> configure)
    {
        throw new NotImplementedException();

        //var error = new ValidationError();

        //configure.Invoke(error);

        //this.MemberRule.AddRule(new EqaulToValidationRule<T, TValue, TValueCompare>(MemberRule.Member, value)
        //{
        //    Error = error
        //});

        //return this;
    }

    public IValidationRuleBuilder<T, TValue> GreaterThan<TArgument>(TArgument value) 
        where TArgument : IComparable
    {
        if (this.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            return GreaterThan<TArgument>(value, error =>
            {
                error.Code = "400";
                error.Message = $"The following expression: {validationRule.ValidationExpression} must be greater than {value}.";
                error.Source = validationRule.ValidationExpression.ToString();
            });
        }
        else
        {
            throw new ValidationUnsupportedRuleException(this.ValidationRule);
        }
    }

    public IValidationRuleBuilder<T, TValue> GreaterThan<TArgument>(TArgument value, Action<IValidationError> configure) 
        where TArgument : IComparable
    {
        if (this.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            var error = new ValidationError();

            configure.Invoke(error);

            validationRule.AddRule(new GreaterThanValidationRule<T, TValue, TArgument>(validationRule.ValidationExpression, value)
            {
                Error = error
            });

            return this;
        }
        else
        {
            throw new ValidationUnsupportedRuleException(this.ValidationRule);
        }
    }

    public IValidationRuleBuilder<T, TValue> GreaterThanOrEqualTo<TNumber>(TNumber value) 
        where TNumber : IComparable
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> GreaterThanOrEqualTo<TArgument>(TArgument value, Action<IValidationError> configure) 
        where TArgument : IComparable
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> Length(int min, int max)
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> Length(int exact)
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> Length(int min, int max, Action<IValidationError> configure)
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> Length(int exact, Action<IValidationError> configure)
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> LessThan<TArgument>(TArgument value) 
        where TArgument : IComparable
    {
        if (this.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            return LessThan<TArgument>(value, error =>
            {
                error.Code = "400";
                error.Message = $"The following expression: {validationRule.ValidationExpression} must be less than {value}.";
                error.Source = validationRule.ValidationExpression.ToString();
            });
        }
        else
        {
            throw new ValidationUnsupportedRuleException(this.ValidationRule);
        }
    }

    public IValidationRuleBuilder<T, TValue> LessThan<TArgument>(TArgument value, Action<IValidationError> configure) 
        where TArgument : IComparable
    {
        if (this.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            var error = new ValidationError();

            configure.Invoke(error);

            validationRule.AddRule(new LessThanValidationRule<T, TValue, TArgument>(validationRule.ValidationExpression, value)
            {
                Error = error
            });

            return this;
        }
        else
        {
            throw new ValidationUnsupportedRuleException(this.ValidationRule);
        }
    }

    public IValidationRuleBuilder<T, TValue> LessThanOrEqualTo<TNumber>(TValue value) 
        where TNumber : IComparable
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> LessThanOrEqualTo<TArgument>(TArgument value) 
        where TArgument : IComparable
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> LessThanOrEqualTo<TArgument>(TArgument value, Action<IValidationError> configure) 
        where TArgument : IComparable
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> MaxLength(int max)
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> MaxLength(int max, Action<IValidationError> configure)
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> MinLength(int min)
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> MinLength(int min, Action<IValidationError> configure)
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> NotEmpty()
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> NotEmpty(Action<IValidationError> configure)
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> NotEqual<TNumber>(TNumber value) where TNumber : IComparable
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> NotEqual<TArgument>(TArgument value, Action<IValidationError> configure) where TArgument : IComparable
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> NotNull()
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> NotNull(Action<IValidationError> configure)
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> Null()
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> Null(Action<IValidationError> configure)
    {
        throw new NotImplementedException();
    }

    //public IValidationRuleBuilder<T, TValue> UseValidator(IValidator<TValue> validator)
    //{
    //    throw new NotImplementedException();
    //}

    public IValidationRuleBuilder<T, TValue> WithErrorCode(string code)
    {
        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> WithErrorMessage(string message)
    {
        throw new NotImplementedException();
    }
}

