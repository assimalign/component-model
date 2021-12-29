using System;
using System.Collections;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Validation.Internal.Rules;

internal sealed class BetweenValidationRule<T, TValue, TBound> : ValidationRuleBase<T, TValue, TBound>
    where TBound : notnull, IComparable
{
    private readonly TBound lower;
    private readonly TBound upper;
    private readonly Func<TBound, TBound, object, bool> isOutOfBounds;

    public BetweenValidationRule(Expression<Func<T, TValue>> expression, TBound lower, TBound upper) : base(expression)
    {
        if (expression is null)
        {
            throw new ArgumentNullException(
                paramName: nameof(expression), 
                message: $"The following expression where the 'Between()' rule is defined cannot be null.");
        }
        this.lower = lower;
        this.upper = upper;
        this.isOutOfBounds = (lower, upper, value) =>
        {
            var lowerResults = lower.CompareTo(value);
            var upperResults = upper.CompareTo(value);

            return lowerResults >= 0 || upperResults <= 0;
        };

        this.ValidationRuleSource = $"RuleFor(p => p.{this.ExpressionBody}).Between({this.lower}, {this.upper})";
    }

    public override string Name => $"BetweenValidationRule<{this.ParamType.Name},{this.ExpressionBody ?? this.ValueType.Name},{this.ArgumentType.Name}>";

    public override void Evaluate(IValidationContext context)
    {
        if (context.Instance is T instance)
        {
            var value = this.GetValue(instance);

            if (value is null)
            {
                context.AddFailure(this.Error);
            }
            else if (this.RuleType == ValidationRuleType.RecursiveRule)
            {
                if (value is IEnumerable enumerable)
                {
                    foreach (var item in enumerable)
                    {
                        if (item is null || isOutOfBounds(this.lower, this.upper, item))
                        {
                            context.AddFailure(this.Error);
                            break;
                        }
                    }
                }
            }
            else if (this.RuleType == ValidationRuleType.SingularRule && isOutOfBounds(this.lower, this.upper, value))
            {
                context.AddFailure(this.Error);
            }
            else
            {
                context.AddSuccess(this);
            }
        }
    }
}
