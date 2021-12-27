using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Internal.Rules;


using Assimalign.ComponentModel.Validation.Internal.Exceptions;

internal sealed class LessThanValidationRule<T, TValue, TArgument> : IValidationRule, IComparer<TArgument>
    where TArgument : IComparable<TArgument>
{
    private readonly Func<TArgument, bool> isLessThan;
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

        this.expression = expression;
        this.isLessThan = x => Compare(x, argument) < 0;
    }

    public string Name { get; }

    public IValidationError Error { get; set; }

    public int Compare(TArgument left, TArgument right) => left.CompareTo(right);

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
                    if (item is null || (item is TArgument a && !isLessThan(a)))
                    {
                        context.AddFailure(this.Error);
                        break;
                    }
                }
            }
            else if (value is TArgument b && !isLessThan(b))
            {
                context.AddFailure(this.Error);
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
