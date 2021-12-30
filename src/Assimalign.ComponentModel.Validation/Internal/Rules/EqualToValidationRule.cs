using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;


namespace Assimalign.ComponentModel.Validation.Internal.Rules;

using Assimalign.ComponentModel.Validation.Internal.Exceptions;
using Assimalign.ComponentModel.Validation.Internal.Extensions;

internal sealed class EqualToValidationRule<T, TValue, TArgument> : ValidationRuleBase<T,TValue, TArgument>
    where TArgument : notnull, IEquatable<TArgument>
{
    private readonly Type paramType;
    private readonly Type valueType;
    private readonly Type argumentType;
    private readonly TArgument argument;
    private readonly Func<object, bool> isEqualTo;
    private readonly Expression<Func<T, TValue>> expression;
    private readonly string expressionBody;

    public EqualToValidationRule(Expression<Func<T, TValue>> expression, TArgument argument) : base(expression, argument)
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

    public override string Name => $"EqualToValidationRule<{paramType.Name}, {expressionBody ?? valueType.Name}, {argumentType.Name}>";

    public IValidationContext Error { get; set; }


    public override void Evaluate(IValidationContext context)
    {
        if (context.Instance is T instance)
        {
            var value = this.GetValue(instance);

            if (value is null)
            {
                context.AddFailure(this.Error);
            }
            else if (this.Argument is not IEnumerable && value is IEnumerable enumerable)
            {
                foreach(var item in enumerable)
                {
                    if (this.Argument is IEquatable<TArgument> equatable)
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
                    else if (this.Argument is IEqualityComparer<TArgument> typedComaprer)
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
                    else if (this.Argument is IEqualityComparer comparer && !comparer.Equals(item))
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
            else if (this.Argument is IEquatable<TArgument> equatable)
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
}

