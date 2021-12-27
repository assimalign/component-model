using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Assimalign.ComponentModel.Validation.Internal;


using Assimalign.ComponentModel.Validation.Internal.Exceptions;

internal sealed class ValidationRule<T, TValue> : IValidationRule<T, TValue>
{

    private Expression<Func<T, TValue>> expression;

    public ValidationRule()
    {
        this.ValidationRules ??= new ValidationRuleStack();
    }


    /// <summary>
    /// 
    /// </summary>
    public Expression<Func<T, TValue>> ValidationExpression
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
                throw new ValidationMemberException();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// 
    /// </summary>
    public IValidationRuleStack ValidationRules { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="rule"></param>
    public IValidationRule<T, TValue> AddRule(IValidationRule rule)
    {
        ValidationRules.Push(rule);
        return this;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public void Evaluate(IValidationContext context)
    {
        if (context.Instance is T instance)
        {
            Parallel.ForEach(this.ValidationRules, (rule, state, index) =>
            {
                rule.Evaluate(context);
            });
        }
    }
}

