﻿using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Validation.Internal.Rules;


internal sealed class LengthMinValidationRule<TValue> : ValidationRuleBase<TValue>
    where TValue : IEnumerable
{
    private readonly int length;

    public LengthMinValidationRule(int length)
    {
        this.length = length;
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

            if (IsUnderMinLength(value))
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

    private bool IsUnderMinLength(object member)
    {
        return member switch
        {
            null => true,
            string stringValue      when stringValue is not null && stringValue.Length < this.length => true,
            ICollection collection  when collection.Count < this.length => true,
            Array array             when array.Length < this.length => true,
            IEnumerable enumerable  when enumerable.Cast<object>().Count() < this.length => true,
            _ => false
        };
    }
}

