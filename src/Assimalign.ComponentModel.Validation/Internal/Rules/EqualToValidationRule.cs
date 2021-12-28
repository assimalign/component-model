using System;
using System.Collections;
using System.Linq.Expressions;


namespace Assimalign.ComponentModel.Validation.Internal.Rules;


internal sealed class EqualToValidationRule<T, TValue, TArgument> : IValidationRule
{
    private readonly TArgument argument;
    private readonly Func<object, bool> isEqualTo;
    private readonly Expression<Func<T, TValue>> expression;

    public EqualToValidationRule(Expression<Func<T, TValue>> expression, TArgument argument)
    {
        if (expression is null)
        {
            throw new ArgumentNullException(nameof(expression));
        }
        if (argument is null)
        {
            throw new ArgumentNullException(nameof(argument));
        }

        this.argument = argument;
        this.expression = expression;
        this.isEqualTo = x => x.Equals(argument);        
    }

    public string Name => nameof(EqualToValidationRule<T, TValue, TArgument>);

    public IValidationError Error { get; set; }

    public void Evaluate(IValidationContext context)
    {
        if (context.Instance is T instance)
        {
            var value = this.GetValue(instance);

            if (value is null)
            {
                context.AddFailure(this.Error);
            }
            else if (argument is IEquatable<TArgument> equatable && value is TArgument equatableArgument && !equatable.Equals(equatableArgument))
            {
                context.AddFailure(this.Error);
            }
            else if (argument is IEqualityComparer comparer && !comparer.Equals(value))
            {
                context.AddFailure(this.Error);
            }
            else if (!this.isEqualTo(value))
            {
                context.AddFailure(this.Error);
            }
            else
            {
                context.AddSuccess(this);
            }
        }
    }


    private object GetValue(T instance)
    {
        try
        {
            var value = this.expression.Compile().Invoke(instance);

            if (value is not TArgument && value is IConvertible convertable) // Need to convert when trying to compare
            {
                return convertable.ToType(typeof(TArgument), default);
            }

            return value;
        }
        catch
        {
            return default(TValue);
        }
    }
}

