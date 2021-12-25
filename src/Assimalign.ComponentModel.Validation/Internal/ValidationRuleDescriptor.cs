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
    /// <typeparam name="TMember"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    public IValidationRuleBuilder<T, TMember> RuleFor<TMember>(Expression<Func<T, TMember>> expression)
    {
        if (expression.Body is not MemberExpression)
        {
            throw new ArgumentException($"The following expression must be a member of type: {nameof(T)}");
        }

        var rule = new ValidationMemberRule<T, TMember>()
        {
            Member = expression
        };

        this.ValidationRules.Push(rule);

        return new ValidationMemberRuleBuilder<T, TMember>(rule);
    }



    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCollection"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    public IValidationRuleBuilder<T, TCollection> RuleForEach<TCollection>(Expression<Func<T, TCollection>> expression)
        where TCollection : IEnumerable
    {
        var rule = new ValidationCollectionRule<T, TCollection>()
        {
            Collection = expression
        };

        this.ValidationRules.Push(rule);

        return new ValidationCollectionRuleBuilder<T, TCollection>(rule);
    }





    /// <summary>
    /// 
    /// </summary>
    /// <param name="condition">What condition is required</param>
    /// <param name="configure">The validation to </param>
    /// <returns></returns>
    public IValidationConditionRule<T> When(Expression<Func<T, bool>> condition, Action<IValidationRuleDescriptor<T>> configure)
    {
        var rule = new ValidationConditionRule<T>()
        {
            Condition = condition,
            ConditionRuleSet = new ValidationRuleStack()
        };

        var descriptor = new ValidationRuleDescriptor<T>()
        {
            ValidationRules =  rule.ConditionRuleSet
        };

        this.ValidationRules.Push(rule);
        
        configure.Invoke(descriptor);

        return rule;
    }
}