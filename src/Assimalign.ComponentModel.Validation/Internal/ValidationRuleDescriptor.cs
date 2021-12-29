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
    public IList<IValidationItem> ValidationItems { get; set; }

    public ValidationMode ValidationMode { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    public IValidationRuleBuilder<TValue> RuleFor<TValue>(Expression<Func<T, TValue>> expression)
    {
        if (expression is null)
        {
            throw new ArgumentNullException(nameof(expression));
        }
        if (expression.Body is not MemberExpression)
        {
            throw new ValidationInvalidMemberException(expression);
        }

        var item = new ValidationItem<T, TValue>()
        {
            ItemExpression = expression,
            ItemValidationMode = this.ValidationMode
        };

        this.ValidationItems.Add(item);

        return new ValidationRuleBuilder<T, TValue>(item);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    public IValidationRuleBuilder<TValue> RuleForEach<TValue>(Expression<Func<T, IEnumerable<TValue>>> expression)
    {
        if (expression is null)
        {
            throw new ArgumentNullException(nameof(expression));
        }
        if (expression.Body is not MemberExpression)
        {
            throw new ValidationInvalidMemberException(expression);
        }

        var item = new ValidationItemCollection<T, TValue>()
        {
            ItemExpression = expression,
            ItemValidationMode = this.ValidationMode
        };

        this.ValidationItems.Add(item);

        return new ValidationRuleBuilder<T, TValue>(item);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="condition">What condition is required</param>
    /// <param name="configure">The validation to </param>
    /// <returns></returns>
    public IValidationItemCondition<T> When(Expression<Func<T, bool>> condition, Action<IValidationRuleDescriptor<T>> configure)
    {
        var rule = new ValidationItemCondition<T>()
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