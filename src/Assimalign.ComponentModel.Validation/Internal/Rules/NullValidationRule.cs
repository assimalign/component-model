using System;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Validation.Internal.Rules;

using Assimalign.ComponentModel.Validation.Internal.Exceptions;

internal sealed class NullValidationRule<T, TValue> : IValidationRule
{
    private readonly Expression<Func<T, TValue>> expression;

    public NullValidationRule(Expression<Func<T, TValue>> expression)
    {
        this.expression = expression;
    }


    public string Name => nameof(NullValidationRule<T, TValue>);

    public IValidationError Error { get; set; }

    public void Evaluate(IValidationContext context)
    {
        if (context.Instance is T instance)
        {
            var value = this.GetValue(instance);

            if (value is not null)
            {
                context.AddFailure(this.Error);
            }
            else
            {
                context.AddSuccess(this);
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

