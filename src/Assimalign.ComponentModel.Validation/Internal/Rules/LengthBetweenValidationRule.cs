using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Validation.Internal.Rules;

internal sealed class LengthBetweenValidationRule<TValue> : ValidationRuleBase<TValue>
    where TValue : IEnumerable
{
    private readonly int lowerBound;
    private readonly int upperBound;


    public LengthBetweenValidationRule( int lowerBound, int upperBound)
    {
        this.upperBound = upperBound;
        this.lowerBound = lowerBound;
    }

    public override string Name { get; set; }


    public override bool TryValidate(object value, out IValidationContext context)
    {
        throw new NotImplementedException();
    }

    public override bool TryValidate(TValue value, out IValidationContext context)
    {
        throw new NotImplementedException();
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
}
