﻿using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Validation.Internal.Rules;


using Assimalign.ComponentModel.Validation.Internal.Exceptions;

internal sealed class BetweenOrEqualToValidationRule<T, TValue, TBound> : IValidationRule
    where TBound : IComparable
{
    private readonly TBound lower;
    private readonly TBound upper;
    private readonly Func<TBound, TBound, object, bool> isOutOfBounds;
    private readonly Expression<Func<T, TValue>> expression;

    public BetweenOrEqualToValidationRule(Expression<Func<T, TValue>> expression, TBound lower, TBound upper)
    {
        if (expression is null)
        {
            throw new ArgumentNullException(nameof(expression));
        }
        if (lower is null)
        {
            throw new ArgumentNullException(nameof(lower));
        }
        if (upper is null)
        {
            throw new ArgumentNullException(nameof(upper));
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

    /// <summary>
    /// 
    /// </summary>
    public string GetNameOfValue => string.Join('.', expression.Body.ToString().Split('.').Skip(1));

    /// <summary>
    /// 
    /// </summary>
    public string Name { get; } = "BetweenOrEqualToValidationRule";

    /// <summary>
    /// 
    /// </summary>
    public IValidationError Error { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
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

