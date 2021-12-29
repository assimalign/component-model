using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Validation.Internal.Rules;

internal sealed class LessThanOrEqualToValidationRule<T, TValue, TArgument> : IValidationRule
    where TArgument : notnull, IComparable
{
    private readonly TArgument argument;
    private readonly Func<TArgument, object, bool> isLessThanOrEqualTo;
    private readonly Expression<Func<T, TValue>> expression;
    private readonly string expressionBody;

    public LessThanOrEqualToValidationRule(Expression<Func<T, TValue>> expression, TArgument argument)
    {
        if (expression is null)
        {
            throw new ArgumentNullException(
                paramName: nameof(expression),
                message: $"The following expression where the 'LessThanOrEqualTo()' rule is defined cannot be null.");
        }
        if (expression.Body is MemberExpression member)
        {
            this.expressionBody = string.Join('.', member.ToString().Split('.').Skip(1));
        }

        this.argument = argument;
        this.expression = expression;
        this.isLessThanOrEqualTo = (arg, val) => arg.CompareTo(val) >= 0;
    }

    public string Name => $"LessThanOrEqualToValidationRule<{typeof(T).Name}, {expressionBody ?? typeof(TValue).Name}, {typeof(TArgument).Name}>";

    public IValidationError Error { get; set; }

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
                    if (item is null || !isLessThanOrEqualTo(argument, item))
                    {
                        context.AddFailure(this.Error);
                        break;
                    }
                }
            }
            else if (!isLessThanOrEqualTo(argument, value))
            {
                context.AddFailure(this.Error);
            }
            else
            {
                context.AddSuccess(this);
            }
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="instance"></param>
    /// <returns></returns>
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