﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Validation.Internal.Rules;


using Assimalign.ComponentModel.Validation.Internal.Exceptions;

internal sealed class GreaterThanValidationRule<T, TValue, TArgument> : IValidationRule, IComparer<TArgument>
    where TArgument : IComparable
{
    private readonly TArgument argument;
    private readonly Func<TArgument, object, bool> isGreaterThan;
    private readonly Expression<Func<T, TValue>> expression;

    public GreaterThanValidationRule(Expression<Func<T, TValue>> expression, TArgument argument)
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
        this.isGreaterThan = (arg, val) => arg.CompareTo(val) < 0; // Is the argument less than the value
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
                    if (item is null || !isGreaterThan.Invoke(argument, item))
                    {
                        context.AddFailure(this.Error);
                        break;
                    }
                }
            }
            else if (!isGreaterThan.Invoke(argument, value))
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
    private object GetValue(T instance)
    {
        try
        {
            var value = expression.Compile().Invoke(instance);

            if (value is IConvertible convertible)
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
