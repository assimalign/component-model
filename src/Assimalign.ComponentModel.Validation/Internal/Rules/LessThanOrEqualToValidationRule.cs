using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Internal.Rules;

using Assimalign.ComponentModel.Validation.Internal.Exceptions;

internal sealed class LessThanOrEqualToValidationRule<T, TValue, TArgument> : IValidationRule
    where TArgument : IComparable
{
    private readonly TArgument argument;
    private readonly Func<TArgument, object, bool> isLessThanOrEqualTo;
    private readonly Expression<Func<T, TValue>> expression;

    public LessThanOrEqualToValidationRule(Expression<Func<T, TValue>> expression, TArgument argument)
    {
        if (expression is null)
        {
            throw new ArgumentNullException(nameof(expression));
        }

        if (argument is null)
        {
            throw new ArgumentNullException(nameof(TArgument));
        }
        this.argument = argument;
        this.expression = expression;
        this.isLessThanOrEqualTo = (arg, val) => arg.CompareTo(val) >= 0;
    }

    public string Name => nameof(LessThanOrEqualToValidationRule<T, TValue, TArgument>);

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
                    if (item is null || !isLessThanOrEqualTo(argument, item))
                    {
                        context.AddFailure(this.Error);
                        break;
                    }
                }
            }
            else if (!isLessThanOrEqualTo(argument, value))
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


    /// <summary>
    /// 
    /// </summary>
    /// <param name="instance"></param>
    /// <returns></returns>
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