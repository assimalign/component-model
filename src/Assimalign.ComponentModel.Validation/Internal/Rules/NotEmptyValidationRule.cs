using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Validation.Internal.Rules;


internal class NotEmptyValidationRule<T, TValue> : IValidationRule 
    where TValue : IEnumerable
{

    private readonly Expression<Func<T, TValue>> expression;

    public NotEmptyValidationRule(Expression<Func<T, TValue>> expression)
    {
        this.expression = expression;
    }

    public string Name => nameof(NotEmptyValidationRule<T, TValue>);

    public IValidationError Error { get; set; }

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
        else
        {
            // TODO: 
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