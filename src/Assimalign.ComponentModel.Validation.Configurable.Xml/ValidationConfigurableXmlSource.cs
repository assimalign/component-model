using System;


namespace Assimalign.ComponentModel.Validation.Configurable;

internal sealed class ValidationConfigurableXmlSource<T> : IValidationConfigurableSource
{
    private readonly Func<ValidationConfigurableXmlProfile<T>> configure;

    public ValidationConfigurableXmlSource(Func<ValidationConfigurableXmlProfile<T>> configure)
    {
        this.configure = configure;
    }

    public IValidationConfigurableProvider Build() =>
        new ValidationConfigurableXmlProvider<T>(configure.Invoke());
}