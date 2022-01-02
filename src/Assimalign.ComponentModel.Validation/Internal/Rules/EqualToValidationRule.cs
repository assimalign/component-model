﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;


namespace Assimalign.ComponentModel.Validation.Internal.Rules;

internal sealed class EqualToValidationRule<TValue, TArgument> : ValidationRuleBase<TValue>
{
    private readonly TArgument argument;

    public EqualToValidationRule(TArgument argument)
    {
        this.ArgumentType = typeof(TArgument);
        this.argument = argument;
    }

    public Type ArgumentType { get; }

    public override string Name { get; set; }

    public override bool TryValidate(object value, out IValidationContext context)
    {
        context = null;

        if (value is null)
        {
            context = new ValidationContext<TValue>(default(TValue));
            context.AddFailure(this.Error);
            return true;
        }
        else if (value is TValue tv)
        {
            return TryValidate(tv, out context);
        }
        else
        {
            return false;
        }
    }

    public override bool TryValidate(TValue value, out IValidationContext context)
    {
        try
        {
            context = new ValidationContext<TValue>(value);

            if (value is IConvertible convertible)
            {
                var convertedValue = (TArgument)convertible.ToType(this.ArgumentType, default);

                if (!this.argument.Equals(convertedValue))
                {
                    context.AddFailure(this.Error);
                }
            }
            else if (!this.argument.Equals(value))
            {
                context.AddFailure(this.Error);
            }

            return true;
        }
        catch (InvalidCastException)
        {
            context = new ValidationContext<TValue>(value);

            if (!this.argument.Equals(value))
            {
                this.Error.Source = $"{this.Error.Source}. Comparison of type '{this.ArgumentType.Name}' and '{this.ValueType.Name}' is not allowed.";
                context.AddFailure(this.Error);
            }

            return true;
        }
        catch
        {
            context = null;
            return false;
        }
    }
}

