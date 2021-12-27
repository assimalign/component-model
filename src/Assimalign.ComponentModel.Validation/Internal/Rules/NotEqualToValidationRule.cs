﻿using System;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Validation.Internal.Rules;

internal sealed class NotEqualToValidationRule<T, TValue, TArgument> : IValidationRule
{
    private readonly Func<object, bool> isEqualTo;
    private readonly Expression<Func<T, TValue>> expression;

    public NotEqualToValidationRule(Expression<Func<T, TValue>> expression, TArgument value)
    {
        this.expression = expression;
        this.isEqualTo = x => x.Equals(value);


    }

    /// <summary>
    /// 
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// 
    /// </summary>
    public IValidationError Error { get; set; }

    public void Evaluate(IValidationContext context)
    {
        if (context.Instance is T instance)
        {
            var value = this.GetValue(instance);

            if (this.isEqualTo(value))
            {
                context.AddFailure(this.Error);
            }
        }
    }


    private object GetValue(T instance)
    {
        try
        {
            return this.expression.Compile().Invoke(instance);
        }
        catch
        {
            return default(TValue);
        }
    }
}

