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

    public override bool TryValidate(object value, out IValidationError error)
    {
        return TryValidate(value ?? default(TValue), out error);
    }

    public override bool IsValid(TValue value, out IValidationError error)
    {
        error = null;

        var context = new ValidationContext<TValue>(value);

        validation.Invoke(value, context);
        
        if (context.Errors.Any())
        {
            error = context.Errors.First();
            return false;
        }
        else
        {
            return true;
        }
    }
}
