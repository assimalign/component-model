using System;
using System.Collections;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;


namespace Assimalign.ComponentModel.Validation.Internal;


using Assimalign.ComponentModel.Validation.Exceptions;


internal sealed class ValidationConditionRule<T> : IValidationConditionRule<T>
{

    public ValidationConditionRule()
    {
        this.ConditionDefaultRuleSet ??= new ValidationRuleStack();
    }


    /// <summary>
    /// 
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// 
    /// </summary>
    public IValidationRuleStack ConditionRuleSet { get; set; }

    /// <summary>
    /// The rule set to run if condition is not valid.
    /// </summary>
    public IValidationRuleStack ConditionDefaultRuleSet { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Expression<Func<T, bool>> Condition { get; set; }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public void Evaluate(IValidationContext context)
    {
        if (context.Instance is T instance)
        {
            if (this.Condition.Compile().Invoke(instance))
            {
                Parallel.ForEach(this.ConditionRuleSet, rule =>
                {
                    rule.Evaluate(context);
                });
            }
            else if (this.ConditionDefaultRuleSet.Any())
            {
                Parallel.ForEach(this.ConditionDefaultRuleSet, rule =>
                {
                    rule.Evaluate(context);
                });
            }
        }
        else
        {
            throw new ValidationPredicateException("");
        }
    }

    public void Otherwise(Action<IValidationRuleDescriptor<T>> configure)
    {
        var descriptor = new ValidationRuleDescriptor<T>()
        {
            ValidationRules = new ValidationRuleStack()
        };

        configure.Invoke(descriptor);

        foreach(var rule in descriptor.ValidationRules)
        {
            this.ConditionDefaultRuleSet.Push(rule);
        }
    }

    public IValidationConditionRule<T> When(Expression<Func<T, bool>> condition, Action<IValidationRuleDescriptor<T>> configure)
    {
        var descriptor = new ValidationRuleDescriptor<T>()
        {
            ValidationRules = new ValidationRuleStack()
        };

        var rule = new ValidationConditionRule<T>()
        {
            Condition = condition
        };

        //ConditionRuleSet.Add(rule);

        configure.Invoke(descriptor);

        return rule;
    }
}

