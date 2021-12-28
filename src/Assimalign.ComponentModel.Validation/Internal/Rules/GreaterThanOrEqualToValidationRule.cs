using System;
using System.Collections;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Validation.Internal.Rules;

using Assimalign.ComponentModel.Validation.Internal.Exceptions;

internal sealed class GreaterThanOrEqualToValidationRule<T, TValue, TArgument> : IValidationRule
    where TArgument : IComparable
{
    private readonly TArgument argument;
    private readonly Func<TArgument, object, bool> isGreaterThanOrEqualTo;
    private readonly Expression<Func<T, TValue>> expression;

    public GreaterThanOrEqualToValidationRule(Expression<Func<T, TValue>> expression, TArgument argument)
    {
        if (expression is null)
        {
            throw new ArgumentNullException(nameof(expression));
        }

        if (argument is null)
        {
            throw new ArgumentNullException(nameof(argument));
        }

        this.argument = argument;
        this.expression = expression;
        this.isGreaterThanOrEqualTo = (arg, val) => arg.CompareTo(val) <= 0; // Is the argument less than the value
    }

    public string Name => nameof(GreaterThanOrEqualToValidationRule<T, TValue, TArgument>);

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
                    if (item is null || !isGreaterThanOrEqualTo.Invoke(this.argument, item))
                    {
                        context.AddFailure(this.Error);
                        break;
                    }
                }
            }
            else if (!isGreaterThanOrEqualTo.Invoke(this.argument, value))
            {
                context.AddFailure(this.Error);
            }
            else
            {
                context.AddSuccess(this);
            }
        }
        else
        {
            // TODO: Something has happened if the code has gotten this far
            throw new ValidationInternalException("The type being evaluated does not match the evaluation type.");
        }
    }


    private object GetValue(T instance)
    {
        try
        {
            var value = expression.Compile().Invoke(instance);

            if (value is not TArgument && value is IConvertible convertible)
            {
                return convertible.ToType(typeof(TArgument), default);
            }

            return value;
        }
        catch
        {
            return null;
        }
    }
}

