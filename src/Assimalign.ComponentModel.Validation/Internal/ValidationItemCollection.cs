using System;
using System.Threading;
using System.Threading.Tasks;
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

            if (value is null || value is IEnumerable<TValue>)
            {
                return value;
            }
            else if (value is )
            {

            }


            //if (value is null || this.ValueType == this.ArgumentType)
            //{
            //    return value; // Let's exit if value is null or if the argument type is the same as the value type
            //}
            //if (this.RuleType == ValidationRuleType.RecursiveRule)
            //{
            //    var enumerableConversions = new List<object>();
            //    var enumerable = value as IEnumerable;

            //    if (enumerable is null)
            //    {
            //        throw new ValidationInternalException(
            //            message: $"Unable to convert expression value: {this.ExpressionBody} to IEnumerable.",
            //            inner: new InvalidCastException($"Type {this.ValueType.Name} is not valid as {this.ArgumentType.Name}"),
            //            source: this.ExpressionBody); // This should never happen, but for safety let's add it for now.
            //    }

            //    foreach (var enumValue in enumerable)
            //    {
            //        if (enumValue is not TArgument && enumValue is IConvertible convertible)
            //        {
            //            enumerableConversions.Add(convertible.ToType(this.ArgumentType, default));
            //        }
            //        else
            //        {
            //            enumerableConversions.Add(enumValue);
            //        }
            //    }

            //    return enumerableConversions;
            //}
            //if (this.RuleType == ValidationRuleType.SingularRule)
            //{
            //    if (value is not TArgument && value is IConvertible convertible)
            //    {
            //        return convertible.ToType(this.ArgumentType, default);
            //    }
            //    else
            //    {
            //        return value;
            //    }
            //}
            //else
            //{
            //    return null; // TODO: Decide whether to throw an exception
            //}
        }
        catch (InvalidCastException exception)
        {
            throw new ValidationInvalidCastException(
                message: $"Unable to equate type '{this.ValueType.Name}' against type '{this.ArgumentType.Name}'. '{this.ExpressionBody}' must be able to convert to {this.ArgumentType}",
                inner: exception,
                source: this.ValidationRuleSource);
        }
        catch (Exception exception) when (exception is not ValidationInvalidCastException)
        {
            return null;
        }
    }
}