using System;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Validation.Internal.Rules;

using Assimalign.ComponentModel.Validation.Internal.Exceptions;

internal sealed class NotNullValidationRule<T, TValue> : IValidationRule
{
    private readonly Expression<Func<T, TValue>> expression;

    public NotNullValidationRule(Expression<Func<T, TValue>> expression)
    {
        this.expression = expression;
    }

    public string Name => nameof(NotNullValidationRule<T, TValue>);

    public IValidationError Error { get; set; }

    public void Evaluate(IValidationContext context)
    {
        if (context.Instance is T instance)
        {
            var value = this.GetValue(instance);

            if (value is null)
            {
                context.AddFailure(this.Error);
            }
        }
        else
        {
            throw new ValidationInternalException("");
        }
    }

    private object GetValue(T instance)
    {
        try
        {
            return expression.Compile().Invoke(instance);
        }
        catch
        {
            return null;
        }
    }
}

