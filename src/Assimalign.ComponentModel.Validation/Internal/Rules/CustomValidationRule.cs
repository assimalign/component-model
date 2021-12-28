using System;
using System.Linq;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Validation.Internal.Rules;

internal sealed class CustomValidationRule<T, TValue> : IValidationRule
{
    private readonly Expression<Func<T, TValue>> expression;
    private readonly Action<TValue, IValidationContext> validation;
    private readonly string expressionBody;

    public CustomValidationRule(
        Expression<Func<T, TValue>> expression,
        Action<TValue, IValidationContext> validation)
    {
        if (expression is null)
        {
            throw new ArgumentNullException(
                paramName: nameof(expression),
                message: $"The following expression where the 'Custom()' rule is defined cannot be null.");
        }
        if (validation is null)
        {
            throw new ArgumentNullException(nameof(validation));
        }
        if (expression.Body is MemberExpression member)
        {
            this.expressionBody = string.Join('.', member.ToString().Split('.').Skip(1));
        }

        this.expression = expression;
        this.validation = validation;
    }

    public string Name => $"CustomValidationRule<{typeof(T).Name}, {expressionBody ?? typeof(TValue).Name}>";


    public void Evaluate(IValidationContext context)
    {
        if (context.Instance is T instance)
        {
            var member = this.GetValue(instance);

            validation.Invoke(member, context);
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
