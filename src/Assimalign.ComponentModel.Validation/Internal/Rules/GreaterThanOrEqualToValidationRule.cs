﻿using System;

namespace Assimalign.ComponentModel.Validation.Internal.Rules;

internal sealed class GreaterThanOrEqualToValidationRule<TValue, TArgument> : ValidationRuleBase<TValue>
    where TArgument : notnull, IComparable
{
    private readonly TArgument argument;
    private readonly Func<TArgument, object, bool> isGreaterThanOrEqualTo;

    public GreaterThanOrEqualToValidationRule(TArgument argument)
    {
        this.argument = argument;
        this.isGreaterThanOrEqualTo = (arg, val) => arg.CompareTo(val) <= 0; // Is the argument less than the value
        this.ArgumentType = typeof(TArgument);
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

                if (!isGreaterThanOrEqualTo(this.argument, convertedValue))
                {
                    context.AddFailure(this.Error);
                }
            }
            else if (!isGreaterThanOrEqualTo(this.argument, value))
            {
                context.AddFailure(this.Error);
            }

            return true;
        }
        catch (InvalidCastException)
        {
            context = new ValidationContext<TValue>(value);

            if (!isGreaterThanOrEqualTo(this.argument, value))
            {
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