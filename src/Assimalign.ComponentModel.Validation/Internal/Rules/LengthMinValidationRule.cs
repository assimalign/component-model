﻿using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Validation.Internal.Rules;


internal sealed class LengthMinValidationRule<T, TValue> : IValidationRule
    where TValue : IEnumerable
{
    private readonly int length;
    private readonly Expression<Func<T, TValue>> expression;
    private readonly string expressionBody;

    public LengthMinValidationRule(Expression<Func<T, TValue>> expression, int length)
    {
        if (expression is null)
        {
            throw new ArgumentNullException(
                paramName: nameof(expression),
                message: $"The following expression where the 'MinLength()' rule is defined cannot be null.");
        }
        if (expression.Body is MemberExpression member)
        {
            this.expressionBody = string.Join('.', member.ToString().Split('.').Skip(1));
        }

        this.length = length;
        this.expression = expression;
    }

    public string Name => $"LengthMinValidationRule<{typeof(T).Name}, {expressionBody ?? typeof(TValue).Name}>";

    public IValidationError Error { get; set; }

    public ValidationRuleType RuleType { get; set; }

    public void Evaluate(IValidationContext context)
    {
        if (context.Instance is T instance)
        {
            var value = this.GetValue(instance);

            if (this.IsUnderMinLength(value))
            {
                context.AddFailure(this.Error);
            }
            else
            {
                context.AddSuccess(this);
            }
        }
    }

    private bool IsUnderMinLength(object member)
    {
        return member switch
        {
            null => true,
            string stringValue      when stringValue is not null && stringValue.Length < this.length => true,
            ICollection collection  when collection.Count < this.length => true,
            Array array             when array.Length < this.length => true,
            IEnumerable enumerable  when enumerable.Cast<object>().Count() < this.length => true,
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

