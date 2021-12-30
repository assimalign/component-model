using System;
using System.Collections;
using System.Linq;

namespace Assimalign.ComponentModel.Validation.Internal;

using Assimalign.ComponentModel.Validation.Properties;
using Assimalign.ComponentModel.Validation.Internal.Rules;
using Assimalign.ComponentModel.Validation.Internal.Exceptions;
using Assimalign.ComponentModel.Validation.Internal.Extensions;


internal sealed class ValidationRuleBuilder<T, TValue> : IValidationRuleBuilder<TValue>
{

    public ValidationRuleBuilder(IValidationItem validationItem)
    {
        this.ValidationItem = validationItem;
    }

    public IValidationItem ValidationItem { get; }

    public IValidationRuleBuilder<TValue> ChildRules(Action<IValidationRuleDescriptor<TValue>> configure)
    {
        if (configure is null)
        {
            throw new ArgumentNullException(
                paramName: nameof(configure),
                message: "The 'configure' parameter cannot be null in: ChildRules(Action<IValidationRuleDescriptor<TValue>> configure)");
        }
        if (this.ValidationItem is IValidationItem<T, TValue> validationItem)
        {


        }
        else
        {
            throw new ValidationItemUnsupportedException(this.ValidationItem);
        }


        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<TValue> Custom(Action<TValue, IValidationContext> validation)
    {
        if (validation is null)
        {
            throw new ArgumentNullException(
                paramName: nameof(validation),
                message: "The 'validation' parameter cannot be null in: Custom(Action<TValue, IValidationContext> validation)")
            {
                Source = $"RuleFor({this.ValidationItem}).Custom({validation}) or RuleForEach({this.ValidationItem}).Custom({validation})"
            };
        }

        this.ValidationItem.ItemRuleStack.Push(new CustomValidationRule<TValue>(validation));

        return this;
    }

    public IValidationRuleBuilder<TValue> Between<TBound>(TBound lowerBound, TBound upperBound)
        where TBound : notnull, IComparable
    {
        return Between<TBound>(lowerBound, upperBound, configure =>
        {
            var validationExpression = this.ValidationItem.ToString();

            configure.Code = Resources.DefaultValidationErrorCode;
            configure.Message = string.Format(Resources.DefaultValidationMessageBetweenRule, validationExpression, lowerBound, upperBound);
            configure.Source = validationExpression;
        });
    }

    public IValidationRuleBuilder<TValue> Between<TBound>(TBound lowerBound, TBound upperBound, Action<IValidationError> configure)
        where TBound : notnull, IComparable
    {
        if (configure is null)
        {
            throw new ArgumentNullException(
                paramName: nameof(configure),
                message: "The 'configure' parameter cannot be null in: Between<TBound>(TBound lowerBound, TBound upperBound, Action<IValidationError> configure)")
            {
                Source = $"RuleFor({this.ValidationItem}).Between<{typeof(TBound).Name}>({lowerBound}, {upperBound}, {configure})"
            };
        }

        var error = new ValidationError();

        configure.Invoke(error);

        this.ValidationItem.ItemRuleStack.Push(new BetweenValidationRule<TValue, TBound>(lowerBound, upperBound)
        {
            Error = error
        });

        return this;
    }

    public IValidationRuleBuilder<TValue> BetweenOrEqualTo<TBound>(TBound lowerBound, TBound upperBound)
        where TBound : notnull, IComparable
    {
        return BetweenOrEqualTo<TBound>(lowerBound, upperBound, configure =>
        {
            var validationExpression = this.ValidationItem.ToString();

            configure.Code = Resources.DefaultValidationErrorCode;
            configure.Message = string.Format(Resources.DefaultValidationMessageBetweenOrEqualToRule, validationExpression, lowerBound, upperBound);
            configure.Source = validationExpression;
        });
    }

    public IValidationRuleBuilder<TValue> BetweenOrEqualTo<TBound>(TBound lowerBound, TBound upperBound, Action<IValidationError> configure)
        where TBound : notnull, IComparable
    {
        if (lowerBound.CompareTo(upperBound) < 0)
        {

        }
        if (configure is null)
        {
            throw new ArgumentNullException(
                paramName: nameof(configure),
                message: "The 'configure' parameter cannot be null in: BetweenOrEqualTo<TBound>(TBound lowerBound, TBound upperBound, Action<IValidationError> configure)")
            {
                Source = $"RuleFor({this.ValidationItem}).BetweenOrEqualTo<{typeof(TBound).Name}>({lowerBound}, {upperBound}, {configure})"
            };
        }

        var error = new ValidationError();

        configure.Invoke(error);

        this.ValidationItem.ItemRuleStack.Push(new BetweenOrEqualToValidationRule<TValue, TBound>(lowerBound, upperBound)
        {
            Error = error
        });

        return this;

    }

    public IValidationRuleBuilder<TValue> EqualTo<TArgument>(TArgument value)
        where TArgument : notnull, IEquatable<TArgument>
    {
        return this.EqualTo<TArgument>(value, configure =>
        {
            var validationExpression = this.ValidationItem.ToString();

            configure.Code = Resources.DefaultValidationErrorCode;
            configure.Message = string.Format(Resources.DefaultValidationMessageEqualToRule, validationExpression, typeof(TArgument).IsSystemType() ? value : typeof(TArgument).Name);
            configure.Source = validationExpression;
        });
    }

    public IValidationRuleBuilder<TValue> EqualTo<TArgument>(TArgument value, Action<IValidationError> configure)
        where TArgument : notnull, IEquatable<TArgument>
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }

        var error = new ValidationError();

        configure.Invoke(error);

        this.ValidationItem.ItemRuleStack.Push(new EqualToValidationRule<TValue, TArgument>(value)
        {
            Error = error
        });

        return this;
    }

    public IValidationRuleBuilder<TValue> GreaterThan<TArgument>(TArgument value)
        where TArgument : notnull, IComparable
    {
        return GreaterThan<TArgument>(value, error =>
        {
            var validationExpression = this.ValidationItem.ToString();

            error.Code = Resources.DefaultValidationErrorCode;
            error.Message = String.Format(Resources.DefaultValidationMessageGreaterThanRule, validationExpression, value);
            error.Source = validationExpression;
        });

    }

    public IValidationRuleBuilder<TValue> GreaterThan<TArgument>(TArgument value, Action<IValidationError> configure)
        where TArgument : notnull, IComparable
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }

        var error = new ValidationError();

        configure.Invoke(error);

        this.ValidationItem.ItemRuleStack.Push(new GreaterThanValidationRule<TValue, TArgument>(value)
        {
            Error = error,
        });

        return this;
    }

    public IValidationRuleBuilder<TValue> GreaterThanOrEqualTo<TArgument>(TArgument value)
        where TArgument : notnull, IComparable
    {
        return GreaterThanOrEqualTo<TArgument>(value, error =>
        {
            var validationExpression = this.ValidationItem.ToString();

            error.Code = Resources.DefaultValidationErrorCode;
            error.Message = String.Format(Resources.DefaultValidationMessageGreaterThanOrEqualToRule, validationExpression, value);
            error.Source = validationExpression;
        });
    }

    public IValidationRuleBuilder<TValue> GreaterThanOrEqualTo<TArgument>(TArgument value, Action<IValidationError> configure)
        where TArgument : notnull, IComparable
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }

        var error = new ValidationError();

        configure.Invoke(error);

        this.ValidationItem.ItemRuleStack.Push(new GreaterThanOrEqualToValidationRule<TValue, TArgument>(value)
        {
            Error = error,
        });

        return this;
    }

    public IValidationRuleBuilder<TValue> LessThan<TArgument>(TArgument value)
        where TArgument : notnull, IComparable
    {
        return LessThan<TArgument>(value, error =>
        {
            var validationExpression = this.ValidationItem.ToString();

            error.Code = Resources.DefaultValidationErrorCode;
            error.Message = String.Format(Resources.DefaultValidationMessageLessThanRule, validationExpression, value);
            error.Source = validationExpression;
        });
    }

    public IValidationRuleBuilder<TValue> LessThan<TArgument>(TArgument value, Action<IValidationError> configure)
        where TArgument : notnull, IComparable
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }

        var error = new ValidationError();

        configure.Invoke(error);

        this.ValidationItem.ItemRuleStack.Push(new LessThanValidationRule<TValue, TArgument>(value)
        {
            Error = error
        });

        return this;
    }

    public IValidationRuleBuilder<TValue> LessThanOrEqualTo<TArgument>(TArgument value)
        where TArgument : notnull, IComparable
    {
        return LessThan<TArgument>(value, error =>
        {
            var validationExpression = this.ValidationItem.ToString();

            error.Code = Resources.DefaultValidationErrorCode;
            error.Message = String.Format(Resources.DefaultValidationMessageLessThanOrEqualToRule, validationExpression, value);
            error.Source = validationExpression;
        });
    }

    public IValidationRuleBuilder<TValue> LessThanOrEqualTo<TArgument>(TArgument value, Action<IValidationError> configure)
        where TArgument : notnull, IComparable
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }
        var error = new ValidationError();

        configure.Invoke(error);

        this.ValidationItem.ItemRuleStack.Push(new LessThanOrEqualToValidationRule<TValue, TArgument>(value)
        {
            Error = error
        });

        return this;
    }

    public IValidationRuleBuilder<TValue> NotEqualTo<TArgument>(TArgument value)
        where TArgument : notnull, IEquatable<TArgument>
    {
        return this.NotEqualTo<TArgument>(value, configure =>
        {
            var validationExpression = this.ValidationItem.ToString();

            configure.Code = Resources.DefaultValidationErrorCode;
            configure.Message = string.Format(Resources.DefaultValidationMessageNotEqualToRule, validationExpression, typeof(TArgument).IsValueType ? value : nameof(value));
            configure.Source = validationExpression;
        });
    }

    public IValidationRuleBuilder<TValue> NotEqualTo<TArgument>(TArgument value, Action<IValidationError> configure)
        where TArgument : notnull, IEquatable<TArgument>
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }

        var error = new ValidationError();

        configure.Invoke(error);

        this.ValidationItem.ItemRuleStack.Push(new NotEqualToValidationRule<TValue, TArgument>(value)
        {
            Error = error
        });

        return this;
    }

    public IValidationRuleBuilder<TValue> NotNull()
    {
        return this.NotNull(configure =>
        {
            var validationExpression = this.ValidationItem.ToString();

            configure.Code = Resources.DefaultValidationErrorCode;
            configure.Message = string.Format(Resources.DefaultValidationMessageNotNullRule, validationExpression);
            configure.Source = validationExpression;
        });
    }

    public IValidationRuleBuilder<TValue> NotNull(Action<IValidationError> configure)
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }

        var error = new ValidationError();

        configure.Invoke(error);

        this.ValidationItem.ItemRuleStack.Push(new NotNullValidationRule<T, TValue>()
        {
            Error = error,
        });

        return this;
    }

    public IValidationRuleBuilder<TValue> Null()
    {
        return this.Null(configure =>
        {
            var validationExpression = this.ValidationItem.ToString();

            configure.Code = Resources.DefaultValidationErrorCode;
            configure.Message = string.Format(Resources.DefaultValidationMessageNullRule, validationExpression);
            configure.Source = validationExpression;
        });
    }

    public IValidationRuleBuilder<TValue> Null(Action<IValidationError> configure)
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }

        var error = new ValidationError();

        configure.Invoke(error);

        this.ValidationItem.ItemRuleStack.Push(new NullValidationRule<TValue>()
        {
            Error = error
        });

        return this;
    }
}