using System;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Validation.Internal;

using Assimalign.ComponentModel.Validation.Internal.Exceptions;

internal abstract class ValidationItemBase<T, TValue> : IValidationItem<T, TValue>
{
    private string expressionBody;
    private Func<T, TValue> expressionCompiled;
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
                this.expressionBody = expression.ToString();
                this.expressionCompiled = this.expression.Compile();
            }
            else
            {
                throw new ValidationInvalidMemberException(value);
            }
        }
    }

    public Func<T,bool> ValidationCondition { get; set; }

    public ValidationMode ItemValidationMode { get; set; }

    public IValidationRuleStack ItemRuleStack { get; }

    public abstract void Evaluate(IValidationContext context);

    public virtual TValue GetValue(T instance)
    {
        try
        {
            return this.expressionCompiled.Invoke(instance);
        }
        catch // Null Reference Exceptions tend to be thrown when chained members in a type are null.
        {
            return default(TValue);
        }
    }

    public override string ToString() => this.expressionBody;
}

