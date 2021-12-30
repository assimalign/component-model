using System;
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
            var value = this.GetValue(instance);
            var tokenSource = new CancellationTokenSource();
            var parallelOptions = new ParallelOptions()
            {
                CancellationToken = tokenSource.Token
            };

            var results = Parallel.ForEach(this.ItemRuleStack, parallelOptions, (rule, state, index) =>
            {
                var start = DateTime.Now.Ticks;
                var elapsed = (long)0;
                var timer = new System.Timers.Timer();

                timer.AutoReset = true;
                timer.Enabled = true;

                timer.Elapsed += (s,e) =>
                {
                    elapsed = e.SignalTime.Ticks - start;
                };

                if (this.ItemValidationMode == ValidationMode.Stop && context.Errors.Any())
                {
                    tokenSource.Cancel();
                }

                timer.Start();

                if (rule.TryValidate(instance, out var ruleContext))
                {
                    foreach(var error in ruleContext.Errors)
                    {
                        context.AddFailure(error);
                    }

                    timer.Stop();
                    context.AddInvocation(new ValidationInvocation(rule.Name, true, elapsed));
                }
                else
                {
                    timer.Stop();
                    context.AddInvocation(new ValidationInvocation(rule.Name, false));
                }

                timer.Dispose();
            });
        }
    }


    private void OnTimedEvent(object source, System.Timers.ElapsedEventArgs args)
    {

    }
}

