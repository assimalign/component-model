using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Internal;

internal sealed class ValidationItem<T, TValue> : ValidationItemBase<T, TValue>
{
    public override void Evaluate(IValidationContext context)
    {
        if (context.Instance is T instance)
        {
            if (this.ValidationCondition is not null && !this.ValidationCondition.Invoke(instance))
            {
                return;
            }

            var value = this.GetValue(instance);
            var tokenSource = new CancellationTokenSource();
            var parallelOptions = new ParallelOptions()
            {
                CancellationToken = tokenSource.Token
            };


            foreach (var rule in this.ItemRuleStack)
            {
                var stopwatch = new Stopwatch();

                if (this.ItemValidationMode == ValidationMode.Stop && context.Errors.Any())
                {
                    break;
                }

                stopwatch.Start();

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

