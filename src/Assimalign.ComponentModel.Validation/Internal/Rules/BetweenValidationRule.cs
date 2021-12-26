using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Validation.Internal.Rules;


using Assimalign.ComponentModel.Validation.Internal.Exceptions;

internal sealed class BetweenValidationRule<T, TValue, TBound> : IValidationRule, IComparer<TBound>
    where TBound : IComparable<TBound>
{
    private readonly TBound lower;
    private readonly TBound upper;
    private readonly Func<TBound, bool> isOutOfBounds;
    private readonly Expression<Func<T, TValue>> expression;

    public BetweenValidationRule(Expression<Func<T, TValue>> expression, TBound lower, TBound upper)
    {
        this.lower = lower;
        this.upper = upper;
        this.expression = expression;
        this.isOutOfBounds = x => Compare(x, lower) < 0 || Compare(x, upper) > 0;
    }

    /// <summary>
    /// 
    /// </summary>
    public string GetNameOfValue => string.Join('.', expression.Body.ToString().Split('.').Skip(1));

    /// <summary>
    /// 
    /// </summary>
    public string Name { get; } = "BetweenValidationRule";

    /// <summary>
    /// 
    /// </summary>
    public IValidationError Error { get; set; }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <exception cref="Exception"></exception>
    public void Evaluate(IValidationContext context)
    {
        if (context.Instance is T instance)
        {
            var value = this.expression.Compile().Invoke(instance);

            if (value is IEnumerable enumerable)
            {
                foreach (var item in enumerable)
                {
                    if (item is TBound a && isOutOfBounds(a))
                    {
                        context.AddFailure(new ValidationError()
                        {
                            Message = $"One of the following items in '{GetNameOfValue}' is not within bounds of: {lower} and {upper}.",
                            Source = expression.Body.ToString()
                        });
                        return;
                    }
                }
            }
            if (value is TBound b && isOutOfBounds(b))
            {
                context.AddFailure(new ValidationError()
                {
                    Message = $"The value of '{GetNameOfValue}' is not within bounds of: {lower} & {upper}.",
                    Source = expression.Body.ToString()
                });
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
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public int Compare(TBound left, TBound right) => left.CompareTo(right);
}
