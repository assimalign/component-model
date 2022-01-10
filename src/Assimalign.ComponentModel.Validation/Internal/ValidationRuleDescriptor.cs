using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Validation.Internal;

using Assimalign.ComponentModel.Validation.Internal.Exceptions;

internal sealed class ValidationRuleDescriptor<T> : IValidationRuleDescriptor<T>
{
    public IList<IValidationItem> ValidationItems { get; set; }

    public Func<T, bool> ValidationCondition { get; set; }


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
            throw new ArgumentNullException(
                paramName: nameof(expression),
                message: $"A null expression passed occurred at RuleFor<TValue>(Expression<Func<T, TValue>> expression).");
        }
        if (expression.Body is not MemberExpression)
        {
            throw new ValidationInvalidMemberException(expression);
        }

        var item = new ValidationItem<T, TValue>()
        {
            ItemExpression = expression,
            ValidationCondition = this.ValidationCondition
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
            throw new ArgumentNullException(
                paramName: nameof(expression),
                message: $"A null expression passed occurred at RuleForEach<TValue>(Expression<Func<T, IEnumerable<TValue>>> expression).");
        }
        if (expression.Body is not MemberExpression)
        {
            throw new ValidationInvalidMemberException(expression);
        }

        var item = new ValidationItemCollection<T, TValue>()
        {
            ItemExpression = expression,
            ValidationCondition = this.ValidationCondition
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
    public IValidationCondition<T> When(Expression<Func<T, bool>> condition, Action<IValidationRuleDescriptor<T>> configure)
    {
        var validationCondition = new ValidationCondition<T>()
        {
            Condition = condition,
            ValidationItems = this.ValidationItems,
        };

        var descriptor = new ValidationRuleDescriptor<T>()
        {
            ValidationItems = new List<IValidationItem>(),
            ValidationCondition = condition.Compile()
        };
        
        configure.Invoke(descriptor);

        foreach(var item in descriptor.ValidationItems)
        {
            this.ValidationItems.Add(item);
        }

        return validationCondition;
    }
}