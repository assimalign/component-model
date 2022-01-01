﻿using System;

namespace Assimalign.ComponentModel.Validation.Internal.Rules;

internal sealed class LessThanOrEqualToValidationRule<TValue> : ValidationRuleBase<TValue>
    where TValue : struct, IComparable, IComparable<TValue>
{
    private readonly TValue argument;
    private readonly Func<TValue, TValue, bool> isLessThanOrEqualTo;

    public LessThanOrEqualToValidationRule(TValue argument)
    {
        this.argument = argument;
        this.isLessThanOrEqualTo = (arg, val) => arg.CompareTo(val) >= 0;
    }

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

            if (!isLessThanOrEqualTo(this.argument, value))
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