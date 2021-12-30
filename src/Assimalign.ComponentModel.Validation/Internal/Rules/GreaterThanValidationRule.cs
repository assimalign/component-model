using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Validation.Internal.Rules;

internal sealed class GreaterThanValidationRule<TValue, TArgument> : ValidationRuleBase<TValue>
    where TArgument : notnull, IComparable
{
    private readonly TArgument argument;
    private readonly Func<TArgument, object, bool> isGreaterThan;

    public GreaterThanValidationRule(TArgument argument)
    {
        this.argument = argument;
        this.isGreaterThan = (arg, val) => arg.CompareTo(val) < 0; // Is the argument less than the value
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
        context = null;

        try
        {
            if (!isGreaterThan(this.argument, value))
            {
                context = new ValidationContext<TValue>(value);
                context.AddFailure(this.Error);
            }

            return true;
        }
        catch
        {
            return false;
        }
    }
}
