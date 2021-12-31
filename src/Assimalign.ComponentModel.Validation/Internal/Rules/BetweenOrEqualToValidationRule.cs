using System;

namespace Assimalign.ComponentModel.Validation.Internal.Rules;

internal sealed class BetweenOrEqualToValidationRule<TValue, TBound> : ValidationRuleBase<TValue>
    where TBound : notnull, IComparable
{
    private readonly TBound lowerBound;
    private readonly TBound upperBound;
    private readonly Func<TBound, TBound, object, bool> isOutOfBounds;

    public BetweenOrEqualToValidationRule(TBound lowerBound, TBound upperBound)
    {
        this.lowerBound = lowerBound;
        this.upperBound = upperBound;
        this.isOutOfBounds = (lower, upper, value) =>
        {
            var lowerResults = lower.CompareTo(value);
            var upperResults = upper.CompareTo(value);

            return lowerResults > 0 || upperResults < 0;
        };
        this.BoundaryType = typeof(TBound);
    }

    public Type BoundaryType { get; }

    public override string Name { get; set; }

    public override bool TryValidate(object value, out IValidationContext context)
    {
        context = null;

        if (value is null)
        {
            context = new ValidationContext<TValue>(default(TValue));
            context.AddFailure(this.Error);
            return true;
        }
        if (value is TValue tv)
        {
            return TryValidate(tv, out context);
        }
        else
        {
            return false;
        }
    }

    public override bool TryValidate(TValue value, out IValidationContext context)
    {
        try
        {
            context = new ValidationContext<TValue>(value);

            if (value is IConvertible convertible)
            {
                var convertedValue = (TBound)convertible.ToType(this.BoundaryType, default);

                if (isOutOfBounds(this.lowerBound, this.upperBound, convertedValue))
                {
                    context.AddFailure(this.Error);
                }
            }
            else if (isOutOfBounds(this.lowerBound, this.upperBound, value))
            {
                context.AddFailure(this.Error);
            }

            return true;
        }
        catch(InvalidCastException)
        {
            context = new ValidationContext<TValue>(value);

            if (isOutOfBounds(this.lowerBound, this.upperBound, value))
            {
                context.AddFailure(this.Error);
            }

            return true;
        }
        catch
        {
            context = null;
            return false;
        }
    }
}