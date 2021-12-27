using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Validation.Internal.Rules;


internal sealed class EmptyValidationRule<T, TValue> : IValidationRule
{
    private readonly Expression<Func<T, TValue>> expression;

    public EmptyValidationRule(Expression<Func<T, TValue>> expression)
    {
        if (expression is null)
        {
            throw new ArgumentNullException(nameof(expression));
        }
        this.expression = expression;
    }


    public string Name => nameof(EmptyValidationRule<T, TValue>);

    public IValidationError Error { get; set; }

    public void Evaluate(IValidationContext context)
    {
        if (context.Instance is T instance)
        {
            var value = this.GetValue(instance);

            if (!IsEmpty(value))
            {
                context.AddFailure(this.Error);
            }
        }
        else
        {

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
            return default(TValue);
        }
    }
}
