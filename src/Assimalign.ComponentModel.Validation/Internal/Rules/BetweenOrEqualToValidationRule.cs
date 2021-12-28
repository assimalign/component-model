using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Validation.Internal.Rules;


using Assimalign.ComponentModel.Validation.Internal.Exceptions;

internal sealed class BetweenOrEqualToValidationRule<T, TValue, TBound> : IValidationRule
    where TBound : notnull, IComparable
{
    private readonly TBound lower;
    private readonly TBound upper;
    private readonly Func<TBound, TBound, object, bool> isOutOfBounds;
    private readonly Expression<Func<T, TValue>> expression;
    private readonly string expressionBody;

    public BetweenOrEqualToValidationRule(Expression<Func<T, TValue>> expression, TBound lower, TBound upper)
    {
        if (expression is null)
        {
            throw new ArgumentNullException(
                paramName: nameof(expression),
                message: $"The following expression where the 'BetweenOrEqualTo()' rule is defined cannot be null.");
        }
        if (expression.Body is MemberExpression member)
        {
            this.expressionBody = string.Join('.', member.ToString().Split('.').Skip(1));
        }
        this.lower = lower;
        this.upper = upper;
        this.expression = expression;
        this.isOutOfBounds = (lower, upper, value) =>
        {
            var lowerResults = lower.CompareTo(value);
            var upperResults = upper.CompareTo(value);

            return lowerResults > 0 || upperResults < 0;
        };
    }


    public string Name => $"BetweenOrEqualToValidationRule<{typeof(T).Name},{expressionBody ?? typeof(TValue).Name},{typeof(TBound).Name}>";

    public IValidationError Error { get; set; }

    public void Evaluate(IValidationContext context)
    {
        if (context.Instance is T instance)
        {
            var value = this.GetValue(instance);
            
            if (value is null)
            {
                context.AddFailure(this.Error);
            }
            else if (value is IEnumerable enumerable)
            {
                foreach (var item in enumerable)
                {
                    object obj = item;

                    if (item is not TBound && item is IConvertible convertible)
                    {
                        obj = convertible.ToType(typeof(TBound), default);
                    }

                    if (obj is null || isOutOfBounds(this.lower, this.upper, obj))
                    {
                        context.AddFailure(this.Error);
                        break;
                    }
                }
            }
            else if (isOutOfBounds(this.lower, this.upper, value))
            {
                context.AddFailure(this.Error);
            }
            else
            {
                context.AddSuccess(this);
            }
        }
    }


    private object GetValue(T instance)
    {
        try
        {
            var value = expression.Compile().Invoke(instance);

            if (value is not TBound && value is IConvertible convertible)
            {
                return convertible.ToType(typeof(TBound), default);
            }

            return value;
        }
        catch
        {
            return null;
        }
    }
}

