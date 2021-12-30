using System;
using System.Linq;

namespace Assimalign.ComponentModel.Validation.Internal.Rules;

internal sealed class CustomValidationRule<TValue> : ValidationRuleBase<TValue>
{
    private readonly Action<TValue, IValidationContext> validation;

    public CustomValidationRule(Action<TValue, IValidationContext> validation)
    {
        if (validation is null)
        {
            throw new ArgumentNullException(nameof(validation));
        }

        this.validation = validation;
    }

    public override string Name { get; set; }

    public override bool TryValidate(object value, out IValidationContext context)
    {
        return TryValidate(value ?? default(TValue), out context);
    }

    public override bool TryValidate(TValue value, out IValidationContext context)
    {
        context = null;

        try
        {
            context = new ValidationContext<TValue>(value);

            validation.Invoke(value, context);

            return true;
        }
        catch
        {
            return false;
        }
    }
}
