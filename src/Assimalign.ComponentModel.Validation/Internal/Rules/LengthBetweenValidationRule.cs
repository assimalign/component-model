using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Validation.Internal.Rules;

internal sealed class LengthBetweenValidationRule<T, TValue> : IValidationRule
    where TValue : IEnumerable
{
    private readonly int lowerBound;
    private readonly int upperBound;
    private readonly Expression<Func<T, TValue>> expression;
    private readonly string expressionBody;


    public LengthBetweenValidationRule(Expression<Func<T, TValue>> expression, int lowerBound, int upperBound)
    {
        if (expression is null)
        {
            throw new ArgumentNullException(
                paramName: nameof(expression),
                message: $"The following expression where the 'Length()' rule is defined cannot be null.");
        }
        if (expression.Body is MemberExpression member)
        {
            this.expressionBody = string.Join('.', member.ToString().Split('.').Skip(1));
        }

        this.upperBound = upperBound;
        this.lowerBound = lowerBound;
        this.expression = expression;
    }

    public string Name => $"LengthValidationRule<{typeof(T).Name}, {expressionBody ?? typeof(TValue).Name}>";

    public IValidationError Error { get; set; }

    public ValidationRuleType RuleType { get; set; }

    public void Evaluate(IValidationContext context)
    {
        if (context.Instance is T instance)
        {
            var value = this.GetValue(instance);

            if (!IsBetweenLength(value))
            {
                context.AddFailure(this.Error);
            }
            else
            {
                context.AddSuccess(this);
            }
        }
    }

    private bool IsBetweenLength(object member)
    {
        return member switch
        {
            null => true,
            string stringValue      when stringValue is not null && stringValue.Length >= this.lowerBound && stringValue.Length <= this.upperBound => true,
            ICollection collection  when collection.Count >= this.lowerBound && collection.Count <= this.upperBound => true,
            Array array             when array.Length >= this.lowerBound && array.Length <= this.upperBound => true,
            IEnumerable enumerable  when enumerable.Cast<object>().Count() >= this.lowerBound  && enumerable.Cast<object>().Count() <= this.upperBound => true,
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
