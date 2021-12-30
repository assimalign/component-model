using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;


namespace Assimalign.ComponentModel.Validation.Internal.Rules;


internal sealed class LengthValidationRule<TValue> : ValidationRuleBase<TValue>
    where TValue : IEnumerable
{
    private readonly int length;

    public LengthValidationRule(int length)
    {
        this.length = length;
    }

    public override string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


    public override bool TryValidate(object value, out IValidationContext context)
    {
        throw new NotImplementedException();
    }

    public override bool TryValidate(TValue value, out IValidationContext context)
    {
        throw new NotImplementedException();
    }



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
}