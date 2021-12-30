using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Validation.Internal.Rules;

internal sealed class GreaterThanValidationRule<T, TValue, TArgument> : IValidationRule
    where TArgument : notnull, IComparable
{
    private readonly TArgument argument;
    private readonly Func<TArgument, object, bool> isGreaterThan;
    private readonly Expression<Func<T, TValue>> expression;
    private readonly string expressionBody;

    public GreaterThanValidationRule(Expression<Func<T, TValue>> expression, TArgument argument)
    {
        if (expression is null)
        {
            throw new ArgumentNullException(
                paramName: nameof(expression),
                message: $"The following expression where the 'GreaterThan()' rule is defined cannot be null.");
        }
        if (expression.Body is MemberExpression member)
        {
            this.expressionBody = string.Join('.', member.ToString().Split('.').Skip(1));
        }

        this.argument = argument;
        this.expression = expression;
        this.isGreaterThan = (arg, val) => arg.CompareTo(val) < 0; // Is the argument less than the value
    }

    public string Name => $"GreaterThanValidationRule<{typeof(T).Name}, {expressionBody ?? typeof(TValue).Name}, {typeof(TArgument).Name}>";

    public IValidationContext Error { get; set; }

    public ValidationRuleType RuleType { get; set; }

    public void Evaluate(IValidationContext context)
    {
        if (context.Instance is T instance)
        {
            var value = this.GetValue(instance);

            if (value is null)
            {
                context.AddFailure(this.Error);
            }
            else if (value is IEnumerable enumerable)
            {
                foreach (var item in enumerable)
                {
                    if (item is null || !isGreaterThan.Invoke(argument, item))
                    {
                        context.AddFailure(this.Error);
                        break;
                    }
                }
            }
            else if (!isGreaterThan.Invoke(argument, value))
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
            var value = expression.Compile().Invoke(instance);

            if (value is not TArgument && value is IConvertible convertible)
            {
                return convertible.ToType(typeof(TArgument), default);
            }

            return value;
        }
        catch
        {
            return null;
        }
    }
}
