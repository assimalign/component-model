using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Validation.Internal.Rules;

internal sealed class LessThanValidationRule<T, TValue, TArgument> : IValidationRule
    where TArgument : notnull, IComparable
{
    private readonly TArgument argument;
    private readonly Func<TArgument, object, bool> isLessThan;
    private readonly Expression<Func<T, TValue>> expression;
    private readonly string expressionBody;

    public LessThanValidationRule(Expression<Func<T, TValue>> expression, TArgument argument)
    {
        if (expression is null)
        {
            throw new ArgumentNullException(
                paramName: nameof(expression),
                message: $"The following expression where the 'LessThan()' rule is defined cannot be null.");
        }
        if (expression.Body is MemberExpression member)
        {
            this.expressionBody = string.Join('.', member.ToString().Split('.').Skip(1));
        }

        this.argument = argument;
        this.expression = expression;
        this.isLessThan = (arg, val) => arg.CompareTo(val) >= 0;
    }

    public string Name => $"LessThanValidationRule<{typeof(T).Name}, {expressionBody ?? typeof(TValue).Name}, {typeof(TArgument).Name}>";

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
                    if (item is null || !isLessThan(this.argument, item))
                    {
                        context.AddFailure(this.Error);
                        break;
                    }
                }
            }
            else if (!isLessThan(this.argument, value))
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
    private TValue GetValue(T instance)
    {
        try
        {
            return expression.Compile().Invoke(instance);
        }
        catch (Exception exception)
        {
            return default(TValue);
        }
    }
}
