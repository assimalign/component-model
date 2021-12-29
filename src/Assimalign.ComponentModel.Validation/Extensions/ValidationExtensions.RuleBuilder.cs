using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation;

using Assimalign.ComponentModel.Validation.Properties;
using Assimalign.ComponentModel.Validation.Internal.Rules;
using Assimalign.ComponentModel.Validation.Internal.Exceptions;

/// <summary>
/// 
/// </summary>
public static partial class ValidationExtensions
{
    /// <summary>
    /// Creates a rule specifying that the member must be in the format of an email address.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="builder">The current instance of the validation builder.</param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    /// <exception cref="ValidationException">Is thrown when <see cref="IValidationRuleBuilder{T, TValue}.ValidationRule"/> is not of type <see cref="IValidationRule{T, TValue}"/>.</exception>
    public static IValidationRuleBuilder<string> EmailAddress<T>(this IValidationRuleBuilder<string> builder)
    {
        if (builder.ValidationRule is IValidationRule<T, string> validationRule)
        {
            return builder.EmailAddress(error =>
            {
                var validationExpression = validationRule.ValidationExpression.ToString();

                error.Code = Resources.DefaultValidationErrorCode;
                error.Message = string.Format(Resources.DefaultValidationMessageEmailAddressRule, validationExpression);
                error.Source = validationExpression;
            });
        }
        else
        {
            throw new ValidationUnsupportedRuleException(builder.ValidationRule);
        }
    }

    /// <summary>
    /// Creates a rule specifying that the member must be in the format of an email address.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="builder">The current instance of the validation builder.</param>
    /// <param name="configure"></param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    /// <exception cref="ArgumentNullException">Is thrown when the <paramref name="configure"/> is null.</exception>
    /// <exception cref="ValidationException">Is thrown when <see cref="IValidationRuleBuilder{T, TValue}.ValidationRule"/> is not of type <see cref="IValidationRule{T, TValue}"/>.</exception>
    public static IValidationRuleBuilder<string> EmailAddress<T>(this IValidationRuleBuilder<string> builder, Action<IValidationError> configure)
    {
        if (builder.ValidationRule is IValidationRule<T, string> validationRule)
        {
            if (configure is null)
            {
                throw new ArgumentNullException(
                    paramName: nameof(configure),
                    message: "The 'configure' parameter cannot be null in: EmailAddress(Action<IValidationError> configure)")
                {
                    Source = $"RuleFor({validationRule.ValidationExpression}).EmailAddress({configure})."
                };
            }

            var error = new ValidationError();

            configure.Invoke(error);

            validationRule.AddRule(new EmailValidationRule<T, string>(validationRule.ValidationExpression)
            {
                Error = error
            });

            return builder;
        }
        else
        {
            throw new ValidationUnsupportedRuleException(builder.ValidationRule);
        }
    }

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be in the format of an email address.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="builder">The current instance of the validation builder.</param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    /// <exception cref="ValidationException">Is thrown when <see cref="IValidationRuleBuilder{T, TValue}.ValidationRule"/> is not of type <see cref="IValidationRule{T, TValue}"/>.</exception>
    public static IValidationRuleBuilder<TValue> EmailAddress<T, TValue>(this IValidationRuleBuilder<TValue> builder)
        where TValue : IEnumerable<string>
    {
        if (builder.ValidationRule is IValidationRule<T, string> validationRule)
        {
            return builder.EmailAddress(error =>
            {
                var validationExpression = validationRule.ValidationExpression.ToString();

                error.Code = Resources.DefaultValidationErrorCode;
                error.Message = string.Format(Resources.DefaultValidationMessageEmailAddressRule, validationExpression);
                error.Source = validationExpression;
            });
        }
        else
        {
            throw new ValidationUnsupportedRuleException(builder.ValidationRule);
        }
    }

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be in the format of an email address.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="builder"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Is thrown when the <paramref name="configure"/> is null.</exception>
    /// <exception cref="ValidationException">Is thrown when <see cref="IValidationRuleBuilder{T, TValue}.ValidationRule"/> is not of type <see cref="IValidationRule{T, TValue}"/>.</exception>
    public static IValidationRuleBuilder<T, TValue> EmailAddress<T, TValue>(this IValidationRuleBuilder<TValue> builder, Action<IValidationError> configure)
        where TValue : IEnumerable<string>
    {
        if (builder.ValidationRule is IValidationRule<T, string> validationRule)
        {
            if (configure is null)
            {
                throw new ArgumentNullException(
                    paramName: nameof(configure),
                    message: "The 'configure' parameter cannot be null in: EmailAddress(Action<IValidationError> configure)")
                {
                    Source = $"RuleFor({validationRule.ValidationExpression}).EmailAddress({configure})."
                };
            }

            var error = new ValidationError();

            configure.Invoke(error);

            validationRule.AddRule(new EmailValidationRule<T, string>(validationRule.ValidationExpression)
            {
                Error = error
            });

            return builder;
        }
        else
        {
            throw new ValidationUnsupportedRuleException(builder.ValidationRule);
        }
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
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    /// <exception cref="ValidationException">Is thrown when <see cref="IValidationRuleBuilder{T, TValue}.ValidationRule"/> is not of type <see cref="IValidationRule{T, TValue}"/>.</exception>
    /// <exception cref="InvalidOperationException">Is thrown when <paramref name="min"/> is greater than <paramref name="max"/>.</exception>
    public static IValidationRuleBuilder<T, TValue> Length<T, TValue>(this IValidationRuleBuilder<T, TValue> builder, int min, int max) 
        where TValue : IEnumerable
    {
        if (builder.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            if (min > max)
            {
                throw new InvalidOperationException($"The minimum length cannot be larger than the maximum length.")
                {
                    Source = $"RuleFor({validationRule.ValidationExpression}).Length({min},{max})"
                };
            }
            return builder.Length(min, max, error =>
            {
                var validationExpression = validationRule.ValidationExpression.ToString();

                error.Code = Resources.DefaultValidationErrorCode;
                error.Message = String.Format(Resources.DefaultValidationMessageLengthBetweenRule, validationExpression, min, max);
                error.Source = validationExpression;
            });
        }
        else
        {
            throw new ValidationUnsupportedRuleException(builder.ValidationRule);
        }
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
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    /// <exception cref="ArgumentNullException">Is thrown when the <paramref name="configure"/> is null.</exception>
    /// <exception cref="ValidationException">Is thrown when <see cref="IValidationRuleBuilder{T, TValue}.ValidationRule"/> is not of type <see cref="IValidationRule{T, TValue}"/>.</exception>
    /// <exception cref="InvalidOperationException">Is thrown when <paramref name="min"/> is greater than <paramref name="max"/>.</exception>
    public static IValidationRuleBuilder<T, TValue> Length<T, TValue>(this IValidationRuleBuilder<T, TValue> builder, int min, int max, Action<IValidationError> configure)
        where TValue : IEnumerable
    {
        if (builder.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            if (configure is null)
            {
                throw new ArgumentNullException(
                    paramName: nameof(configure),
                    message: "The 'configure' parameter cannot be null in: Length(int min, int max, Action<IValidationError> configure)")
                {
                    Source = $"RuleFor({validationRule.ValidationExpression}).Length({min}, {max}, {configure})."
                };
            } 
            if (min > max)
            {
                throw new InvalidOperationException($"The minimum length cannot be larger than the maximum length.")
                {
                    Source = $"RuleFor({validationRule.ValidationExpression}).Length({min},{max}, {configure})"
                };
            }

            var error = new ValidationError();

            configure.Invoke(error);

            validationRule.AddRule(new LengthBetweenValidationRule<T, TValue>(validationRule.ValidationExpression, min, max)
            {
                Error = error
            });

            return builder;
        }
        else
        {
            throw new ValidationUnsupportedRuleException(builder.ValidationRule);
        }
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
    ///     <item><see cref="String"/></item>
    /// </list>
    /// </remarks>
    /// <param name="builder">The current instance of the validation builder.</param>
    /// <param name="exact">The required length of the <see cref="IEnumerable"/> being validated.</param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    /// <exception cref="ValidationException">Is thrown when <see cref="IValidationRuleBuilder{T, TValue}.ValidationRule"/> is not of type <see cref="IValidationRule{T, TValue}"/>.</exception>
    public static IValidationRuleBuilder<T, TValue> Length<T, TValue>(this IValidationRuleBuilder<T, TValue> builder, int exact)
        where TValue : IEnumerable
    {
        if (builder.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            return builder.Length(exact, error =>
            {
                var validationExpression = validationRule.ValidationExpression.ToString();

                error.Code = Resources.DefaultValidationErrorCode;
                error.Message = string.Format(Resources.DefaultValidationMessageLengthRule, validationExpression, exact);
                error.Source = validationExpression;
            });
        }
        else
        {
            throw new ValidationUnsupportedRuleException(builder.ValidationRule);
        }
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
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    /// </exception>
    /// <exception cref="ArgumentNullException">Is thrown when the <paramref name="configure"/> is null.</exception>
    /// <exception cref="ValidationException">Is thrown when <see cref="IValidationRuleBuilder{T, TValue}.ValidationRule"/> is not of type <see cref="IValidationRule{T, TValue}"/>.</exception>
    public static IValidationRuleBuilder<T, TValue> Length<T, TValue>(this IValidationRuleBuilder<T, TValue> builder, int exact, Action<IValidationError> configure)
        where TValue : IEnumerable
    {
        if (builder.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            if (configure is null)
            {
                throw new ArgumentNullException(
                    paramName: nameof(configure),
                    message: "The 'configure' parameter cannot be null in: Length(int exact, Action<IValidationError> configure)")
                {
                    Source = $"RuleFor({validationRule.ValidationExpression}).Length({exact}, {configure})."
                };
            }

            var error = new ValidationError();

            configure.Invoke(error);

            validationRule.AddRule(new LengthValidationRule<T, TValue>(validationRule.ValidationExpression, exact)
            {
                Error = error
            });

            return builder;
        }
        else
        {
            throw new ValidationUnsupportedRuleException(builder.ValidationRule);
        }
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
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    /// <exception cref="ValidationException">Is thrown when <see cref="IValidationRuleBuilder{T, TValue}.ValidationRule"/> is not of type <see cref="IValidationRule{T, TValue}"/>.</exception>
    public static IValidationRuleBuilder<T, TValue> MaxLength<T, TValue>(this IValidationRuleBuilder<T, TValue> builder, int max)
        where TValue : IEnumerable
    {
        if (builder.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            return builder.MaxLength(max, error =>
            {
                var validationExpression = validationRule.ValidationExpression.ToString();

                error.Code = Resources.DefaultValidationErrorCode;
                error.Message = string.Format(Resources.DefaultValidationMessageMaxLengthRule, validationExpression, max);
                error.Source = validationExpression;
            });
        }
        else
        {
            throw new ValidationUnsupportedRuleException(builder.ValidationRule);
        }
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
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    /// <exception cref="ArgumentNullException">Is thrown when the <paramref name="configure"/> is null.</exception>
    /// <exception cref="ValidationException">Is thrown when <see cref="IValidationRuleBuilder{T, TValue}.ValidationRule"/> is not of type <see cref="IValidationRule{T, TValue}"/>.</exception>
    public static IValidationRuleBuilder<T, TValue> MaxLength<T, TValue>(this IValidationRuleBuilder<T, TValue> builder, int max, Action<IValidationError> configure)
        where TValue : IEnumerable
    {
        if (builder.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            if (configure is null)
            {
                throw new ArgumentNullException(
                    paramName: nameof(configure),
                    message: "The 'configure' parameter cannot be null in: MaxLength(int max, Action<IValidationError> configure)")
                {
                    Source = $"RuleFor({validationRule.ValidationExpression}).MaxLength({max}, {configure})."
                };
            }

            var error = new ValidationError();

            configure.Invoke(error);

            validationRule.AddRule(new LengthMaxValidationRule<T, TValue>(validationRule.ValidationExpression, max)
            {
                Error = error
            });

            return builder;
        }
        else
        {
            throw new ValidationUnsupportedRuleException(builder.ValidationRule);
        }
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
    ///     <item><see cref="String"/></item>
    /// </list>
    /// </remarks>
    /// <param name="builder">The current instance of the validation builder.</param>
    /// <param name="min">The minimum length of the <see cref="IEnumerable"/> being validated.</param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    /// <exception cref="ValidationException">Is thrown when <see cref="IValidationRuleBuilder{T, TValue}.ValidationRule"/> is not of type <see cref="IValidationRule{T, TValue}"/>.</exception>
    public static IValidationRuleBuilder<T, TValue> MinLength<T, TValue>(this IValidationRuleBuilder<T, TValue> builder, int min)
        where TValue : IEnumerable
    {
        if (builder.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            return builder.MinLength(min, error =>
            {
                var validationExpression = validationRule.ValidationExpression.ToString();

                error.Code = Resources.DefaultValidationErrorCode;
                error.Message = String.Format(Resources.DefaultValidationMessageMinLengthRule, validationExpression, min);
                error.Source = validationExpression;
            });
        }
        else
        {
            throw new ValidationUnsupportedRuleException(builder.ValidationRule);
        }
    }

    /// <summary>
    /// Creates a rule specifying the minimum length of an <see cref="IEnumerable"/> with 
    /// a configurable custom error.
    /// </summary>
    /// <param name="builder">The current instance of the validation builder.</param>
    /// <param name="min">The minimum length of the <see cref="IEnumerable"/> being validated.</param>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    /// <exception cref="ArgumentNullException">Is thrown when the <paramref name="configure"/> is null.</exception>
    /// <exception cref="ValidationException">Is thrown when <see cref="IValidationRuleBuilder{T, TValue}.ValidationRule"/> is not of type <see cref="IValidationRule{T, TValue}"/>.</exception>
    public static IValidationRuleBuilder<T, TValue> MinLength<T, TValue>(this IValidationRuleBuilder<T, TValue> builder, int min, Action<IValidationError> configure)
        where TValue : IEnumerable
    {
        if (builder.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            if (configure is null)
            {
                throw new ArgumentNullException(
                    paramName: nameof(configure),
                    message: "The 'configure' parameter cannot be null in: MinLength(Action<IValidationError> configure)")
                {
                    Source = $"RuleFor({validationRule.ValidationExpression}).MinLength({min}, {configure})."
                };
            }

            var error = new ValidationError();

            configure.Invoke(error);

            validationRule.AddRule(new LengthMinValidationRule<T, TValue>(validationRule.ValidationExpression, min)
            {
                Error = error
            });

            return builder;
        }
        else
        {
            throw new ValidationUnsupportedRuleException(builder.ValidationRule);
        }
    }


    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must not be empty.
    /// </summary>
    /// <param name="builder">The current instance of the validation builder.</param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    /// <exception cref="ValidationException">Is thrown when <see cref="IValidationRuleBuilder{T, TValue}.ValidationRule"/> is not of type <see cref="IValidationRule{T, TValue}"/>.</exception>
    public static IValidationRuleBuilder<T, TValue> NotEmpty<T, TValue>(this IValidationRuleBuilder<T, TValue> builder)
        where TValue : IEnumerable
    {
        if (builder.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            return builder.NotEmpty(configure =>
            {
                var validationExpression = validationRule.ValidationExpression.ToString();

                configure.Code = Resources.DefaultValidationErrorCode;
                configure.Message = string.Format(Resources.DefaultValidationMessageNotEmptyRule, validationExpression);
                configure.Source = validationExpression;
            });
        }
        else
        {
            throw new ValidationUnsupportedRuleException(builder.ValidationRule);
        }
    }

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must not be empty with 
    /// a configurable custom error.
    /// </summary>
    /// <param name="builder">The current instance of the validation builder.</param>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    /// <exception cref="ArgumentNullException">Is thrown when the <paramref name="configure"/> is null.</exception>
    /// <exception cref="ValidationException">Is thrown when <see cref="IValidationRuleBuilder{T, TValue}.ValidationRule"/> is not of type <see cref="IValidationRule{T, TValue}"/>.</exception>
    public static IValidationRuleBuilder<T, TValue> NotEmpty<T, TValue>(this IValidationRuleBuilder<T, TValue> builder, Action<IValidationError> configure)
        where TValue : IEnumerable
    {
        if (builder.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            if (configure is null)
            {
                throw new ArgumentNullException(
                    paramName: nameof(configure),
                    message: "The 'configure' parameter cannot be null in: NotEmpty(Action<IValidationError> configure)")
                {
                    Source = $"RuleFor({validationRule.ValidationExpression}).NotEmpty({configure}) or RuleForEach({validationRule.ValidationExpression}).Empty({configure})"
                };
            }

            var error = new ValidationError();

            configure.Invoke(error);

            validationRule.AddRule(new NotEmptyValidationRule<T, TValue>(validationRule.ValidationExpression)
            {
                Error = error
            });

            return builder;
        }
        else
        {
            throw new ValidationUnsupportedRuleException(builder.ValidationRule);
        }
    }

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must not be empty.
    /// </summary>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    /// <exception cref="ValidationException">Is thrown when <see cref="IValidationRuleBuilder{T, TValue}.ValidationRule"/> is not of type <see cref="IValidationRule{T, TValue}"/>.</exception>
    public static IValidationRuleBuilder<T, TValue> Empty<T, TValue>(this IValidationRuleBuilder<T, TValue> builder)
        where TValue : IEnumerable
    {
        if (builder.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            return builder.Empty(configure =>
            {
                var validationExpression = validationRule.ValidationExpression.ToString();

                configure.Code = Resources.DefaultValidationErrorCode;
                configure.Message = string.Format(Resources.DefaultValidationMessageEmptyRule, validationExpression);
                configure.Source = validationExpression;
            });
        }
        else
        {
            throw new ValidationUnsupportedRuleException(builder.ValidationRule);
        }
    }

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be empty with 
    /// a configurable custom error.
    /// </summary>
    /// <param name="builder">The current instance of the validation builder.</param>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    /// <exception cref="ArgumentNullException">Is thrown when the <paramref name="configure"/> is null.</exception>
    /// <exception cref="ValidationException">Is thrown when <see cref="IValidationRuleBuilder{TValue}.ValidationRule"/> is not of type <see cref="IValidationRule{T, TValue}"/>.</exception>
    public static IValidationRuleBuilder<TValue> Empty<TValue>(this IValidationRuleBuilder<TValue> builder, Action<IValidationError> configure)
        where TValue : IEnumerable
    {
        if (builder.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            if (configure is null)
            {
                throw new ArgumentNullException(
                    paramName: nameof(configure),
                    message: "The 'configure' parameter cannot be null in: Empty(Action<IValidationError> configure)")
                {
                    Source = $"RuleFor({validationRule.ValidationExpression}).Empty({configure}) or RuleForEach({validationRule.ValidationExpression}).Empty({configure})"
                };
            }

            var error = new ValidationError();

            configure.Invoke(error);

            validationRule.AddRule(new EmptyValidationRule<T, TValue>(validationRule.ValidationExpression)
            {
                Error = error
            });

            return builder;
        }
        else
        {
            throw new ValidationUnsupportedRuleException(builder.ValidationRule);
        }
    }
}

