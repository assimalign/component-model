using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;


namespace Assimalign.ComponentModel.Validation.Internal.Rules;


internal sealed class LengthValidationRule<T, TValue> : IValidationRule
    where TValue : IEnumerable
{
    private readonly int length;
    private readonly Expression<Func<T, TValue>> expression;

    public LengthValidationRule(Expression<Func<T, TValue>> expression, int length)
    {
        if (expression is null)
        {
            throw new ArgumentNullException(nameof(expression));
        }

        this.length = length;
        this.expression = expression;
    }

    public string Name => nameof(LengthValidationRule<T, TValue>);

    public IValidationError Error { get; set; }

    public void Evaluate(IValidationContext context)
    {
        if (context is T instance)
        {
            var value = this.GetValue(instance);

            if (!IsLength(value))
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

        }
    }


    private bool IsLength(object member)
    {
        return member switch
        {
            null => true,
            string stringValue      when stringValue is not null && stringValue.Length == this.length => true, // May not need this since string is IEnumerable
            ICollection collection  when collection.Count == this.length => true,
            Array array             when array.Length == this.length => true,
            IEnumerable enumerable  when enumerable.Cast<object>().Count() == this.length => true,
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

