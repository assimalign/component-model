using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Internal;

using Assimalign.ComponentModel.Validation.Internal.Exceptions;

internal sealed class ValidationRuleDescriptor<T> : IValidationRuleDescriptor<T>
{
    public IValidationRuleStack ValidationRules { get; set; }

    public ValidationMode ValidationMode { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    public IValidationRuleBuilder<T, TValue> RuleFor<TValue>(Expression<Func<T, TValue>> expression)
    {
        if (expression is null)
        {
            throw new ArgumentNullException(nameof(expression));
        }
        if (expression.Body is not MemberExpression)
        {
            throw new ValidationInvalidMemberException(expression);
        }

        var rule = new ValidationRule<T, TValue>()
        {
            ValidationExpression = expression,
            ValidationMode = this.ValidationMode
        };

        this.ValidationRules.Push(rule);

        return new ValidationRuleBuilder<T, TValue>(rule);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    public IValidationRuleBuilder<T, TValue> RuleForEach<TValue>(Expression<Func<T, TValue>> expression)
        where TValue : IEnumerable
    {
        if (expression is null)
        {
            throw new ArgumentNullException(nameof(expression));
        }
        if (expression.Body is not MemberExpression)
        {
            throw new ValidationInvalidMemberException(expression);
        }

        var rule = new ValidationRule<T, TValue>()
        {
            ValidationExpression = expression,
            ValidationMode = this.ValidationMode
        };

        this.ValidationRules.Push(rule);

        return new ValidationRuleBuilder<T, TValue>(rule);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="condition">What condition is required</param>
    /// <param name="configure">The validation to </param>
    /// <returns></returns>
    public IValidationRuleCondition<T> When(Expression<Func<T, bool>> condition, Action<IValidationRuleDescriptor<T>> configure)
    {
        var rule = new ValidationRuleCondition<T>()
        {
            Condition = condition,
            ConditionRuleSet = new ValidationRuleStack(),
            ValidationMode = this.ValidationMode
        };

        var descriptor = new ValidationRuleDescriptor<T>()
        {
            ValidationRules =  rule.ConditionRuleSet
        };
        
        configure.Invoke(descriptor);

        this.ValidationRules.Push(rule);

        return rule;
    }
}