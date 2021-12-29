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
                if (this.ItemValidationMode == ValidationMode.Stop && context.Errors.Any())
                {
                    tokenSource.Cancel();
                }

                if (!rule.IsValid(instance, out var error))
                {
                    context.AddFailure(error);
                }
            });
        }
    }
}

