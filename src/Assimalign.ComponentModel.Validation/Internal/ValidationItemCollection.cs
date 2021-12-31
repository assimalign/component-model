using System;
using System.Diagnostics;
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

            foreach (var  rule in this.ItemRuleStack)
            {
                var stopwatch = new Stopwatch();

                if (this.ItemValidationMode == ValidationMode.Stop && context.Errors.Any())
                {
                    break;
                }

                stopwatch.Start();
                if (value is not null && value is IEnumerable<TValue> enumerable)
                {
                    foreach (var enumValue in enumerable)
                    {
                        if (rule.TryValidate(enumValue, out var ruleContext))
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
                else
                {
                    stopwatch.Stop();
                    context.AddInvocation(new ValidationInvocation(rule.Name, false, stopwatch.ElapsedTicks));
                }
            }
        }
    }
}


//var tokenSource = new CancellationTokenSource();
//var parallelOptions = new ParallelOptions()
//{
//    CancellationToken = tokenSource.Token
//};

//var results = Parallel.ForEach(this.ItemRuleStack, parallelOptions, (rule, state, index) =>
//{
//    var stopwatch = new Stopwatch();

//    if (this.ItemValidationMode == ValidationMode.Stop && context.Errors.Any())
//    {
//        tokenSource.Cancel();
//    }

//    stopwatch.Start();
//    if (value is not null && value is IEnumerable<TValue> enumerable)
//    {
//        foreach (var enumValue in enumerable)
//        {
//            if (rule.TryValidate(enumValue, out var ruleContext))
//            {
//                foreach (var error in ruleContext.Errors)
//                {
//                    context.AddFailure(error);
//                }

//                stopwatch.Stop();
//                context.AddInvocation(new ValidationInvocation(rule.Name, true, stopwatch.ElapsedTicks));
//            }
//            else
//            {
//                stopwatch.Stop();
//                context.AddInvocation(new ValidationInvocation(rule.Name, false, stopwatch.ElapsedTicks));
//            }
//        }
//    }
//    else
//    {
//        stopwatch.Stop();
//        context.AddInvocation(new ValidationInvocation(rule.Name, false, stopwatch.ElapsedTicks));
//    }
//});