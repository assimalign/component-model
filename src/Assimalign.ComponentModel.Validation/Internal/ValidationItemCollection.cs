using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Validation.Internal;

using Assimalign.ComponentModel.Validation.Internal.Exceptions;

internal sealed class ValidationItemCollection<T, TValue> : ValidationItemBase<T, IEnumerable<TValue>>
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

            var results = Parallel.ForEach(this.ItemRuleStack, parallelOptions, (rule, state, index) =>
            {
                var start = DateTime.Now.Ticks;
                var elapsed = (long)0;
                var timer = new System.Timers.Timer()
                {
                    AutoReset = true,
                    Enabled = true,
                };

                timer.Elapsed += (s, e) =>
                {
                    elapsed = e.SignalTime.Ticks - start;
                };

                if (this.ItemValidationMode == ValidationMode.Stop && context.Errors.Any())
                {
                    tokenSource.Cancel();
                }

                timer.Start();
                if (value is not null && value is IEnumerable<TValue> enumerable)
                {
                    foreach (var enumValue in enumerable)
                    {
                        if (rule.TryValidate(instance, out var ruleContext))
                        {
                            foreach (var error in ruleContext.Errors)
                            {
                                context.AddFailure(error);
                            }

                            timer.Stop();
                            context.AddInvocation(new ValidationInvocation(rule.Name, true));
                        }
                        else
                        {
                            timer.Stop();
                            context.AddInvocation(new ValidationInvocation(rule.Name, false));
                        }
                    }
                }
                else
                {
                    context.AddInvocation(new ValidationInvocation(rule.Name, false));
                }

                timer.Dispose();
            });
        }
    }

    public override object GetValue(T instance)
    {
        try
        {
            var value = this.ItemExpression.Compile().Invoke(instance);

            if (value is null || value is IEnumerable)
            {
                return value;
            }
            else
            {
                throw new Exception("");
            }
        }
        //catch (InvalidCastException exception)
        //{
        //    throw new ValidationInvalidCastException(
        //        message: $"Unable to equate type '{this.ValueType.Name}' against type '{this.ArgumentType.Name}'. '{this.ExpressionBody}' must be able to convert to {this.ArgumentType}",
        //        inner: exception,
        //        source: this.ValidationRuleSource);
        //}
        catch (Exception exception) when (exception is not ValidationInvalidCastException)
        {
            return null;
        }
    }
}