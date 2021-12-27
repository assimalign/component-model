﻿using System;
using System.Linq;

namespace Assimalign.ComponentModel.Validation.Internal;

using Assimalign.ComponentModel.Validation.Properties;
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


    public IValidationRuleBuilder<T, TValue> ChildRules(Action<IValidationRuleDescriptor<TValue>> configure)
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }


        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<T, TValue> Custom(Action<TValue, IValidationContext> validation)
    {
        if (validation is null)
        {
            throw new ArgumentNullException(nameof(validation));
        }
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

    public IValidationRuleBuilder<T, TValue> Between<TBound>(TBound lowerBound, TBound upperBound)
        where TBound : IComparable
    {
        if (lowerBound is null)
        {
            throw new ArgumentNullException(nameof(lowerBound));
        }
        if(upperBound is null)
        {
            throw new ArgumentNullException(nameof(upperBound));
        }
        if (this.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            return Between<TBound>(lowerBound, upperBound, configure =>
            {
                var validationExpression = validationRule.ValidationExpression.Body.ToString();

                configure.Code = Resources.DefaultValidationErrorCode;
                configure.Message = string.Format(Resources.DefaultValidationMessageBetweenRule, validationExpression, lowerBound, upperBound);
                configure.Source = validationExpression;
            });
        }
        else
        {
            throw new ValidationUnsupportedRuleException(this.ValidationRule);
        }
    }

    public IValidationRuleBuilder<T, TValue> Between<TBound>(TBound lowerBound, TBound upperBound, Action<IValidationError> configure) 
        where TBound : IComparable
    {
        if (lowerBound is null)
        {
            throw new ArgumentNullException(nameof(lowerBound));
        }
        if (upperBound is null)
        {
            throw new ArgumentNullException(nameof(upperBound));
        }
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }
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

    public IValidationRuleBuilder<T, TValue> BetweenOrEqualTo<TBound>(TBound lowerBound, TBound upperBound)
        where TBound : IComparable
    {
        if (lowerBound is null)
        {
            throw new ArgumentNullException(nameof(lowerBound));
        }
        if (upperBound is null)
        {
            throw new ArgumentNullException(nameof(upperBound));
        }
        if (this.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            return BetweenOrEqualTo<TBound>(lowerBound, upperBound, configure =>
            {
                var validationExpression = validationRule.ValidationExpression.ToString();

                configure.Code = Resources.DefaultValidationErrorCode;
                configure.Message = string.Format(Resources.DefaultValidationMessageBetweenOrEqualToRule, validationExpression, lowerBound, upperBound);
                configure.Source = validationExpression;
            });
        }
        else
        {
            throw new ValidationUnsupportedRuleException(this.ValidationRule);
        }
    }

    public IValidationRuleBuilder<T, TValue> BetweenOrEqualTo<TBound>(TBound lowerBound, TBound upperBound, Action<IValidationError> configure)
        where TBound : IComparable
    {
        if (lowerBound is null)
        {
            throw new ArgumentNullException(nameof(lowerBound));
        }
        if (upperBound is null)
        {
            throw new ArgumentNullException(nameof(upperBound));
        }
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }
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

    public IValidationRuleBuilder<T, TValue> Empty()
    {
        if (this.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            return Empty(configure =>
            {
                var validationExpression = validationRule.ValidationExpression.ToString();

                configure.Code = Resources.DefaultValidationErrorCode;
                configure.Message = string.Format(Resources.DefaultValidationMessageEmptyRule, validationExpression);
                configure.Source = validationExpression;
            });
        }
        else
        {
            throw new ValidationUnsupportedRuleException(this.ValidationRule);
        }
    }

    public IValidationRuleBuilder<T, TValue> Empty(Action<IValidationError> configure)
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }
        if (this.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            var error = new ValidationError();

            configure.Invoke(error);

            validationRule.AddRule(new EmptyValidationRule<T, TValue>(validationRule.ValidationExpression)
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

    public IValidationRuleBuilder<T, TValue> EqualTo<TArgument>(TArgument value) 
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }
        if (this.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            return this.EqualTo<TArgument>(value, configure =>
            {
                var validationExpression = validationRule.ValidationExpression.ToString();

                configure.Code = Resources.DefaultValidationErrorCode;
                configure.Message = string.Format(Resources.DefaultValidationMessageEqualToRule, validationExpression, typeof(TArgument).IsValueType ? value : nameof(value));
                configure.Source = validationExpression;
            });
        }
        else
        {
            throw new ValidationUnsupportedRuleException(this.ValidationRule);
        }
    }

    public IValidationRuleBuilder<T, TValue> EqualTo<TArgument>(TArgument value, Action<IValidationError> configure)
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }
        if(configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }
        if (this.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            var error = new ValidationError();

            configure.Invoke(error);

            validationRule.AddRule(new EqualToValidationRule<T, TValue, TArgument>(validationRule.ValidationExpression, value)
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

    public IValidationRuleBuilder<T, TValue> GreaterThan<TArgument>(TArgument value) 
        where TArgument : IComparable
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }
        if (this.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            return GreaterThan<TArgument>(value, error =>
            {
                var validationExpression = validationRule.ValidationExpression.ToString();

                error.Code = Resources.DefaultValidationErrorCode;
                error.Message = String.Format(Resources.DefaultValidationMessageGreaterThanRule, validationExpression, value);
                error.Source = validationExpression;
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
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }
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

    public IValidationRuleBuilder<T, TValue> GreaterThanOrEqualTo<TArgument>(TArgument value) 
        where TArgument : IComparable
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }
        if (this.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            return GreaterThanOrEqualTo<TArgument>(value, error =>
            {
                var validationExpression = validationRule.ValidationExpression.ToString();

                error.Code = Resources.DefaultValidationErrorCode;
                error.Message = String.Format(Resources.DefaultValidationMessageGreaterThanOrEqualToRule, validationExpression, value);
                error.Source = validationExpression;
            });
        }
        else
        {
            throw new ValidationUnsupportedRuleException(this.ValidationRule);
        }
    }

    public IValidationRuleBuilder<T, TValue> GreaterThanOrEqualTo<TArgument>(TArgument value, Action<IValidationError> configure) 
        where TArgument : IComparable
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }
        if (this.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            var error = new ValidationError();

            configure.Invoke(error);

            validationRule.AddRule(new GreaterThanOrEqualToValidationRule<T, TValue, TArgument>(validationRule.ValidationExpression, value)
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

    public IValidationRuleBuilder<T, TValue> Length(int min, int max)
    {
        if (min > max)
        {
            throw new InvalidOperationException($"The minimum length cannot be larger than the max length.");
        }
        if (this.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            return Length(min, max, error =>
            {
                var validationExpression = validationRule.ValidationExpression.ToString();

                error.Code = Resources.DefaultValidationErrorCode;
                error.Message = String.Format(Resources.DefaultValidationMessageLengthBetweenRule, validationExpression, min, max);
                error.Source = validationExpression;
            });
        }
        else
        {
            throw new ValidationUnsupportedRuleException(this.ValidationRule);
        }
    }

    public IValidationRuleBuilder<T, TValue> Length(int min, int max, Action<IValidationError> configure)
    {
        if (min > max)
        {
            throw new InvalidOperationException($"The minimum length cannot be larger than the max length.");
        }
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }
        if (this.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            var error = new ValidationError();

            configure.Invoke(error);

            validationRule.AddRule(new LengthBetweenValidationRule<T, TValue>(validationRule.ValidationExpression, min, max)
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

    public IValidationRuleBuilder<T, TValue> Length(int exact)
    {
        if (this.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            return Length(exact, error =>
            {
                var validationExpression = validationRule.ValidationExpression.ToString();

                error.Code = Resources.DefaultValidationErrorCode;
                error.Message = String.Format(Resources.DefaultValidationMessageLengthRule, validationExpression, exact);
                error.Source = validationExpression;
            });
        }
        else
        {
            throw new ValidationUnsupportedRuleException(this.ValidationRule);
        }
    }

    public IValidationRuleBuilder<T, TValue> Length(int exact, Action<IValidationError> configure)
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }
        if (this.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            var error = new ValidationError();

            configure.Invoke(error);

            validationRule.AddRule(new LengthValidationRule<T, TValue>(validationRule.ValidationExpression, exact)
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

    public IValidationRuleBuilder<T, TValue> LessThan<TArgument>(TArgument value) 
        where TArgument : IComparable
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }
        if (this.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            return LessThan<TArgument>(value, error =>
            {
                var validationExpression = validationRule.ValidationExpression.ToString();

                error.Code = Resources.DefaultValidationErrorCode;
                error.Message = String.Format(Resources.DefaultValidationMessageLessThanRule, validationExpression, value);
                error.Source = validationExpression;
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
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }
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

    public IValidationRuleBuilder<T, TValue> LessThanOrEqualTo<TArgument>(TArgument value) 
        where TArgument : IComparable
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }
        if (this.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            return LessThan<TArgument>(value, error =>
            {
                var validationExpression = validationRule.ValidationExpression.ToString();

                error.Code = Resources.DefaultValidationErrorCode;
                error.Message = String.Format(Resources.DefaultValidationMessageLessThanOrEqualToRule, validationExpression, value);
                error.Source = validationExpression;
            });
        }
        else
        {
            throw new ValidationUnsupportedRuleException(this.ValidationRule);
        }
    }

    public IValidationRuleBuilder<T, TValue> LessThanOrEqualTo<TArgument>(TArgument value, Action<IValidationError> configure) 
        where TArgument : IComparable
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }
        if (this.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            var error = new ValidationError();

            configure.Invoke(error);

            validationRule.AddRule(new LessThanOrEqualToValidationRule<T, TValue, TArgument>(validationRule.ValidationExpression, value)
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

    public IValidationRuleBuilder<T, TValue> MaxLength(int max)
    {
        if (this.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            return MaxLength(max, error =>
            {
                var validationExpression = validationRule.ValidationExpression.ToString();

                error.Code = Resources.DefaultValidationErrorCode;
                error.Message = String.Format(Resources.DefaultValidationMessageMaxLengthRule, validationExpression, max);
                error.Source = validationExpression;
            });
        }
        else
        {
            throw new ValidationUnsupportedRuleException(this.ValidationRule);
        }
    }

    public IValidationRuleBuilder<T, TValue> MaxLength(int max, Action<IValidationError> configure)
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }
        if (this.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            var error = new ValidationError();

            configure.Invoke(error);

            validationRule.AddRule(new MaxLengthValidationRule<T, TValue>(validationRule.ValidationExpression, max)
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

    public IValidationRuleBuilder<T, TValue> MinLength(int min)
    {
        if (this.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            return MinLength(min, error =>
            {
                var validationExpression = validationRule.ValidationExpression.ToString();

                error.Code = Resources.DefaultValidationErrorCode;
                error.Message = String.Format(Resources.DefaultValidationMessageMinLengthRule, validationExpression, min);
                error.Source = validationExpression;
            });
        }
        else
        {
            throw new ValidationUnsupportedRuleException(this.ValidationRule);
        }
    }

    public IValidationRuleBuilder<T, TValue> MinLength(int min, Action<IValidationError> configure)
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }
        if (this.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            var error = new ValidationError();

            configure.Invoke(error);

            validationRule.AddRule(new MinLengthValidationRule<T, TValue>(validationRule.ValidationExpression, min)
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

    public IValidationRuleBuilder<T, TValue> NotEmpty()
    {
        if (this.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            return NotEmpty(configure =>
            {
                var validationExpression = validationRule.ValidationExpression.ToString();

                configure.Code = Resources.DefaultValidationErrorCode;
                configure.Message = string.Format(Resources.DefaultValidationMessageNotEmptyRule, validationExpression);
                configure.Source = validationExpression;
            });
        }
        else
        {
            throw new ValidationUnsupportedRuleException(this.ValidationRule);
        }
    }

    public IValidationRuleBuilder<T, TValue> NotEmpty(Action<IValidationError> configure)
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }
        if (this.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            var error = new ValidationError();

            configure.Invoke(error);

            validationRule.AddRule(new NotEmptyValidationRule<T, TValue>(validationRule.ValidationExpression)
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

    public IValidationRuleBuilder<T, TValue> NotEqualTo<TArgument>(TArgument value)
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }
        if (this.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            return this.NotEqualTo<TArgument>(value, configure =>
            {
                var validationExpression = validationRule.ValidationExpression.ToString();

                configure.Code = Resources.DefaultValidationErrorCode;
                configure.Message = string.Format(Resources.DefaultValidationMessageNotEqualToRule, validationExpression, typeof(TArgument).IsValueType ? value : nameof(value));
                configure.Source = validationExpression;
            });
        }
        else
        {
            throw new ValidationUnsupportedRuleException(this.ValidationRule);
        }
    }

    public IValidationRuleBuilder<T, TValue> NotEqualTo<TArgument>(TArgument value, Action<IValidationError> configure)
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }
        if (this.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            var error = new ValidationError();

            configure.Invoke(error);

            validationRule.AddRule(new NotEqualToValidationRule<T, TValue, TArgument>(validationRule.ValidationExpression, value)
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

    public IValidationRuleBuilder<T, TValue> NotNull()
    {
        if (this.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            return this.NotNull(configure =>
            {
                var validationExpression = validationRule.ValidationExpression.ToString();

                configure.Code = Resources.DefaultValidationErrorCode;
                configure.Message = string.Format(Resources.DefaultValidationMessageNotNullRule, validationExpression);
                configure.Source = validationExpression;
            });
        }
        else
        {
            throw new ValidationUnsupportedRuleException(this.ValidationRule);
        }
    }

    public IValidationRuleBuilder<T, TValue> NotNull(Action<IValidationError> configure)
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }
        if (this.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            var error = new ValidationError();

            configure.Invoke(error);

            validationRule.AddRule(new NotNullValidationRule<T, TValue>(validationRule.ValidationExpression)
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

    public IValidationRuleBuilder<T, TValue> Null()
    {
        if (this.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            return this.Null(configure =>
            {
                var validationExpression = validationRule.ValidationExpression.ToString();

                configure.Code = Resources.DefaultValidationErrorCode;
                configure.Message = string.Format(Resources.DefaultValidationMessageNullRule, validationExpression);
                configure.Source = validationExpression;
            });
        }
        else
        {
            throw new ValidationUnsupportedRuleException(this.ValidationRule);
        }
    }

    public IValidationRuleBuilder<T, TValue> Null(Action<IValidationError> configure)
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }
        if (this.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            var error = new ValidationError();

            configure.Invoke(error);

            validationRule.AddRule(new NullValidationRule<T, TValue>(validationRule.ValidationExpression)
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
}

