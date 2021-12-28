using System;
using System.Collections;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Validation.Internal.Rules;


using Assimalign.ComponentModel.Validation.Internal.Exceptions;

internal sealed class LessThanValidationRule<T, TValue, TArgument> : IValidationRule
    where TArgument : IComparable
{
    private readonly TArgument argument;
    private readonly Func<TArgument, object, bool> isLessThan;
    private readonly Expression<Func<T, TValue>> expression;

    public LessThanValidationRule(Expression<Func<T, TValue>> expression, TArgument argument)
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
        this.isLessThan = (arg, val) => arg.CompareTo(val) >= 0;
    }

    public string Name { get; }

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
                    if (item is null || !isLessThan(this.argument, item))
                    {
                        context.AddFailure(this.Error);
                        break;
                    }
                }
            }
            else if (!isLessThan(this.argument, value))
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
    private TValue GetValue(T instance)
    {
        try
        {
            return expression.Compile().Invoke(instance);
        }
        catch (Exception exception)
        {
            return default(TValue);
        }
    }
}
