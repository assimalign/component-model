using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Validation.Internal;


using Assimalign.ComponentModel.Validation.Internal.Exceptions;

internal sealed class ValidationRule<T, TValue> : IValidationRule<T, TValue>
{
    private Expression<Func<T, TValue>> expression;

    public ValidationRule()
    {
        this.ValidationRules ??= new ValidationRuleStack();
    }

    public Expression<Func<T, TValue>> ValidationExpression
    {
        get => expression;
        set
        {
            // Only member expressions are supported for validation
            if (value.Body is MemberExpression)
            {
                this.expression = value;
            }
            else
            {
                throw new ValidationInvalidMemberException(value);
            }
        }
    }

    public string Name => nameof(ValidationRule<T, TValue>);

    public ValidationMode ValidationMode { get; set; }

    public IValidationRuleStack ValidationRules { get; set; }

    public IValidationRule<T, TValue> AddRule(IValidationRule rule)
    {
        ValidationRules.Push(rule);
        return this;
    }

    public void Evaluate(IValidationContext context)
    {
        if (context.Instance is T instance)
        {
            var tokenSource = new CancellationTokenSource();
            var parallelOptions = new ParallelOptions()
            {
                CancellationToken = tokenSource.Token
            };

            var results = Parallel.ForEach(this.ValidationRules, parallelOptions, (rule, state, index) =>
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

