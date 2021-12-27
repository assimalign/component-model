using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Internal;


internal sealed class ValidationRuleDescriptor<T> : IValidationRuleDescriptor<T>
{

    /// <summary>
    /// 
    /// </summary>
    public IValidationRuleStack ValidationRules { get; set; }


    public IValidationRule Current { get; set; }

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
            throw new ArgumentException($"The following expression must be a member of type: {nameof(T)}");
        }

        var rule = new ValidationRule<T, TValue>()
        {
            ValidationExpression = expression
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
            throw new ArgumentException($"The following expression must be a member of type: {nameof(T)}");
        }

        var rule = new ValidationRule<T, TValue>()
        {
            ValidationExpression = expression
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
    public IValidationCondition<T> When(Expression<Func<T, bool>> condition, Action<IValidationRuleDescriptor<T>> configure)
    {
        var rule = new ValidationCondition<T>()
        {
            Condition = condition,
            ConditionRuleSet = new ValidationRuleStack()
        };

        var descriptor = new ValidationRuleDescriptor<T>()
        {
            ValidationRules =  rule.ConditionRuleSet
        };

        // this.ValidationRules.Push(rule);
        
        configure.Invoke(descriptor);

        return rule;
    }
}