using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation;

using Assimalign.ComponentModel.Validation.Properties;
using Assimalign.ComponentModel.Validation.Internal;
using Assimalign.ComponentModel.Validation.Internal.Rules;
using Assimalign.ComponentModel.Validation.Internal.Exceptions;
using System.Text.RegularExpressions;

/// <summary>
/// 
/// </summary>
public static partial class ValidationExtensions
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="builder"></param>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns></returns>
    public static IValidationRuleBuilder<TValue> ChildRules<TValue>(this IValidationRuleBuilder<TValue> builder, Action<IValidationRuleDescriptor<TValue>> configure)
        where TValue : class
    {
        var rule = new ChildValidationRule<TValue>()
        {
            ValidationItems = new List<IValidationItem>(),
            Name = $"Validate child members of {builder.ValidationItem}",
            Error = new ValidationError()
            {

            }
        };

        var descriptor = new ValidationRuleDescriptor<TValue>()
        {
            ValidationItems = rule.ValidationItems
        };

        configure.Invoke(descriptor);

        builder.ValidationItem.ItemRuleStack.Push(rule);

        return builder;
    }

    /// <summary>
    /// Creates a rule specifying that the member must be in the format of an email address.
    /// </summary>
    /// <param name="builder">The current instance of the validation builder.</param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    /// <exception cref="ValidationException">Is thrown when <see cref="IValidationRuleBuilder{TValue}.ValidationItem"/> is not of type <see cref="IValidationRule{TValue}"/>.</exception>
    public static IValidationRuleBuilder<string> EmailAddress(this IValidationRuleBuilder<string> builder)
    {
        return builder.EmailAddress(error =>
        {
            var validationExpression = builder.ValidationItem.ToString();

            error.Code = Resources.DefaultValidationErrorCode;
            error.Message = string.Format(Resources.DefaultValidationMessageEmailAddressRule, validationExpression);
            error.Source = validationExpression;
        });
    }

    /// <summary>
    /// Creates a rule specifying that the member must be in the format of an email address.
    /// </summary>
    /// <param name="builder">The current instance of the validation builder.</param>
    /// <param name="configure"></param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    /// <exception cref="ArgumentNullException">Is thrown when the <paramref name="configure"/> is null.</exception>
    /// <exception cref="ValidationException">Is thrown when <see cref="IValidationRuleBuilder{TValue}.ValidationItem"/> is not of type <see cref="IValidationRule{TValue}"/>.</exception>
    public static IValidationRuleBuilder<string> EmailAddress(this IValidationRuleBuilder<string> builder, Action<IValidationError> configure)
    {
        if (configure is null)
        {
            throw new ArgumentNullException(
                paramName: nameof(configure),
                message: "The 'configure' parameter cannot be null in: EmailAddress(Action<IValidationError> configure)")
            {
                Source = $"RuleFor[Each]({builder.ValidationItem}).EmailAddress({configure})."
            };
        }

        var error = new ValidationError();

        configure.Invoke(error);

        builder.ValidationItem.ItemRuleStack.Push(new EmailValidationRule<string>()
        {
            Error = error,
            Name= $"Validate {builder.ValidationItem} is valid email format"
        });

        return builder;
    }

    /// <summary>
    /// Creates a rule specifying the required minimum and maximum length of an <see cref="IEnumerable"/>.
    /// </summary>
    /// <remarks>
    /// <listheader>Examples of <see cref="IEnumerable"/> Types:</listheader>
    /// <list type="bullet">
    ///     <item><see cref="IEnumerable{T}"/></item>
    ///     <item><see cref="IDictionary{TKey, TValue}"/></item>
    ///     <item><see cref="Array"/></item>
    ///     <item><see cref="String"/></item>
    /// </list>
    /// </remarks>
    /// <param name="builder">The current instance of the validation builder.</param>
    /// <param name="min">The minimum length of the <see cref="IEnumerable"/> being validated.</param>
    /// <param name="max">The maximum length of the <see cref="IEnumerable"/> being validated.</param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    /// <exception cref="ValidationException">Is thrown when <see cref="IValidationRuleBuilder{TValue}.ValidationItem"/> is not of type <see cref="IValidationRule{TValue}"/>.</exception>
    /// <exception cref="InvalidOperationException">Is thrown when <paramref name="min"/> is greater than <paramref name="max"/>.</exception>
    public static IValidationRuleBuilder<TValue> Length<TValue>(this IValidationRuleBuilder<TValue> builder, int min, int max)
        where TValue : notnull, IEnumerable
    {
        if (min > max)
        {
            throw new InvalidOperationException($"The minimum length cannot be larger than the maximum length.")
            {
                Source = $"RuleFor({builder.ValidationItem}).Length({min},{max})"
            };
        }
        return builder.Length(min, max, error =>
        {
            var validationExpression = builder.ValidationItem.ToString();

            error.Code = Resources.DefaultValidationErrorCode;
            error.Message = String.Format(Resources.DefaultValidationMessageLengthBetweenRule, validationExpression, min, max);
            error.Source = validationExpression;
        });
    }

    /// <summary>
    /// Creates a rule specifying the required minimum and maximum length of an <see cref="IEnumerable"/> with 
    /// a configurable custom error.
    /// </summary>
    /// <remarks>
    /// <listheader>Examples of <see cref="IEnumerable"/> Types:</listheader>
    /// <list type="bullet">
    ///     <item><see cref="IEnumerable{T}"/></item>
    ///     <item><see cref="IDictionary{TKey, TValue}"/></item>
    ///     <item><see cref="Array"/></item>
    ///     <item><see cref="String"/></item>
    /// </list>
    /// </remarks>
    /// <param name="builder">The current instance of the validation builder.</param>
    /// <param name="min">The minimum length of the <see cref="IEnumerable"/> being validated.</param>
    /// <param name="max">The maximum length of the <see cref="IEnumerable"/> being validated.</param>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    /// <exception cref="ArgumentNullException">Is thrown when the <paramref name="configure"/> is null.</exception>
    /// <exception cref="ValidationException">Is thrown when <see cref="IValidationRuleBuilder{TValue}.ValidationItem"/> is not of type <see cref="IValidationRule{TValue}"/>.</exception>
    /// <exception cref="InvalidOperationException">Is thrown when <paramref name="min"/> is greater than <paramref name="max"/>.</exception>
    public static IValidationRuleBuilder<TValue> Length<TValue>(this IValidationRuleBuilder<TValue> builder, int min, int max, Action<IValidationError> configure)
        where TValue : notnull, IEnumerable
    {
        if (configure is null)
        {
            throw new ArgumentNullException(
                paramName: nameof(configure),
                message: "The 'configure' parameter cannot be null in: Length(int min, int max, Action<IValidationError> configure)")
            {
                Source = $"RuleFor[Each]({builder.ValidationItem}).Length({min}, {max}, {configure})."
            };
        }
        if (min > max)
        {
            throw new InvalidOperationException($"The minimum length cannot be larger than the maximum length.")
            {
                Source = $"RuleFor[Each]({builder.ValidationItem}).Length({min},{max}, {configure})"
            };
        }

        var error = new ValidationError();

        configure.Invoke(error);

        builder.ValidationItem.ItemRuleStack.Push(new LengthBetweenValidationRule<TValue>(min, max)
        {
            Error = error,
            Name = $"Validate {builder.ValidationItem} is within range of {min} and {max}"
        });

        return builder;
    }

    /// <summary>
    /// Creates a rule specifying the exact length of an <see cref="IEnumerable"/>.
    /// </summary>
    /// <remarks>
    /// <listheader>Examples of <see cref="IEnumerable"/> Types:</listheader>
    /// <list type="bullet">
    ///     <item><see cref="IEnumerable{T}"/></item>
    ///     <item><see cref="IDictionary{TKey, TValue}"/></item>
    ///     <item><see cref="Array"/></item>
    ///     <item><see cref="string"/></item>
    /// </list>
    /// </remarks>
    /// <param name="builder">The current instance of the validation builder.</param>
    /// <param name="exact">The required length of the <see cref="IEnumerable"/> being validated.</param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    /// <exception cref="ValidationException">Is thrown when <see cref="IValidationRuleBuilder{TValue}.ValidationItem"/> is not of type <see cref="IValidationRule{TValue}"/>.</exception>
    public static IValidationRuleBuilder<TValue> Length<TValue>(this IValidationRuleBuilder<TValue> builder, int exact)
        where TValue : notnull, IEnumerable
    {
        return builder.Length(exact, error =>
        {
            var validationExpression = builder.ValidationItem.ToString();

            error.Code = Resources.DefaultValidationErrorCode;
            error.Message = string.Format(Resources.DefaultValidationMessageLengthRule, validationExpression, exact);
            error.Source = validationExpression;
        });
    }

    /// <summary>
    /// Creates a rule specifying the exact length of an <see cref="IEnumerable"/> with 
    /// a configurable custom error.
    /// </summary>
    /// <remarks>
    /// <listheader>Examples of <see cref="IEnumerable"/> Types:</listheader>
    /// <list type="bullet">
    ///     <item><see cref="IEnumerable{T}"/></item>
    ///     <item><see cref="IDictionary{TKey, TValue}"/></item>
    ///     <item><see cref="Array"/></item>
    ///     <item><see cref="String"/></item>
    /// </list>
    /// </remarks>
    /// <param name="builder">The current instance of the validation builder.</param>
    /// <param name="exact">The required length of the <see cref="IEnumerable"/> being validated.</param>
    /// <param name="configure">A delegate to configure a custom validation error.></param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    /// </exception>
    /// <exception cref="ArgumentNullException">Is thrown when the <paramref name="configure"/> is null.</exception>
    /// <exception cref="ValidationException">Is thrown when <see cref="IValidationRuleBuilder{T, TValue}.ValidationRule"/> is not of type <see cref="IValidationRule{T, TValue}"/>.</exception>
    public static IValidationRuleBuilder<TValue> Length<TValue>(this IValidationRuleBuilder<TValue> builder, int exact, Action<IValidationError> configure)
        where TValue : notnull, IEnumerable
    {
        if (configure is null)
        {
            throw new ArgumentNullException(
                paramName: nameof(configure),
                message: "The 'configure' parameter cannot be null in: Length(int exact, Action<IValidationError> configure)")
            {
                Source = $"RuleFor[Each]({builder.ValidationItem}).Length({exact}, {configure})."
            };
        }

        var error = new ValidationError();

        configure.Invoke(error);

        builder.ValidationItem.ItemRuleStack.Push(new LengthValidationRule<TValue>(exact)
        {
            Error = error,
            Name = $"Validate {builder.ValidationItem} equals the exact range of {exact}"
        });

        return builder;
    }

    /// <summary>
    /// Creates a rule specifying the maximum length of an <see cref="IEnumerable"/>.
    /// </summary>
    /// <remarks>
    /// <listheader>Examples of <see cref="IEnumerable"/> Types:</listheader>
    /// <list type="bullet">
    ///     <item><see cref="IEnumerable{T}"/></item>
    ///     <item><see cref="IDictionary{TKey, TValue}"/></item>
    ///     <item><see cref="Array"/></item>
    ///     <item><see cref="String"/></item>
    /// </list>
    /// </remarks>
    /// <param name="builder">The current instance of the validation builder.</param>
    /// <param name="max">The maximum length of the <see cref="IEnumerable"/> being validated.</param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    /// <exception cref="ValidationException">Is thrown when <see cref="IValidationRuleBuilder{TValue}.ValidationItem"/> is not of type <see cref="IValidationRule{TValue}"/>.</exception>
    public static IValidationRuleBuilder<TValue> MaxLength<TValue>(this IValidationRuleBuilder<TValue> builder, int max)
        where TValue : notnull, IEnumerable
    {
        return builder.MaxLength(max, error =>
        {
            var validationExpression = builder.ValidationItem.ToString();

            error.Code = Resources.DefaultValidationErrorCode;
            error.Message = string.Format(Resources.DefaultValidationMessageMaxLengthRule, validationExpression, max);
            error.Source = validationExpression;
        });
    }

    /// <summary>
    /// Creates a rule specifying the maximum length of an <see cref="IEnumerable"/> with 
    /// a configurable custom error.
    /// </summary>
    /// <remarks>
    /// <listheader>Examples of <see cref="IEnumerable"/> Types:</listheader>
    /// <list type="bullet">
    ///     <item><see cref="IEnumerable{T}"/></item>
    ///     <item><see cref="IDictionary{TKey, TValue}"/></item>
    ///     <item><see cref="Array"/></item>
    ///     <item><see cref="String"/></item>
    /// </list>
    /// </remarks>
    /// <param name="builder">The current instance of the validation builder.</param>
    /// <param name="max">The maximum length of the <see cref="IEnumerable"/> being validated.</param>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    /// <exception cref="ArgumentNullException">Is thrown when the <paramref name="configure"/> is null.</exception>
    /// <exception cref="ValidationException">Is thrown when <see cref="IValidationRuleBuilder{TValue}.ValidationItem"/> is not of type <see cref="IValidationRule{TValue}"/>.</exception>
    public static IValidationRuleBuilder<TValue> MaxLength<TValue>(this IValidationRuleBuilder<TValue> builder, int max, Action<IValidationError> configure)
        where TValue : notnull, IEnumerable
    {
        if (configure is null)
        {
            throw new ArgumentNullException(
                paramName: nameof(configure),
                message: "The 'configure' parameter cannot be null in: MaxLength(int max, Action<IValidationError> configure)")
            {
                Source = $"RuleFor[Each]({builder.ValidationItem}).MaxLength({max}, {configure})."
            };
        }

        var error = new ValidationError();

        configure.Invoke(error);

        builder.ValidationItem.ItemRuleStack.Push(new LengthMaxValidationRule<TValue>(max)
        {
            Error = error,
            Name = $"Validate {builder.ValidationItem} is not outside the range of {max}"
        });

        return builder;
    }

    /// <summary>
    /// Creates a rule specifying the minimum length of an <see cref="IEnumerable"/>.
    /// </summary>
    /// <remarks>
    /// <listheader>Examples of <see cref="IEnumerable"/> Types:</listheader>
    /// <list type="bullet">
    ///     <item><see cref="IEnumerable{T}"/></item>
    ///     <item><see cref="IDictionary{TKey, TValue}"/></item>
    ///     <item><see cref="Array"/> Any type of array such as: int[], long[] </item>
    ///     <item><see cref="string"/></item>
    /// </list>
    /// </remarks>
    /// <param name="builder">The current instance of the validation builder.</param>
    /// <param name="min">The minimum length of the <see cref="IEnumerable"/> being validated.</param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    /// <exception cref="ValidationException">Is thrown when <see cref="IValidationRuleBuilder{TValue}.ValidationItem"/> is not of type <see cref="IValidationRule{TValue}"/>.</exception>
    public static IValidationRuleBuilder<TValue> MinLength<TValue>(this IValidationRuleBuilder<TValue> builder, int min)
        where TValue : notnull, IEnumerable
    {
        return builder.MinLength(min, error =>
        {
            var validationExpression = builder.ValidationItem.ToString();

            error.Code = Resources.DefaultValidationErrorCode;
            error.Message = string.Format(Resources.DefaultValidationMessageMinLengthRule, validationExpression, min);
            error.Source = validationExpression;
        });
    }

    /// <summary>
    /// Creates a rule specifying the minimum length of an <see cref="IEnumerable"/> with 
    /// a configurable custom error.
    /// </summary>
    /// <remarks>
    /// <listheader>Examples of <see cref="IEnumerable"/> Types:</listheader>
    /// <list type="bullet">
    ///     <item><see cref="IEnumerable{T}"/></item>
    ///     <item><see cref="IDictionary{TKey, TValue}"/></item>
    ///     <item><see cref="Array"/> Any type of array such as: int[], long[] </item>
    ///     <item><see cref="string"/></item>
    /// </list>
    /// </remarks>
    /// <param name="builder">The current instance of the validation builder.</param>
    /// <param name="min">The minimum length of the <see cref="IEnumerable"/> being validated.</param>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    /// <exception cref="ArgumentNullException">Is thrown when the <paramref name="configure"/> is null.</exception>
    /// <exception cref="ValidationException">Is thrown when <see cref="IValidationRuleBuilder{TValue}.ValidationItem"/> is not of type <see cref="IValidationRule{TValue}"/>.</exception>
    public static IValidationRuleBuilder<TValue> MinLength<TValue>(this IValidationRuleBuilder<TValue> builder, int min, Action<IValidationError> configure)
        where TValue : notnull, IEnumerable
    {
        if (configure is null)
        {
            throw new ArgumentNullException(
                paramName: nameof(configure),
                message: "The 'configure' parameter cannot be null in: MinLength(Action<IValidationError> configure)")
            {
                Source = $"RuleFor[Each]({builder.ValidationItem}).MinLength({min}, {configure})."
            };
        }

        var error = new ValidationError();

        configure.Invoke(error);

        builder.ValidationItem.ItemRuleStack.Push(new LengthMinValidationRule<TValue>(min)
        {
            Error = error,
            Name = $"Validate {builder.ValidationItem} is at least within range of {min}"
        });

        return builder;
    }

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must not be empty.
    /// </summary>
    /// <param name="builder">The current instance of the validation builder.</param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    /// <exception cref="ValidationException">Is thrown when <see cref="IValidationRuleBuilder{TValue}.ValidationItem"/> is not of type <see cref="IValidationRule{TValue}"/>.</exception>
    public static IValidationRuleBuilder<TValue> NotEmpty<TValue>(this IValidationRuleBuilder<TValue> builder)
        where TValue : notnull, IEnumerable
    {
        return builder.NotEmpty(configure =>
        {
            var validationExpression = builder.ValidationItem.ToString();

            configure.Code = Resources.DefaultValidationErrorCode;
            configure.Message = string.Format(Resources.DefaultValidationMessageNotEmptyRule, validationExpression);
            configure.Source = validationExpression;
        });
    }

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must not be empty with 
    /// a configurable custom error.
    /// </summary>
    /// <param name="builder">The current instance of the validation builder.</param>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    /// <exception cref="ArgumentNullException">Is thrown when the <paramref name="configure"/> is null.</exception>
    /// <exception cref="ValidationException">Is thrown when <see cref="IValidationRuleBuilder{TValue}.ValidationItem"/> is not of type <see cref="IValidationRule{TValue}"/>.</exception>
    public static IValidationRuleBuilder<TValue> NotEmpty<TValue>(this IValidationRuleBuilder<TValue> builder, Action<IValidationError> configure)
        where TValue : notnull, IEnumerable
    {
        if (configure is null)
        {
            throw new ArgumentNullException(
                paramName: nameof(configure),
                message: "The 'configure' parameter cannot be null in: NotEmpty(Action<IValidationError> configure)")
            {
                Source = $"RuleFor[Each]({builder.ValidationItem}).NotEmpty({configure})"
            };
        }

        var error = new ValidationError();

        configure.Invoke(error);

        builder.ValidationItem.ItemRuleStack.Push(new NotEmptyValidationRule<TValue>()
        {
            Error = error,
            Name = $"Validate {builder.ValidationItem} is not empty"
        });

        return builder;
    }

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must not be empty.
    /// </summary>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    /// <exception cref="ValidationException">Is thrown when <see cref="IValidationRuleBuilder{TValue}.ValidationItem"/> is not of type <see cref="IValidationRule{TValue}"/>.</exception>
    public static IValidationRuleBuilder<TValue> Empty<TValue>(this IValidationRuleBuilder<TValue> builder)
        where TValue : notnull, IEnumerable
    {
        return builder.Empty(configure =>
        {
            var validationExpression = builder.ValidationItem.ToString(); ;

            configure.Code = Resources.DefaultValidationErrorCode;
            configure.Message = string.Format(Resources.DefaultValidationMessageEmptyRule, validationExpression);
            configure.Source = validationExpression;
        });
    }

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be empty with 
    /// a configurable custom error.
    /// </summary>
    /// <param name="builder">The current instance of the validation builder.</param>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    /// <exception cref="ArgumentNullException">Is thrown when the <paramref name="configure"/> is null.</exception>
    /// <exception cref="ValidationException">Is thrown when <see cref="IValidationRuleBuilder{TValue}.ValidationItem"/> is not of type <see cref="IValidationRule{TValue}"/>.</exception>
    public static IValidationRuleBuilder<TValue> Empty<TValue>(this IValidationRuleBuilder<TValue> builder, Action<IValidationError> configure)
        where TValue : notnull, IEnumerable
    {
        if (configure is null)
        {
            throw new ArgumentNullException(
                paramName: nameof(configure),
                message: "The 'configure' parameter cannot be null in: Empty(Action<IValidationError> configure)")
            {
                Source = $"RuleFor[Each]({builder.ValidationItem}).Empty({configure})"
            };
        }

        var error = new ValidationError();

        configure.Invoke(error);

        builder.ValidationItem.ItemRuleStack.Push(new EmptyValidationRule<TValue>()
        {
            Error = error,
            Name = $"Validate {builder.ValidationItem} is empty"
        });

        return builder;
    }

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be between <paramref name="lowerBound"/> and <paramref name="upperBound"/>.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="builder"></param>
    /// <param name="lowerBound"></param>
    /// <param name="upperBound"></param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    public static IValidationRuleBuilder<TValue> Between<TValue>(this IValidationRuleBuilder<TValue> builder, TValue lowerBound, TValue upperBound)
       where TValue : struct, IComparable, IComparable<TValue>
    {
        return builder.Between<TValue>(lowerBound, upperBound, configure =>
        {
            var validationExpression = builder.ValidationItem.ToString();

            configure.Code = Resources.DefaultValidationErrorCode;
            configure.Message = string.Format(Resources.DefaultValidationMessageBetweenRule, validationExpression, lowerBound, upperBound);
            configure.Source = validationExpression;
        });
    }

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be between <paramref name="lowerBound"/> and <paramref name="upperBound"/>.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="builder"></param>
    /// <param name="lowerBound"></param>
    /// <param name="upperBound"></param>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    public static IValidationRuleBuilder<TValue> Between<TValue>(this IValidationRuleBuilder<TValue> builder, TValue lowerBound, TValue upperBound, Action<IValidationError> configure)
        where TValue : struct, IComparable, IComparable<TValue>
    {
        if (lowerBound.CompareTo(upperBound) >= 0)
        {
            throw new InvalidOperationException(
                message: $"The lower bound value cannot be greater than or equal to the upper bound value when validating 'BetweenOrEqualTo()'")
            {
                Source = $"RuleFor[Each]({builder.ValidationItem}).Between{typeof(TValue).Name}>({lowerBound}, {upperBound})"
            };
        }
        if (configure is null)
        {
            throw new ArgumentNullException(
                paramName: nameof(configure),
                message: "The 'configure' parameter cannot be null in: Between<TBound>(TBound lowerBound, TBound upperBound, Action<IValidationError> configure)")
            {
                Source = $"RuleFor[Each]({builder.ValidationItem}).Between<{typeof(TValue).Name}>({lowerBound}, {upperBound}, {configure})"
            };
        }

        var error = new ValidationError();

        configure.Invoke(error);

        builder.ValidationItem.ItemRuleStack.Push(new BetweenValidationRule<TValue>(lowerBound, upperBound)
        {
            Error = error,
            Name = $"Validate {builder.ValidationItem} is between {lowerBound} and {upperBound}"
        });

        return builder;
    }

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be between <paramref name="lowerBound"/> and <paramref name="upperBound"/>.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="builder"></param>
    /// <param name="lowerBound"></param>
    /// <param name="upperBound"></param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    public static IValidationRuleBuilder<TValue?> Between<TValue>(this IValidationRuleBuilder<TValue?> builder, TValue lowerBound, TValue upperBound)
       where TValue : struct, IComparable, IComparable<TValue>
    {
        return builder.Between<TValue>(lowerBound, upperBound, configure =>
        {
            var validationExpression = builder.ValidationItem.ToString();

            configure.Code = Resources.DefaultValidationErrorCode;
            configure.Message = string.Format(Resources.DefaultValidationMessageBetweenRule, validationExpression, lowerBound, upperBound);
            configure.Source = validationExpression;
        });
    }

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be between <paramref name="lowerBound"/> and <paramref name="upperBound"/>.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="builder"></param>
    /// <param name="lowerBound"></param>
    /// <param name="upperBound"></param>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public static IValidationRuleBuilder<TValue?> Between<TValue>(this IValidationRuleBuilder<TValue?> builder, TValue lowerBound, TValue upperBound, Action<IValidationError> configure)
        where TValue : struct, IComparable, IComparable<TValue>
    {
        if (lowerBound.CompareTo(upperBound) >= 0)
        {
            throw new InvalidOperationException(
                message: $"The lower bound value cannot be greater than or equal to the upper bound value when validating 'BetweenOrEqualTo()'")
            {
                Source = $"RuleFor[Each]({builder.ValidationItem}).Between{typeof(TValue).Name}>({lowerBound}, {upperBound})"
            };
        }
        if (configure is null)
        {
            throw new ArgumentNullException(
                paramName: nameof(configure),
                message: "The 'configure' parameter cannot be null in: Between<TBound>(TBound lowerBound, TBound upperBound, Action<IValidationError> configure)")
            {
                Source = $"RuleFor[Each]({builder.ValidationItem}).Between<{typeof(TValue).Name}>({lowerBound}, {upperBound}, {configure})"
            };
        }

        var error = new ValidationError();

        configure.Invoke(error);

        builder.ValidationItem.ItemRuleStack.Push(new BetweenValidationRule<TValue>(lowerBound, upperBound)
        {
            Error = error,
            Name = $"Validate {builder.ValidationItem} is between {lowerBound} and {upperBound}"
        });

        return builder;
    }

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be between or equal to <paramref name="lowerBound"/> and <paramref name="upperBound"/>.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="builder"></param>
    /// <param name="lowerBound"></param>
    /// <param name="upperBound"></param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    public static IValidationRuleBuilder<TValue> BetweenOrEqualTo<TValue>(this IValidationRuleBuilder<TValue> builder, TValue lowerBound, TValue upperBound)
        where TValue : struct, IComparable, IComparable<TValue>, IConvertible
    {
        return builder.BetweenOrEqualTo<TValue>(lowerBound, upperBound, configure =>
        {
            var validationExpression = builder.ValidationItem.ToString();

            configure.Code = Resources.DefaultValidationErrorCode;
            configure.Message = string.Format(Resources.DefaultValidationMessageBetweenOrEqualToRule, validationExpression, lowerBound, upperBound);
            configure.Source = validationExpression;
        });
    }

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be between or equal to <paramref name="lowerBound"/> and <paramref name="upperBound"/>.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="builder"></param>
    /// <param name="lowerBound"></param>
    /// <param name="upperBound"></param>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public static IValidationRuleBuilder<TValue> BetweenOrEqualTo<TValue>(this IValidationRuleBuilder<TValue> builder, TValue lowerBound, TValue upperBound, Action<IValidationError> configure)
        where TValue : struct, IComparable, IComparable<TValue>, IConvertible
    {
        if (lowerBound.CompareTo(upperBound) >= 0)
        {
            throw new InvalidOperationException(
                message: $"The lower bound value cannot be greater than or equal to the upper bound value when validating 'BetweenOrEqualTo()'")
            {
                Source = $"RuleFor[Each]({builder.ValidationItem}).BetweenOrEqualTo<{typeof(TValue).Name}>({lowerBound}, {upperBound})"
            };
        }
        if (configure is null)
        {
            throw new ArgumentNullException(
                paramName: nameof(configure),
                message: "The 'configure' parameter cannot be null in: BetweenOrEqualTo<TBound>(TBound lowerBound, TBound upperBound, Action<IValidationError> configure)")
            {
                Source = $"RuleFor[Each]({builder.ValidationItem}).BetweenOrEqualTo<{typeof(TValue).Name}>({lowerBound}, {upperBound}, {configure})"
            };
        }

        var error = new ValidationError();

        configure.Invoke(error);

        builder.ValidationItem.ItemRuleStack.Push(new BetweenOrEqualToValidationRule<TValue>(lowerBound, upperBound)
        {
            Error = error,
            Name = $"Validate {builder.ValidationItem} is between or equal to {lowerBound} and {upperBound}"
        });

        return builder;
    }

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be between or equal to <paramref name="lowerBound"/> and <paramref name="upperBound"/>.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="builder"></param>
    /// <param name="lowerBound"></param>
    /// <param name="upperBound"></param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    public static IValidationRuleBuilder<TValue?> BetweenOrEqualTo<TValue>(this IValidationRuleBuilder<TValue?> builder, TValue lowerBound, TValue upperBound)
        where TValue : struct, IComparable, IComparable<TValue>, IConvertible
    {
        return builder.BetweenOrEqualTo<TValue>(lowerBound, upperBound, configure =>
        {
            var validationExpression = builder.ValidationItem.ToString();

            configure.Code = Resources.DefaultValidationErrorCode;
            configure.Message = string.Format(Resources.DefaultValidationMessageBetweenOrEqualToRule, validationExpression, lowerBound, upperBound);
            configure.Source = validationExpression;
        });
    }

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be between or equal to <paramref name="lowerBound"/> and <paramref name="upperBound"/>.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="builder"></param>
    /// <param name="lowerBound"></param>
    /// <param name="upperBound"></param>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public static IValidationRuleBuilder<TValue?> BetweenOrEqualTo<TValue>(this IValidationRuleBuilder<TValue?> builder, TValue lowerBound, TValue upperBound, Action<IValidationError> configure)
        where TValue : struct, IComparable, IComparable<TValue>, IConvertible
    {
        if (lowerBound.CompareTo(upperBound) >= 0)
        {
            throw new InvalidOperationException(
                message: $"The lower bound value cannot be greater than or equal to the upper bound value when validating 'BetweenOrEqualTo()'")
            {
                Source = $"RuleFor[Each]({builder.ValidationItem}).BetweenOrEqualTo<{typeof(TValue).Name}>({lowerBound}, {upperBound})"
            };
        }
        if (configure is null)
        {
            throw new ArgumentNullException(
                paramName: nameof(configure),
                message: "The 'configure' parameter cannot be null in: BetweenOrEqualTo<TBound>(TBound lowerBound, TBound upperBound, Action<IValidationError> configure)")
            {
                Source = $"RuleFor[Each]({builder.ValidationItem}).BetweenOrEqualTo<{typeof(TValue).Name}>({lowerBound}, {upperBound}, {configure})"
            };
        }

        var error = new ValidationError();

        configure.Invoke(error);

        builder.ValidationItem.ItemRuleStack.Push(new BetweenOrEqualToValidationRule<TValue>(lowerBound, upperBound)
        {
            Error = error,
            Name = $"Validate {builder.ValidationItem} is between or equal to {lowerBound} and {upperBound}"
        });

        return builder;
    }

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be greater than <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="builder"></param>
    /// <param name="value"></param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    public static IValidationRuleBuilder<TValue> GreaterThan<TValue>(this IValidationRuleBuilder<TValue> builder, TValue value)
        where TValue : struct, IComparable, IComparable<TValue>
    {
        return builder.GreaterThan<TValue>(value, error =>
        {
            var validationExpression = builder.ValidationItem.ToString();

            error.Code = Resources.DefaultValidationErrorCode;
            error.Message = String.Format(Resources.DefaultValidationMessageGreaterThanRule, validationExpression, value);
            error.Source = validationExpression;
        });
    }

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be greater than <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="builder"></param>
    /// <param name="value"></param>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IValidationRuleBuilder<TValue> GreaterThan<TValue>(this IValidationRuleBuilder<TValue> builder, TValue value, Action<IValidationError> configure)
        where TValue : struct, IComparable, IComparable<TValue>
    {
        if (configure is null)
        {
            throw new ArgumentNullException(
                paramName: nameof(configure),
                message: "The 'configure' parameter cannot be null in: GreaterThan<TValue>(this IValidationRuleBuilder<TValue> builder, TValue value, Action<IValidationError> configure)")
            {
                Source = $"RuleFor[Each]({builder.ValidationItem}).GreaterThan<{typeof(TValue).Name}>({value}, {configure})"
            };
        }

        var error = new ValidationError();

        configure.Invoke(error);

        builder.ValidationItem.ItemRuleStack.Push(new GreaterThanValidationRule<TValue>(value)
        {
            Error = error,
            Name = $"Validate {builder.ValidationItem} is greater than {value}"
        });

        return builder;
    }

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be greater than <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="builder"></param>
    /// <param name="value"></param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    public static IValidationRuleBuilder<TValue?> GreaterThan<TValue>(this IValidationRuleBuilder<TValue?> builder, TValue value)
        where TValue : struct, IComparable, IComparable<TValue>
    {
        return builder.GreaterThan<TValue>(value, error =>
        {
            var validationExpression = builder.ValidationItem.ToString();

            error.Code = Resources.DefaultValidationErrorCode;
            error.Message = String.Format(Resources.DefaultValidationMessageGreaterThanRule, validationExpression, value);
            error.Source = validationExpression;
        });
    }

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be greater than <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="builder"></param>
    /// <param name="value"></param>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IValidationRuleBuilder<TValue?> GreaterThan<TValue>(this IValidationRuleBuilder<TValue?> builder, TValue value, Action<IValidationError> configure)
        where TValue : struct, IComparable, IComparable<TValue>
    {
        if (configure is null)
        {
            throw new ArgumentNullException(
                paramName: nameof(configure),
                message: "The 'configure' parameter cannot be null in: GreaterThan<TValue>(this IValidationRuleBuilder<TValue> builder, TValue value, Action<IValidationError> configure)")
            {
                Source = $"RuleFor[Each]({builder.ValidationItem}).GreaterThan<{typeof(TValue).Name}>({value}, {configure})"
            };
        }

        var error = new ValidationError();

        configure.Invoke(error);

        builder.ValidationItem.ItemRuleStack.Push(new GreaterThanValidationRule<TValue>(value)
        {
            Error = error,
            Name = $"Validate {builder.ValidationItem} is greater than {value}"
        });

        return builder;
    }

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be greater than or equal to <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="builder"></param>
    /// <param name="value"></param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    public static IValidationRuleBuilder<TValue> GreaterThanOrEqualTo<TValue>(this IValidationRuleBuilder<TValue> builder, TValue value)
       where TValue : struct, IComparable, IComparable<TValue>
    {
        return builder.GreaterThanOrEqualTo<TValue>(value, error =>
        {
            var validationExpression = builder.ValidationItem.ToString();

            error.Code = Resources.DefaultValidationErrorCode;
            error.Message = String.Format(Resources.DefaultValidationMessageGreaterThanOrEqualToRule, validationExpression, value);
            error.Source = validationExpression;
        });
    }

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be greater than or equal to <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="builder"></param>
    /// <param name="value"></param>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IValidationRuleBuilder<TValue> GreaterThanOrEqualTo<TValue>(this IValidationRuleBuilder<TValue> builder, TValue value, Action<IValidationError> configure)
       where TValue : struct, IComparable, IComparable<TValue>
    {
        if (configure is null)
        {
            throw new ArgumentNullException(
                paramName: nameof(configure),
                message: "The 'configure' parameter cannot be null in: GreaterThanOrEqualTo<TValue>(this IValidationRuleBuilder<TValue> builder, TValue value, Action<IValidationError> configure)")
            {
                Source = $"RuleFor[Each]({builder.ValidationItem}).GreaterThan<{typeof(TValue).Name}>({value}, {configure})"
            };
        }

        var error = new ValidationError();

        configure.Invoke(error);

        builder.ValidationItem.ItemRuleStack.Push(new GreaterThanOrEqualToValidationRule<TValue>(value)
        {
            Error = error,
            Name = $"Validate {builder.ValidationItem} greater than or equal to {value}"
        });

        return builder;
    }

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be greater than or equal to <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="builder"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static IValidationRuleBuilder<TValue?> GreaterThanOrEqualTo<TValue>(this IValidationRuleBuilder<TValue?> builder, TValue value)
       where TValue : struct, IComparable, IComparable<TValue>
    {
        return builder.GreaterThanOrEqualTo<TValue>(value, error =>
        {
            var validationExpression = builder.ValidationItem.ToString();

            error.Code = Resources.DefaultValidationErrorCode;
            error.Message = String.Format(Resources.DefaultValidationMessageGreaterThanOrEqualToRule, validationExpression, value);
            error.Source = validationExpression;
        });
    }

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be greater than or equal to <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="builder"></param>
    /// <param name="value"></param>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IValidationRuleBuilder<TValue?> GreaterThanOrEqualTo<TValue>(this IValidationRuleBuilder<TValue?> builder, TValue value, Action<IValidationError> configure)
       where TValue : struct, IComparable, IComparable<TValue>
    {
        if (configure is null)
        {
            throw new ArgumentNullException(
                paramName: nameof(configure),
                message: "The 'configure' parameter cannot be null in: GreaterThanOrEqualTo<TValue>(this IValidationRuleBuilder<TValue> builder, TValue value, Action<IValidationError> configure)")
            {
                Source = $"RuleFor[Each]({builder.ValidationItem}).GreaterThan<{typeof(TValue).Name}>({value}, {configure})"
            };
        }

        var error = new ValidationError();

        configure.Invoke(error);

        builder.ValidationItem.ItemRuleStack.Push(new GreaterThanOrEqualToValidationRule<TValue>(value)
        {
            Error = error,
            Name = $"Validate {builder.ValidationItem} greater than or equal to {value}"
        });

        return builder;
    }

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be less than <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="builder"></param>
    /// <param name="value"></param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    public static IValidationRuleBuilder<TValue> LessThan<TValue>(this IValidationRuleBuilder<TValue> builder, TValue value)
        where TValue : struct, IComparable, IComparable<TValue>
    {
        return builder.LessThan<TValue>(value, error =>
        {
            var validationExpression = builder.ValidationItem.ToString();

            error.Code = Resources.DefaultValidationErrorCode;
            error.Message = String.Format(Resources.DefaultValidationMessageLessThanRule, validationExpression, value);
            error.Source = validationExpression;
        });
    }

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be less than <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="builder"></param>
    /// <param name="value"></param>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IValidationRuleBuilder<TValue> LessThan<TValue>(this IValidationRuleBuilder<TValue> builder, TValue value, Action<IValidationError> configure)
        where TValue : struct, IComparable, IComparable<TValue>
    {
        if (configure is null)
        {
            throw new ArgumentNullException(
                paramName: nameof(configure),
                message: "The 'configure' parameter cannot be null in: LessThan<TValue>(this IValidationRuleBuilder<TValue> builder, TValue value, Action<IValidationError> configure)")
            {
                Source = $"RuleFor[Each]({builder.ValidationItem}).LessThan<{typeof(TValue).Name}>({value}, {configure})"
            };
        }

        var error = new ValidationError();

        configure.Invoke(error);

        builder.ValidationItem.ItemRuleStack.Push(new LessThanValidationRule<TValue>(value)
        {
            Error = error,
            Name = $"Validate {builder.ValidationItem} is less than {value}"
        });

        return builder;
    }

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be less than <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="builder"></param>
    /// <param name="value"></param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    public static IValidationRuleBuilder<TValue?> LessThan<TValue>(this IValidationRuleBuilder<TValue?> builder, TValue value)
        where TValue : struct, IComparable, IComparable<TValue>
    {
        return builder.LessThan<TValue>(value, error =>
        {
            var validationExpression = builder.ValidationItem.ToString();

            error.Code = Resources.DefaultValidationErrorCode;
            error.Message = String.Format(Resources.DefaultValidationMessageLessThanRule, validationExpression, value);
            error.Source = validationExpression;
        });
    }

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be less than <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="builder"></param>
    /// <param name="value"></param>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IValidationRuleBuilder<TValue?> LessThan<TValue>(this IValidationRuleBuilder<TValue?> builder, TValue value, Action<IValidationError> configure)
        where TValue : struct, IComparable, IComparable<TValue>
    {
        if (configure is null)
        {
            throw new ArgumentNullException(
                paramName: nameof(configure),
                message: "The 'configure' parameter cannot be null in: LessThan<TValue>(this IValidationRuleBuilder<TValue> builder, TValue value, Action<IValidationError> configure)")
            {
                Source = $"RuleFor[Each]({builder.ValidationItem}).LessThan<{typeof(TValue).Name}>({value}, {configure})"
            };
        }

        var error = new ValidationError();

        configure.Invoke(error);

        builder.ValidationItem.ItemRuleStack.Push(new LessThanValidationRule<TValue>(value)
        {
            Error = error,
            Name = $"Validate {builder.ValidationItem} is less than {value}"
        });

        return builder;
    }

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be less than or equal to <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="builder"></param>
    /// <param name="value"></param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    public static IValidationRuleBuilder<TValue> LessThanOrEqualTo<TValue>(this IValidationRuleBuilder<TValue> builder, TValue value)
       where TValue : struct, IComparable, IComparable<TValue>
    {
        return builder.LessThan<TValue>(value, error =>
        {
            var validationExpression = builder.ValidationItem.ToString();

            error.Code = Resources.DefaultValidationErrorCode;
            error.Message = String.Format(Resources.DefaultValidationMessageLessThanOrEqualToRule, validationExpression, value);
            error.Source = validationExpression;
        });
    }

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be less than or equal to <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="builder"></param>
    /// <param name="value"></param>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IValidationRuleBuilder<TValue> LessThanOrEqualTo<TValue>(this IValidationRuleBuilder<TValue> builder, TValue value, Action<IValidationError> configure)
        where TValue : struct, IComparable, IComparable<TValue>
    {
        if (configure is null)
        {
            throw new ArgumentNullException(
                paramName: nameof(configure),
                message: "The 'configure' parameter cannot be null in: LessThanOrEqualTo<TValue>(this IValidationRuleBuilder<TValue> builder, TValue value, Action<IValidationError> configure)")
            {
                Source = $"RuleFor[Each]({builder.ValidationItem}).LessThanOrEqualTo<{typeof(TValue).Name}>({value}, {configure})"
            };
        }
        var error = new ValidationError();

        configure.Invoke(error);

        builder.ValidationItem.ItemRuleStack.Push(new LessThanOrEqualToValidationRule<TValue>(value)
        {
            Error = error,
            Name = $"Validate {builder.ValidationItem} is less than or equal to {value}"
        });

        return builder;
    }

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be less than or equal to <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="builder"></param>
    /// <param name="value"></param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    public static IValidationRuleBuilder<TValue?> LessThanOrEqualTo<TValue>(this IValidationRuleBuilder<TValue?> builder, TValue value)
       where TValue : struct, IComparable, IComparable<TValue>
    {
        return builder.LessThan<TValue>(value, error =>
        {
            var validationExpression = builder.ValidationItem.ToString();

            error.Code = Resources.DefaultValidationErrorCode;
            error.Message = String.Format(Resources.DefaultValidationMessageLessThanOrEqualToRule, validationExpression, value);
            error.Source = validationExpression;
        });
    }

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be less than or equal to <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="builder"></param>
    /// <param name="value"></param>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IValidationRuleBuilder<TValue?> LessThanOrEqualTo<TValue>(this IValidationRuleBuilder<TValue?> builder, TValue value, Action<IValidationError> configure)
        where TValue : struct, IComparable, IComparable<TValue>
    {
        if (configure is null)
        {
            throw new ArgumentNullException(
                paramName: nameof(configure),
                message: "The 'configure' parameter cannot be null in: LessThanOrEqualTo<TValue>(this IValidationRuleBuilder<TValue> builder, TValue value, Action<IValidationError> configure)")
            {
                Source = $"RuleFor[Each]({builder.ValidationItem}).LessThanOrEqualTo<{typeof(TValue).Name}>({value}, {configure})"
            };
        }
        var error = new ValidationError();

        configure.Invoke(error);

        builder.ValidationItem.ItemRuleStack.Push(new LessThanOrEqualToValidationRule<TValue>(value)
        {
            Error = error,
            Name = $"Validate {builder.ValidationItem} is less than or equal to {value}"
        });

        return builder;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="pattern"></param>
    /// <returns></returns>
    public static IValidationRuleBuilder<string> Matches(this IValidationRuleBuilder<string> builder, string pattern)
    {
        if (string.IsNullOrEmpty(pattern))
        {
            throw new ArgumentNullException(
                paramName: nameof(pattern),
                message: "The 'pattern' parameter cannot be null in: Matches<string>(this IValidationRuleBuilder<string> builder, string pattern)")
            {
                Source = $"RuleFor[Each]({builder.ValidationItem}).Matches<string>({pattern})"
            };
        }

        return builder.Matches(pattern, error =>
        {
            var validationExpression = builder.ValidationItem.ToString();

            error.Code = Resources.DefaultValidationErrorCode;
            error.Message = String.Format(Resources.DefaultValidationMessageMatchesRule, validationExpression, pattern);
            error.Source = validationExpression;
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="pattern"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IValidationRuleBuilder<string> Matches(this IValidationRuleBuilder<string> builder, string pattern, Action<IValidationError> configure)
    {
        if (string.IsNullOrEmpty(pattern))
        {
            throw new ArgumentNullException(
                paramName: nameof(pattern),
                message: "The 'pattern' parameter cannot be null in: Matches<string>(this IValidationRuleBuilder<string> builder, string pattern, Action<IValidationError> configure)")
            {
                Source = $"RuleFor[Each]({builder.ValidationItem}).Matches<string>({pattern}, {configure})"
            };
        }
        if (configure is null)
        {
            throw new ArgumentNullException(
                paramName: nameof(configure),
                message: "The 'configure' parameter cannot be null in: Matches<string>(this IValidationRuleBuilder<string> builder, string pattern, Action<IValidationError> configure)")
            {
                Source = $"RuleFor[Each]({builder.ValidationItem}).Matches<string>({pattern}, {configure})"
            };
        }
        var error = new ValidationError();

        configure.Invoke(error);

        builder.ValidationItem.ItemRuleStack.Push(new MatchValidationRule(pattern)
        {
            Error = error,
            Name = $"Validate {builder.ValidationItem} must match pattern: {pattern}"
        });

        return builder;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="pattern"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static IValidationRuleBuilder<string> Matches(this IValidationRuleBuilder<string> builder, string pattern, RegexOptions options)
    {
        if (string.IsNullOrEmpty(pattern))
        {
            throw new ArgumentNullException(
                paramName: nameof(pattern),
                message: "The 'pattern' parameter cannot be null in: Matches<string>(this IValidationRuleBuilder<string> builder, string pattern, RegexOptions options)")
            {
                Source = $"RuleFor[Each]({builder.ValidationItem}).Matches<string>({pattern}, {options})"
            };
        }
        return builder.Matches(pattern, options, error =>
        {
            var validationExpression = builder.ValidationItem.ToString();

            error.Code = Resources.DefaultValidationErrorCode;
            error.Message = String.Format(Resources.DefaultValidationMessageMatchesRule, validationExpression, pattern);
            error.Source = validationExpression;
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="pattern"></param>
    /// <param name="options"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IValidationRuleBuilder<string> Matches(this IValidationRuleBuilder<string> builder, string pattern, RegexOptions options, Action<IValidationError> configure)
    {
        if (string.IsNullOrEmpty(pattern))
        {
            throw new ArgumentNullException(
                paramName: nameof(pattern),
                message: "The 'pattern' parameter cannot be null in: Matches<string>(this IValidationRuleBuilder<string> builder, string pattern, RegexOptions options, RegexOptions options)")
            {
                Source = $"RuleFor[Each]({builder.ValidationItem}).Matches<string>({pattern}, {options}, {configure})"
            };
        }
        if (configure is null)
        {
            throw new ArgumentNullException(
                paramName: nameof(configure),
                message: "The 'configure' parameter cannot be null in: Matches<string>(this IValidationRuleBuilder<string> builder, string pattern, RegexOptions options, Action<IValidationError> configure)")
            {
                Source = $"RuleFor[Each]({builder.ValidationItem}).Matches<string>({pattern}, {options}, {configure})"
            };
        }
        var error = new ValidationError();

        configure.Invoke(error);

        builder.ValidationItem.ItemRuleStack.Push(new MatchValidationRule(pattern, options)
        {
            Error = error,
            Name = $"Validate {builder.ValidationItem} must match pattern: {pattern}"
        });

        return builder;
    }
}