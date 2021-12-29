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
            var value = this.GetValue(instance);
            var tokenSource = new CancellationTokenSource();
            var parallelOptions = new ParallelOptions()
            {
                CancellationToken = tokenSource.Token
            };

            var results = Parallel.ForEach(this.ItemRuleStack, parallelOptions, (rule, state, index) =>
            {
                if (this.ItemValidationMode == ValidationMode.Stop && context.Errors.Any())
                {
                    tokenSource.Cancel();
                }

                if (value is not null && value is IEnumerable<TValue> enumerable)
                {
                    foreach (var enumValue in enumerable)
                    {
                        if (!rule.IsValid(instance, out var error))
                        {
                            context.AddFailure(error);
                            break;
                        }
                    }
                }
                else
                {
                    if (!rule.IsValid(instance, out var error))
                    {
                        context.AddFailure(error);
                    }
                }
            });
        }
    }
}