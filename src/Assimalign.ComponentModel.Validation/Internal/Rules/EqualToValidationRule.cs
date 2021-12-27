using System;
using System.Linq.Expressions;


namespace Assimalign.ComponentModel.Validation.Internal.Rules;


internal sealed class EqualToValidationRule<T, TValue, TArgument> : IValidationRule
{
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

            if (!this.isEqualTo(value))
            {
                context.AddFailure(this.Error);
            }
        }
    }


    private object GetValue(T instance)
    {
        try
        {
            return this.expression.Compile().Invoke(instance);
        }
        catch
        {
            return default(TValue);
        }
    }
}

