using System;
using System.Diagnostics;
using System.Linq;

namespace Assimalign.ComponentModel.Validation.Internal;

using Assimalign.ComponentModel.Validation.Internal.Extensions;

internal sealed class ValidationItem<T, TValue> : ValidationItemBase<T, TValue>
{

    public override void Evaluate(IValidationContext context)
    {
        var isUseEntireChain = context.Options.TryGetValue("ContinueThroughValidationChain", out var results) ? 
            (bool)results : 
            false;

        if (context.Instance is T instance)
        {
            if (this.ValidationCondition is not null && !this.ValidationCondition.Invoke(instance))
            {
                return;
            }

            var value = this.GetValue(instance);
            var stopwatch = new Stopwatch();

            foreach (var rule in this.ItemRuleStack)
            {
                if (!isUseEntireChain && context.Errors.Any())
                {
                    break;
                }

                stopwatch.Restart();

                if (rule.TryValidate(value, out var ruleContext))
                {
                    foreach (var error in ruleContext.Errors)
                    {
                        context.AddFailure(error);
                    }

                    stopwatch.Stop();
                    context.AddInvocation(new ValidationInvocation(rule.Name, true, stopwatch.ElapsedTicks));
                }
                else
                {
                    stopwatch.Stop();
                    context.AddInvocation(new ValidationInvocation(rule.Name, false, stopwatch.ElapsedTicks));
                }
            }
        }
    }
}