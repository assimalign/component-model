using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Internal;

using Assimalign.ComponentModel.Validation.Internal.Exceptions;

internal abstract class ValidationRuleBase<T, TValue> : IValidationRule
{
    public ValidationRuleBase(Expression<Func<T, TValue>> expression)
    {
        if (expression.Body is MemberExpression member)
        {
            this.ExpressionBody = string.Join('.', member.ToString().Split('.').Skip(1));
        }

        this.ParamType = typeof(T);
        this.ValueType = typeof(TValue);
        this.Expression = expression;
    }

    protected Type ParamType { get; }
    protected Type ValueType { get; }
    protected Expression<Func<T, TValue>> Expression { get; }
    protected string ExpressionBody { get; }
    public IValidationError Error { get; set; }
    public ValidationRuleType RuleType { get; set; }
    public abstract string Name { get; }
    public abstract void Evaluate(IValidationContext context);
}



internal abstract class ValidationRuleBase<T, TValue, TArgument> : ValidationRuleBase<T, TValue>
{
    public ValidationRuleBase(Expression<Func<T, TValue>> expression) : base(expression)
    {
        this.ArgumentType = typeof(TArgument);
    }


    protected Type ArgumentType { get; }
    protected string ValidationRuleSource { get; set; }


    public virtual object GetValue(T instance)
    {
        try
        {
            var value = this.Expression.Compile().Invoke(instance);

            if (value is null || this.ValueType == this.ArgumentType)
            {
                return value; // Let's exit if value is null or if the argument type is the same as the value type
            }
            if (this.RuleType == ValidationRuleType.RecursiveRule)
            {
                var enumerableConversions = new List<object>();
                var enumerable = value as IEnumerable;

                if (enumerable is null)
                {
                    throw new ValidationInternalException(
                        message: $"Unable to convert expression value: {this.ExpressionBody} to IEnumerable.",
                        inner: new InvalidCastException($"Type {this.ValueType.Name} is not valid as {this.ArgumentType.Name}"),
                        source: this.ExpressionBody); // This should never happen, but for safety let's add it for now.
                }

                foreach (var enumValue in enumerable)
                {
                    if (enumValue is not TArgument && enumValue is IConvertible convertible)
                    {
                        enumerableConversions.Add(convertible.ToType(this.ArgumentType, default));
                    }
                    else
                    {
                        enumerableConversions.Add(enumValue);
                    }
                }

                return enumerableConversions;
            }
            if (this.RuleType == ValidationRuleType.SingularRule)
            {
                if (value is not TArgument && value is IConvertible convertible)
                {
                    return convertible.ToType(this.ArgumentType, default);
                }
                else
                {
                    return value;
                }
            }
            else
            {
                return null; // TODO: Decide whether to throw an exception
            }
        }
        catch (InvalidCastException exception)
        {
            throw new ValidationInvalidCastException(
                message: $"Unable to equate type '{this.ValueType.Name}' against type '{this.ArgumentType.Name}'. '{this.ExpressionBody}' must be able to convert to {this.ArgumentType}",
                inner: exception,
                source: this.ValidationRuleSource);
        }
        catch (Exception exception) when (exception is not ValidationInvalidCastException)
        {
            return null;
        }
    }
}

