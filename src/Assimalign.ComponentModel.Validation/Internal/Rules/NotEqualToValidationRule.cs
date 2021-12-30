using System;
using System.Linq;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Validation.Internal.Rules;

using Assimalign.ComponentModel.Validation.Internal.Exceptions;
using Assimalign.ComponentModel.Validation.Internal.Extensions;

internal sealed class NotEqualToValidationRule<T, TValue, TArgument> : IValidationRule
{
    private readonly Type paramType;
    private readonly Type valueType;
    private readonly Type argumentType;
    private readonly TArgument argument;
    private readonly Func<object, bool> isEqualTo;
    private readonly Expression<Func<T, TValue>> expression;
    private readonly string expressionBody;

    public NotEqualToValidationRule(Expression<Func<T, TValue>> expression, TArgument argument)
    {
        if (expression is null)
        {
            throw new ArgumentNullException(
                paramName: nameof(expression),
                message: $"The following expression where the 'NotEqual()' rule is defined cannot be null.");
        }
        if (argument is null)
        {
            throw new ArgumentNullException(nameof(TArgument));
        }
        if (expression.Body is MemberExpression member)
        {
            this.expressionBody = string.Join('.', member.ToString().Split('.').Skip(1));
        }
        this.paramType = typeof(T);
        this.valueType = typeof(TValue);
        this.argumentType = typeof(TArgument);
        this.argument = argument;
        this.expression = expression;
        this.isEqualTo = x => x.Equals(argument);
    }

    public string Name => $"NotEqualValidationRule<{typeof(T).Name}, {expressionBody ?? typeof(TValue).Name}>";

    public IValidationContext Error { get; set; }

    public ValidationRuleType RuleType { get; set; }

    public void Evaluate(IValidationContext context)
    {
        if (context.Instance is T instance)
        {
            var value = this.GetValue(instance);

            if (this.isEqualTo(value))
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
            return this.expression.Compile().Invoke(instance);
        }
        catch (InvalidCastException exception)
        {
            throw new ValidationInvalidCastException(
                message: $"Unable to equate type '{valueType.Name}' against type '{argumentType.Name}'",
                inner: exception,
                source: $"RuleFor(p => p.{this.expressionBody}).NotEqualTo({this.argument})");
        }
        catch (Exception exception) when (exception is not ValidationInvalidCastException)
        {
            return null;
        }
    }
}

