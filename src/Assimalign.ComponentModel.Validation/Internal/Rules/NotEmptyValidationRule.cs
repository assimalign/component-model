using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Validation.Internal.Rules;


internal class NotEmptyValidationRule<T, TValue> : IValidationRule 
    where TValue : IEnumerable
{
    private readonly Expression<Func<T, TValue>> expression;
    private readonly string expressionBody;

    public NotEmptyValidationRule(Expression<Func<T, TValue>> expression)
    {
        if (expression is null)
        {
            throw new ArgumentNullException(
                paramName: nameof(expression),
                message: $"The following expression where the 'NotEmpty()' rule is defined cannot be null.");
        }
        if (expression.Body is MemberExpression member)
        {
            this.expressionBody = string.Join('.', member.ToString().Split('.').Skip(1));
        }
        this.expression = expression;
    }

    public string Name => $"NotEmptyValidationRule<{typeof(T).Name}, {expressionBody ?? typeof(TValue).Name}>";

    public IValidationError Error { get; set; }

    public ValidationRuleType RuleType { get; set; }

    public void Evaluate(IValidationContext context)
    {
        if (context.Instance is T instance)
        {
            var value = this.GetValue(instance);

            if (IsEmpty(value))
            {
                context.AddFailure(this.Error);
            }
            else
            {
                context.AddSuccess(this);
            }
        }
    }


    private bool IsEmpty(object member)
    {
        return member switch
        {
            null => true,
            string      stringValue when string.IsNullOrWhiteSpace(stringValue) => true, // May not need this since string is IEnumerable
            ICollection collection  when collection.Count == 0 => true,
            Array       array       when array.Length == 0 => true,
            IEnumerable enumerable  when !enumerable.Cast<object>().Any() => true,
            _ => false
        };
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