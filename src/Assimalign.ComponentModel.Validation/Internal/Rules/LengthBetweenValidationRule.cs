using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Internal.Rules;

internal sealed class LengthBetweenValidationRule<T, TValue> : IValidationRule
{
    private readonly int lowerBound;
    private readonly int upperBound;
    private readonly Expression<Func<T, TValue>> expression;


    public LengthBetweenValidationRule(Expression<Func<T, TValue>> expression, int lowerBound, int upperBound)
    {
        if (expression is null)
        {
            throw new ArgumentNullException(nameof(expression));
        }

        this.upperBound = upperBound;
        this.lowerBound = lowerBound;
        this.expression = expression;
    }

    public string Name => nameof(LengthBetweenValidationRule<T, TValue>);

    public IValidationError Error { get; set; }

    public void Evaluate(IValidationContext context)
    {
        if (context.Instance is T instance)
        {
            var value = this.GetValue(instance);

            if (!IsBetweenLength(value))
            {
                context.AddFailure(this.Error);
            }
        }
        else
        {

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
