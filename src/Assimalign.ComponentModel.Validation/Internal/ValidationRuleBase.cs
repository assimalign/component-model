using System;
namespace Assimalign.ComponentModel.Validation.Internal;

internal abstract class ValidationRuleBase<TValue> : IValidationRule<TValue>
{
    public Type ValueType => typeof(TValue);
    public IValidationError Error { get; set; }
    public abstract string Name { get; set; }
    public abstract bool TryValidate(object value, out IValidationContext context);
    public abstract bool TryValidate(TValue value, out IValidationContext context);
}