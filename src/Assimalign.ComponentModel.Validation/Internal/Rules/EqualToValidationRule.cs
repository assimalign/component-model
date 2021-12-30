using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;


namespace Assimalign.ComponentModel.Validation.Internal.Rules;

using Assimalign.ComponentModel.Validation.Internal.Exceptions;
using Assimalign.ComponentModel.Validation.Internal.Extensions;

internal sealed class EqualToValidationRule<TValue, TArgument> : ValidationRuleBase<TValue>
    where TArgument : notnull, IEquatable<TArgument>
{
    private readonly TArgument argument;

    public EqualToValidationRule(TArgument argument)
    {
        this.argument = argument;      
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
            context = new ValidationContext<TValue>(value);

            if (!this.argument.Equals(value))
            {
                context.AddFailure(this.Error);
            }

            return true;
        }
        catch (InvalidCastException exception)
        {
            if (value is IConvertible convertible)
            {
                var convertedValue = (TArgument)convertible.ToType(typeof(TArgument), default);

                if (!this.argument.Equals(convertedValue))
                {
                    context = new ValidationContext<TValue>(value);
                    context.AddFailure(this.Error);
                }

                return true;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }
}

