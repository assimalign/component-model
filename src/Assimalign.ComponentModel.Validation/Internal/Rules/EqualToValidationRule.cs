using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace Assimalign.ComponentModel.Validation.Internal.Rules;

using Assimalign.ComponentModel.Validation.Internal.Exceptions;
using Assimalign.ComponentModel.Validation.Internal.Extensions;

internal sealed class EqualToValidationRule<T, TValue, TArgument> : IValidationRule
{
    private readonly Type paramType;
    private readonly Type valueType;
    private readonly Type argumentType;
    private readonly TArgument argument;
    private readonly Func<object, bool> isEqualTo;
    private readonly Expression<Func<T, TValue>> expression;
    private readonly string expressionBody;

    public EqualToValidationRule(Expression<Func<T, TValue>> expression, TArgument argument)
    {
        if (expression is null)
        {
            throw new ArgumentNullException(
                paramName: nameof(expression),
                message: $"The following expression where the 'EqualTo()' rule is defined cannot be null.");
        }
        if (argument is null)
        {
            throw new ArgumentNullException(nameof(argument));
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

    public string Name => $"EqualToValidationRule<{paramType.Name}, {expressionBody ?? valueType.Name}, {argumentType.Name}>";

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
            else if (this.argument is not IEnumerable && value is IEnumerable enumerable)
            {
                foreach(var item in enumerable)
                {
                    if (argument is IEquatable<TArgument> equatable)
                    {
                        if (item is TArgument equatableValue && !equatable.Equals(equatableValue))
                        {
                            context.AddFailure(this.Error);
                            break;
                        }
                        else if (!equatable.Equals(item))
                        {
                            context.AddFailure(this.Error);
                            break;
                        }
                    }
                    else if (argument is IEqualityComparer<TArgument> typedComaprer)
                    {
                        if (item is TArgument equatableValue && !typedComaprer.Equals(equatableValue))
                        {
                            context.AddFailure(this.Error);
                            break;
                        }
                        else if (!typedComaprer.Equals(item))
                        {
                            context.AddFailure(this.Error);
                            break;
                        }
                    }
                    else if (argument is IEqualityComparer comparer && !comparer.Equals(item))
                    {
                        context.AddFailure(this.Error);
                        break;
                    }
                    else if (!this.isEqualTo(item))
                    {
                        context.AddFailure(this.Error);
                        break;
                    }
                }
            }
            else if (argument is IEquatable<TArgument> equatable)
            {
                if (value is TArgument equatableValue && !equatable.Equals(equatableValue))
                {
                    context.AddFailure(this.Error);
                }
                else if (!equatable.Equals(value)) 
                {
                    context.AddFailure(this.Error);
                }
            }
            else if (argument is IEqualityComparer<TArgument> typedComaprer)
            {
                if (value is TArgument equatableValue && !typedComaprer.Equals(equatableValue))
                {
                    context.AddFailure(this.Error);
                }
                else if (!typedComaprer.Equals(value))
                {
                    context.AddFailure(this.Error);
                }
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

            if (this.valueType is null || this.valueType == this.argumentType)
            {
                return null; // Let's exit if value is null or if the argument type is the same as the value type
            }
            if (this.valueType.IsEnumerableType(out var enumerableType))
            {
                if (enumerableType.IsNullable(out var enumNullableType))
                {
                    
                }
                else
                {
                    

                }
            }
            else if (this.valueType.IsNullable(out var nullableType))
            {
                if (nullableType == this.argumentType)
                {
                    return value;
                }
                else
                {

                }
            }
            else if (nullableType != argumentType)
            {

            }
            else
            {

            }

            // Let's check that the we are not comparing 2 enumerables
            if (this.argument is not IEnumerable && value is IEnumerable enumerable)
            {
                var items = new List<object>();

                foreach (var item in enumerable)
                {
                    if (item is not TArgument && item is IConvertible convertable) 
                    {
                        items.Add(convertable.ToType(typeof(TArgument), default));
                    }
                    else
                    {
                        items.Add(item);
                    }
                }

                return items;
            }
            else if (value is not TArgument && value is IConvertible convertable) // Need to convert when trying to compare
            {
                return convertable.ToType(typeof(TArgument), default);
            }

            return value;
        }
        catch (InvalidCastException exception)
        {
            throw new ValidationInvalidCastException(
                message: $"Unable to equate type '{valueType.Name}' against type '{argumentType.Name}'. '{this.expressionBody}' must be able to convert to {this.argumentType}",
                inner: exception,
                source: $"RuleFor(p => p.{this.expressionBody}).EqualTo({this.argument})");
        }
        catch (Exception exception) when (exception is not ValidationInvalidCastException)
        {
            return null;
        }
    }
}

