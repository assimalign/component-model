using System;
using System.Collections;
using System.Linq;

namespace Assimalign.ComponentModel.Validation.Internal;

using Assimalign.ComponentModel.Validation.Properties;
using Assimalign.ComponentModel.Validation.Internal.Rules;
using Assimalign.ComponentModel.Validation.Internal.Exceptions;


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
        if (this.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
          

        }
        else
        {
            throw new ValidationUnsupportedRuleException(this.ValidationRule);
        }


        throw new NotImplementedException();
    }

    public IValidationRuleBuilder<TValue> Custom(Action<TValue, IValidationContext> validation)
    {
        if (this.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            if (validation is null)
            {
                throw new ArgumentNullException(
                    paramName: nameof(validation),
                    message: "The 'validation' parameter cannot be null in: Custom(Action<TValue, IValidationContext> validation)")
                {
                    Source = $"RuleFor({validationRule.ValidationExpression}).Custom({validation}) or RuleForEach({validationRule.ValidationExpression}).Custom({validation})"
                };
            }

            validationRule.AddRule(new CustomValidationRule<T, TValue>(validationRule.ValidationExpression, validation));

            return this;
        }
        else
        {
            throw new ValidationUnsupportedRuleException(this.ValidationRule);
        }
    }

    public IValidationRuleBuilder<TValue> Between<TBound>(TBound lowerBound, TBound upperBound)
        where TBound : notnull, IComparable
    {
        if (this.ValidationItem is IValidationItem<T, TValue> validationRule)
        {
            return Between<TBound>(lowerBound, upperBound, configure =>
            {
                var validationExpression = validationRule.ItemExpression.Body.ToString();

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

    public IValidationRuleBuilder<TValue> Between<TBound>(TBound lowerBound, TBound upperBound, Action<IValidationError> configure) 
        where TBound : notnull, IComparable
    {
        if (this.ValidationItem is IValidationItem<T, TValue> validationRule)
        {
            if (configure is null)
            {
                throw new ArgumentNullException(
                    paramName: nameof(configure),
                    message: "The 'configure' parameter cannot be null in: Between<TBound>(TBound lowerBound, TBound upperBound, Action<IValidationError> configure)")
                {
                    Source = $"RuleFor({validationRule.ItemExpression}).Between<{typeof(TBound).Name}>({lowerBound}, {upperBound}, {configure})"
                };
            }

            var error = new ValidationError();

            configure.Invoke(error);

            validationRule.AddRule(new BetweenValidationRule<T, TValue, TBound>(validationRule.ItemExpression, lowerBound, upperBound)
            {
                Error = error,
                RuleType = this.ValidationRuleType
            });

            return this;
        }
        else
        {
            throw new ValidationUnsupportedRuleException(this.ValidationRule);
        }
    }

    public IValidationRuleBuilder<TValue> BetweenOrEqualTo<TBound>(TBound lowerBound, TBound upperBound)
        where TBound : notnull, IComparable
    {
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

    public IValidationRuleBuilder<TValue> BetweenOrEqualTo<TBound>(TBound lowerBound, TBound upperBound, Action<IValidationError> configure)
        where TBound : notnull, IComparable
    {
        if (this.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            if (configure is null)
            {
                throw new ArgumentNullException(
                    paramName: nameof(configure),
                    message: "The 'configure' parameter cannot be null in: BetweenOrEqualTo<TBound>(TBound lowerBound, TBound upperBound, Action<IValidationError> configure)")
                {
                    Source = $"RuleFor({validationRule.ValidationExpression}).BetweenOrEqualTo<{typeof(TBound).Name}>({lowerBound}, {upperBound}, {configure})"
                };
            }

            var error = new ValidationError();

            configure.Invoke(error);

            validationRule.AddRule(new BetweenOrEqualToValidationRule<T, TValue, TBound>(validationRule.ValidationExpression, lowerBound, upperBound)
            {
                Error = error,
                RuleType = this.ValidationRuleType
            });

            return this;
        }
        else
        {
            throw new ValidationUnsupportedRuleException(this.ValidationRule);
        }
    }

    public IValidationRuleBuilder<TValue> EqualTo<TArgument>(TArgument value)
        where TArgument : notnull, IEquatable<TArgument>
    {
        if (this.ValidationRule is IValidationRule<T, TValue> validationRule)
        {
            return this.EqualTo<TArgument>(value, configure =>
            {
                var validationExpression = validationRule.ValidationExpression.ToString();

                configure.Code = Resources.DefaultValidationErrorCode;
                configure.Message = string.Format(Resources.DefaultValidationMessageEqualToRule, validationExpression, typeof(TArgument).IsValueType ? value : typeof(TArgument).Name);
                configure.Source = validationExpression;
            });
        }
        else
        {
            throw new ValidationUnsupportedRuleException(this.ValidationRule);
        }
    }

    public IValidationRuleBuilder<TValue> EqualTo<TArgument>(TArgument value, Action<IValidationError> configure)
        where TArgument : notnull, IEquatable<TArgument>
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
                Error = error,
                RuleType = this.ValidationRuleType
            });

            return this;
        }
        else
        {
            throw new ValidationUnsupportedRuleException(this.ValidationRule);
        }
    }

    public IValidationRuleBuilder<TValue> GreaterThan<TArgument>(TArgument value) 
        where TArgument : notnull, IComparable
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

    public IValidationRuleBuilder<TValue> GreaterThan<TArgument>(TArgument value, Action<IValidationError> configure) 
        where TArgument : notnull, IComparable
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
                Error = error,
                RuleType = this.ValidationRuleType
            });

            return this;
        }
        else
        {
            throw new ValidationUnsupportedRuleException(this.ValidationRule);
        }
    }

    public IValidationRuleBuilder<TValue> GreaterThanOrEqualTo<TArgument>(TArgument value) 
        where TArgument : notnull, IComparable
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

    public IValidationRuleBuilder<TValue> GreaterThanOrEqualTo<TArgument>(TArgument value, Action<IValidationError> configure) 
        where TArgument : notnull, IComparable
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
                Error = error,
                RuleType = this.ValidationRuleType
            });

            return this;
        }
        else
        {
            throw new ValidationUnsupportedRuleException(this.ValidationRule);
        }
    }

    public IValidationRuleBuilder<TValue> LessThan<TArgument>(TArgument value) 
        where TArgument : notnull, IComparable
    {
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

    public IValidationRuleBuilder<TValue> LessThan<TArgument>(TArgument value, Action<IValidationError> configure) 
        where TArgument : notnull, IComparable
    {
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
                Error = error,
                RuleType = this.ValidationRuleType
            });

            return this;
        }
        else
        {
            throw new ValidationUnsupportedRuleException(this.ValidationRule);
        }
    }

    public IValidationRuleBuilder<TValue> LessThanOrEqualTo<TArgument>(TArgument value) 
        where TArgument : notnull, IComparable
    {
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

    public IValidationRuleBuilder<TValue> LessThanOrEqualTo<TArgument>(TArgument value, Action<IValidationError> configure) 
        where TArgument : notnull, IComparable
    {
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
                Error = error,
                RuleType = this.ValidationRuleType
            });

            return this;
        }
        else
        {
            throw new ValidationUnsupportedRuleException(this.ValidationRule);
        }
    }

    public IValidationRuleBuilder<TValue> NotEqualTo<TArgument>(TArgument value)
        where TArgument : notnull, IEquatable<TArgument>
    {
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

    public IValidationRuleBuilder<TValue> NotEqualTo<TArgument>(TArgument value, Action<IValidationError> configure)
        where TArgument : notnull, IEquatable<TArgument>
    {
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
                Error = error,
                RuleType = this.ValidationRuleType
            });

            return this;
        }
        else
        {
            throw new ValidationUnsupportedRuleException(this.ValidationRule);
        }
    }

    public IValidationRuleBuilder<TValue> NotNull()
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

    public IValidationRuleBuilder<TValue> NotNull(Action<IValidationError> configure)
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
                Error = error,
                RuleType = this.ValidationRuleType
            });

            return this;
        }
        else
        {
            throw new ValidationUnsupportedRuleException(this.ValidationRule);
        }
    }

    public IValidationRuleBuilder<TValue> Null()
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

    public IValidationRuleBuilder<TValue> Null(Action<IValidationError> configure)
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
                Error = error,
                RuleType = this.ValidationRuleType
            });

            return this;
        }
        else
        {
            throw new ValidationUnsupportedRuleException(this.ValidationRule);
        }
    }
}