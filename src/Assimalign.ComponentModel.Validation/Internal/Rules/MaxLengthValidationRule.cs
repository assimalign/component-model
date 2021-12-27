using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Validation.Internal.Rules;


internal sealed class MaxLengthValidationRule<T, TValue> : IValidationRule
{
    private readonly int length;
    private readonly Expression<Func<T, TValue>> expression;

    public MaxLengthValidationRule(Expression<Func<T, TValue>> expression, int length)
    {
        if (expression is null)
        {
            throw new ArgumentNullException(nameof(expression));
        }

        this.length = length;
        this.expression = expression;
    }

    public string Name => nameof(MaxLengthValidationRule<T, TValue>);

    public IValidationError Error { get; set; }
    
    public void Evaluate(IValidationContext context)
    {
        if (context.Instance is T instance)
        {
            var value = this.GetValue(instance);

            if (this.IsOverMaxLength(value))
            {
                context.AddFailure(this.Error);
            }
        }
        else
        {

        }
    }

    private bool IsOverMaxLength(object member)
    {
        return member switch
        {
            null => true,
            string stringValue      when stringValue is not null && stringValue.Length > this.length => true,
            ICollection collection  when collection.Count > this.length => true,
            Array array             when array.Length > this.length => true,
            IEnumerable enumerable  when enumerable.Cast<object>().Count() > this.length => true,
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