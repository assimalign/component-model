using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections;

namespace Assimalign.ComponentModel.Validation.Internal.Rules;

internal sealed class CustomValidationRule<T, TValue> : ValidationRuleBase<T, TValue>
{
    private readonly Action<TValue, IValidationContext> validation;

    public CustomValidationRule(Expression<Func<T, TValue>> expression,Action<TValue, IValidationContext> validation) : base(expression)
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

        this.validation = validation;
    }

    public override string Name => $"CustomValidationRule<{this.ParamType.Name}, {this.ExpressionBody ?? this.ValueType.Name}>";

    public override void Evaluate(IValidationContext context)
    {
        if (context.Instance is T instance)
        {
            var value = this.GetValue(instance);

            if (this.RuleType == ValidationRuleType.SingularRule)
            {
                validation.Invoke(value, context);
            }
            else if (this.RuleType == ValidationRuleType.RecursiveRule)
            {
                if (value is IEnumerable enumerable)
                {
                    foreach(var enumValue in enumerable)
                    {

                    }

                }
            }
        }
    }


    private TValue GetValue(T instance)
    {
        try
        {
            return this.Expression.Compile().Invoke(instance);
        }
        catch
        {
            return default(TValue);
        }
    }
}
