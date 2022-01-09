using System;


namespace Assimalign.ComponentModel.Validation.Configurable;

internal sealed class ValidationConfigurableJsonSource<T> : IValidationConfigurableSource
{
    private readonly Func<ValidationConfigurableJsonProfile<T>> configure;

    public ValidationConfigurableJsonSource(Func<ValidationConfigurableJsonProfile<T>> configure)
    {
        this.configure = configure;
    }

    public IValidationConfigurableProvider Build() =>
        new ValidationConfigurableJsonProvider<T>(configure.Invoke());
}