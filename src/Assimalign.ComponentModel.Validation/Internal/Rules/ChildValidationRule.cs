using System;
using System.Collections.Generic;
using System.Linq;


namespace Assimalign.ComponentModel.Validation.Internal.Rules;

internal sealed class ChildValidationRule<TValue> : ValidationRuleBase<TValue>
    where TValue : class
{

    public ValidationMode ValidationMode { get; set; }

    public IList<IValidationItem> ValidationItems { get; set; }

    public override string Name { get; set; }

    public override bool TryValidate(object value, out IValidationContext context)
    {
        if (value is null)
        {
            context = new ValidationContext<TValue>(default(TValue));
            return true;
        }
        else if (value is TValue tv)
        {
            return TryValidate(tv, out context);
        }
        else
        {
            context = null;
            return false;
        }
    }

    public override bool TryValidate(TValue value, out IValidationContext context)
    {
        try
        {
            context = new ValidationContext<TValue>(value);

            foreach (var item in this.ValidationItems)
            {
                if (this.ValidationMode == ValidationMode.Stop && context.Errors.Any())
                {
                    break;
                }

                item.Evaluate(context);
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

