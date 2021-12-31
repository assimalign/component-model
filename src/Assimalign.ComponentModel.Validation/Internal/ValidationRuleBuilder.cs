using System;

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
                Source = $"RuleFor[Each]({this.ValidationItem}).Custom({validation})"
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
        if (lowerBound.CompareTo(upperBound) >= 0)
        {
            throw new InvalidOperationException(
                message: $"The lower bound value cannot be greater than or equal to the upper bound value when validating 'BetweenOrEqualTo()'")
            {
                Source = $"RuleFor[Each]({this.ValidationItem}).Between{typeof(TValue).Name}>({lowerBound}, {upperBound})"
            };
        }
        if (configure is null)
        {
            throw new ArgumentNullException(
                paramName: nameof(configure),
                message: "The 'configure' parameter cannot be null in: Between<TBound>(TBound lowerBound, TBound upperBound, Action<IValidationError> configure)")
            {
                Source = $"RuleFor[Each]({this.ValidationItem}).Between<{typeof(TValue).Name}>({lowerBound}, {upperBound}, {configure})"
            };
        }

        var error = new ValidationError();

        configure.Invoke(error);

        this.ValidationItem.ItemRuleStack.Push(new BetweenValidationRule<TValue, TBound>(lowerBound, upperBound)
        {
            Error = error,
            Name = $"Validate {this.ValidationItem} is between {lowerBound} and {upperBound}"
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
        if (lowerBound.CompareTo(upperBound) >= 0)
        {
            throw new InvalidOperationException(
                message: $"The lower bound value cannot be greater than or equal to the upper bound value when validating 'BetweenOrEqualTo()'")
            {
                Source = $"RuleFor[Each]({this.ValidationItem}).BetweenOrEqualTo<{typeof(TValue).Name}>({lowerBound}, {upperBound})"
            };
        }
        if (configure is null)
        {
            throw new ArgumentNullException(
                paramName: nameof(configure),
                message: "The 'configure' parameter cannot be null in: BetweenOrEqualTo<TBound>(TBound lowerBound, TBound upperBound, Action<IValidationError> configure)")
            {
                Source = $"RuleFor[Each]({this.ValidationItem}).BetweenOrEqualTo<{typeof(TValue).Name}>({lowerBound}, {upperBound}, {configure})"
            };
        }

        var error = new ValidationError();

        configure.Invoke(error);

        this.ValidationItem.ItemRuleStack.Push(new BetweenOrEqualToValidationRule<TValue, TBound>(lowerBound, upperBound)
        {
            Error = error,
            Name = $"Validate {this.ValidationItem} is between or equal to {lowerBound} and {upperBound}"
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
            configure.Message = string.Format(Resources.DefaultValidationMessageEqualToRule, validationExpression, typeof(TArgument).IsSystemType(false) ? value : typeof(TArgument).Name);
            configure.Source = validationExpression;
        });
    }

    public IValidationRuleBuilder<TValue> EqualTo<TArgument>(TArgument value, Action<IValidationError> configure)
        where TArgument : notnull, IEquatable<TArgument>
    {
        if (configure is null)
        {
            throw new ArgumentNullException(
                paramName: nameof(configure),
                message: "The 'configure' parameter cannot be null in: EqualtTo<TArgument>(TArgument value, Action<IValidationError> configure)")
            {
                Source = $"RuleFor[Each]({this.ValidationItem}).EqualTo<{typeof(TValue).Name}>({value}, {configure})"
            };
        }

        var error = new ValidationError();

        configure.Invoke(error);

        this.ValidationItem.ItemRuleStack.Push(new EqualToValidationRule<TValue, TArgument>(value)
        {
            Error = error,
            Name = $"Validate {this.ValidationItem} is equal to {value}"
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
            throw new ArgumentNullException(
                paramName: nameof(configure),
                message: "The 'configure' parameter cannot be null in: GreaterThan<TArgument>(TArgument value, Action<IValidationError> configure)")
            {
                Source = $"RuleFor[Each]({this.ValidationItem}).GreaterThan<{typeof(TValue).Name}>({value}, {configure})"
            };
        }

        var error = new ValidationError();

        configure.Invoke(error);

        this.ValidationItem.ItemRuleStack.Push(new GreaterThanValidationRule<TValue, TArgument>(value)
        {
            Error = error,
            Name = $"Validate {this.ValidationItem} is greater than {value}"
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
            throw new ArgumentNullException(
                paramName: nameof(configure),
                message: "The 'configure' parameter cannot be null in: GreaterThanOrEqualTo<TArgument>(TArgument value, Action<IValidationError> configure)")
            {
                Source = $"RuleFor[Each]({this.ValidationItem}).GreaterThan<{typeof(TValue).Name}>({value}, {configure})"
            };
        }

        var error = new ValidationError();

        configure.Invoke(error);

        this.ValidationItem.ItemRuleStack.Push(new GreaterThanOrEqualToValidationRule<TValue, TArgument>(value)
        {
            Error = error,
            Name = $"Validate {this.ValidationItem} greater than or equal to {value}"
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
            throw new ArgumentNullException(
                paramName: nameof(configure),
                message: "The 'configure' parameter cannot be null in: LessThan<TArgument>(TArgument value, Action<IValidationError> configure)")
            {
                Source = $"RuleFor[Each]({this.ValidationItem}).LessThan<{typeof(TValue).Name}>({value}, {configure})"
            };
        }

        var error = new ValidationError();

        configure.Invoke(error);

        this.ValidationItem.ItemRuleStack.Push(new LessThanValidationRule<TValue, TArgument>(value)
        {
            Error = error,
            Name = $"Validate {this.ValidationItem} is less than {value}"
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
            throw new ArgumentNullException(
                paramName: nameof(configure),
                message: "The 'configure' parameter cannot be null in: LessThanOrEqualTo<TArgument>(TArgument value, Action<IValidationError> configure)")
            {
                Source = $"RuleFor[Each]({this.ValidationItem}).LessThanOrEqualTo<{typeof(TValue).Name}>({value}, {configure})"
            };
        }
        var error = new ValidationError();

        configure.Invoke(error);

        this.ValidationItem.ItemRuleStack.Push(new LessThanOrEqualToValidationRule<TValue, TArgument>(value)
        {
            Error = error,
            Name = $"Validate {this.ValidationItem} is less than or equal to {value}"
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
            throw new ArgumentNullException(
                paramName: nameof(configure),
                message: "The 'configure' parameter cannot be null in: NotEqualTo<TArgument>(TArgument value, Action<IValidationError> configure)")
            {
                Source = $"RuleFor[Each]({this.ValidationItem}).NotEqulTo<{typeof(TValue).Name}>({value}, {configure})"
            };
        }

        var error = new ValidationError();

        configure.Invoke(error);

        this.ValidationItem.ItemRuleStack.Push(new NotEqualToValidationRule<TValue, TArgument>(value)
        {
            Error = error,
            Name = $"Validate {this.ValidationItem} is not equal to {value}"
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
            throw new ArgumentNullException(
                paramName: nameof(configure),
                message: "The 'configure' parameter cannot be null in: NotNull(Action<IValidationError> configure)")
            {
                Source = $"RuleFor[Each]({this.ValidationItem}).NotNull({configure})"
            };
        }
        var error = new ValidationError();

        configure.Invoke(error);

        this.ValidationItem.ItemRuleStack.Push(new NotNullValidationRule<T, TValue>()
        {
            Error = error,
            Name = $"Validate {this.ValidationItem} is not null"
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
            throw new ArgumentNullException(
                paramName: nameof(configure),
                message: "The 'configure' parameter cannot be null in: Null(Action<IValidationError> configure)")
            {
                Source = $"RuleFor[Each]({this.ValidationItem}).Null({configure})"
            };
        }

        var error = new ValidationError();

        configure.Invoke(error);

        this.ValidationItem.ItemRuleStack.Push(new NullValidationRule<TValue>()
        {
            Error = error,
            Name = $"Validate {typeof(T).Name}.{this.ValidationItem} is null"
        });

        return this;
    }
}