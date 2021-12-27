using System;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Validation.Internal.Rules;

using Assimalign.ComponentModel.Validation.Internal.Exceptions;

internal sealed class CustomValidationRule<T, TValue> : IValidationRule
{
    private readonly Expression<Func<T, TValue>> expression;
    private readonly Action<TValue, IValidationContext> validation;

    public CustomValidationRule(
        Expression<Func<T, TValue>> expression,
        Action<TValue, IValidationContext> validation)
    {
        if (expression is null)
        {
            throw new ArgumentNullException(nameof(expression));
        }
        if (validation is null)
        {
            throw new ArgumentNullException(nameof(validation));
        }

        
        this.expression = expression;
        this.validation = validation;
    }

    public string Name => nameof(CustomValidationRule<T, TValue>);


    public void Evaluate(IValidationContext context)
    {
        if (context.Instance is T instance)
        {
            var member = this.GetValue(instance);

            validation.Invoke(member, context);
        }
        else
        {
            throw new ValidationInternalException("");
        }
    }


    private TValue GetValue(T instance)
    {
        try
        {
            return this.expression.Compile().Invoke(instance);
        }
        catch
        {
            return default(TValue);
        }
    }
}
