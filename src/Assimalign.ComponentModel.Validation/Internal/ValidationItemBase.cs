using System;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Validation.Internal;

using Assimalign.ComponentModel.Validation.Internal.Exceptions;

internal abstract class ValidationItemBase<T, TValue> : IValidationItem<T, TValue>
{
    private Expression<Func<T, TValue>> expression;

    public ValidationItemBase()
    {
        this.ItemRuleStack ??= new ValidationRuleStack();
    }

    public Expression<Func<T, TValue>> ItemExpression
    {
        get => expression;
        set
        {
            // Only member expressions are supported for validation
            if (value.Body is MemberExpression)
            {
                this.expression = value;
            }
            else
            {
                throw new ValidationInvalidMemberException(value);
            }
        }
    }

    public ValidationMode ItemValidationMode { get; set; }

    public IValidationRuleStack ItemRuleStack { get; }


    public abstract void Evaluate(IValidationContext context);

    public virtual object GetValue(T instance)
    {
        try
        {
            var value =  this.ItemExpression.Compile().Invoke(instance);

            if (value is null || value is TValue)
            {
                return value;
            }
            else if (value is IConvertible convertable)
            {
                return convertable.ToType(typeof(TValue), default);
            }
            else
            {
                return value;
            }
        }
        catch
        {
            return null;
        }
    }
}

