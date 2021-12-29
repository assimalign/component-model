using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;


namespace Assimalign.ComponentModel.Validation.Internal;


using Assimalign.ComponentModel.Validation.Internal.Exceptions;


internal sealed class ValidationRuleCondition<T> : IValidationRuleCondition<T>
{

    public ValidationRuleCondition()
    {
        this.ConditionDefaultRuleSet ??= new ValidationRuleStack();
    }

    public ValidationMode ValidationMode { get; set; }

    public string Name => nameof(ValidationRuleCondition<T>);

    public IValidationRuleStack ConditionRuleSet { get; set; }

    public IValidationRuleStack ConditionDefaultRuleSet { get; set; }

    public Expression<Func<T, bool>> Condition { get; set; }

    public void Evaluate(IValidationContext context)
    {
        if (context.Instance is T instance)
        {
            var tokenSource = new CancellationTokenSource();
            var parallelOptions = new ParallelOptions()
            {
                CancellationToken = tokenSource.Token
            };

            if (this.Condition.Compile().Invoke(instance))
            {
                Parallel.ForEach(this.ConditionRuleSet, rule =>
                {
                    if (this.ValidationMode == ValidationMode.Stop && context.Errors.Any())
                    {
                        tokenSource.Cancel();
                    }

                    rule.Evaluate(context);
                });
            }
            else if (this.ConditionDefaultRuleSet is not null && this.ConditionDefaultRuleSet.Any())
            {
                Parallel.ForEach(this.ConditionDefaultRuleSet, rule =>
                {
                    if (this.ValidationMode == ValidationMode.Stop && context.Errors.Any())
                    {
                        tokenSource.Cancel();
                    }

                    rule.Evaluate(context);
                });
            }
        }
    }

    public void Otherwise(Action<IValidationRuleDescriptor<T>> configure)
    {
        var descriptor = new ValidationRuleDescriptor<T>()
        {
            ValidationRules = this.ConditionDefaultRuleSet
        };

        configure.Invoke(descriptor);
    }

    public IValidationRuleCondition<T> When(Expression<Func<T, bool>> condition, Action<IValidationRuleDescriptor<T>> configure)
    {
        var descriptor = new ValidationRuleDescriptor<T>()
        {
            ValidationMode = this.ValidationMode
        };

        var rule = new ValidationRuleCondition<T>()
        {
            Condition = condition,
            ValidationMode = this.ValidationMode
        };

        ConditionRuleSet.Push(rule);

        configure.Invoke(descriptor);

        return rule;
    }
}

