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
        if (value is null) // No need to validate an object that is null
        {
            context = new ValidationContext<TValue>(default(TValue))
            {
                Options = new Dictionary<string, object>()
            };
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
            context = new ValidationContext<TValue>(value)
            {
                Options = new Dictionary<string, object>()
            };

            foreach (var item in this.ValidationItems)
            {
                if (this.ValidationMode == ValidationMode.Stop && context.Errors.Any())
                {
                    break;
                }

                var childContext = new ValidationContext<TValue>(value)
                {
                    Options = new Dictionary<string, object>()
                };

                item.Evaluate(childContext);

                foreach (var error in childContext.Errors)
                {
                    context.AddFailure(error);
                }
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

