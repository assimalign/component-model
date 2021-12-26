using System;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Validation.Internal.Rules;


using Assimalign.ComponentModel.Validation.Internal.Exceptions;

internal sealed class CustomValidationRule<T, TValue> : IValidationRule, IValidationError
{
    private readonly Expression<Func<T, TValue>> expression;
    private readonly Action<TValue, IValidationContext> validation;

    public CustomValidationRule(
        Expression<Func<T, TValue>> expression,
        Action<TValue, IValidationContext> validation)
    {
        this.expression = expression;
        this.validation = validation;
    }

    public string Name { get; }
    public string Code { get; set; }
    public string Message { get; set; }
    public string Source { get; set; }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public void Evaluate(IValidationContext context)
    {
        if (context.Instance is T instance)
        {
            var member = this.expression.Compile().Invoke(instance);

            validation.Invoke(member, context);
        }
        else
        {
            throw new ValidationInternalException("");
        }
    }
}
