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

    public IValidationRuleBuilder<TValue> EqualTo(TValue value)
    {
        return this.EqualTo(value, configure =>
        {
            var validationExpression = this.ValidationItem.ToString();

            configure.Code = Resources.DefaultValidationErrorCode;
            configure.Message = string.Format(Resources.DefaultValidationMessageEqualToRule, validationExpression, typeof(TValue).IsSystemType(false) ? value : typeof(TValue).Name);
            configure.Source = validationExpression;
        });
    }

    public IValidationRuleBuilder<TValue> EqualTo(TValue value, Action<IValidationError> configure)
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

        this.ValidationItem.ItemRuleStack.Push(new EqualToValidationRule<TValue, TValue>(value)
        {
            Error = error,
            Name = $"Validate {this.ValidationItem} is equal to {value}"
        });

        return this;
    }

    public IValidationRuleBuilder<TValue> NotEqualTo(TValue value)
    {
        return this.NotEqualTo(value, configure =>
        {
            var validationExpression = this.ValidationItem.ToString();

            configure.Code = Resources.DefaultValidationErrorCode;
            configure.Message = string.Format(Resources.DefaultValidationMessageNotEqualToRule, validationExpression, typeof(TValue).IsValueType ? value : typeof(TValue).Name);
            configure.Source = validationExpression;
        });
    }

    public IValidationRuleBuilder<TValue> NotEqualTo(TValue value, Action<IValidationError> configure)
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

        this.ValidationItem.ItemRuleStack.Push(new NotEqualToValidationRule<TValue, TValue>(value)
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