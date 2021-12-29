using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Validation.Internal.Rules;


internal sealed class EmptyValidationRule<T, TValue> : ValidationRuleBase<T, TValue>
    where TValue : IEnumerable
{

    public EmptyValidationRule(Expression<Func<T, TValue>> expression) : base (expression)
    {
        if (expression is null)
        {
            throw new ArgumentNullException(
                paramName: nameof(expression),
                message: $"The following expression where the 'Empty()' rule is defined cannot be null.");
        }
    }

    public override string Name => $"EmptyValidationRule<{this.ParamType.Name}, {this.ExpressionBody ?? this.ValueType.Name}>";

    public override void Evaluate(IValidationContext context)
    {
        if (context.Instance is T instance)
        {
            var value = this.GetValue(instance);

            if (value is null)
            {
                context.AddSuccess(this);
                return;
            }
            if (this.RuleType == ValidationRuleType.RecursiveRule)
            {
                if (value is IEnumerable enumerable)
                {
                    foreach(var enumValue in enumerable)
                    {
                        if (enumValue is not null && enumValue is IEnumerable enumChildValue)
                        {
                            if (!IsEmpty(enumChildValue))
                            {
                                context.AddFailure(this.Error);
                                return;
                            }
                        }
                        else
                        {
                            throw new Exception("");
                        }
                    }
                }
            }
            else if (this.RuleType == ValidationRuleType.SingularRule && !IsEmpty(value))
            {
                context.AddFailure(this.Error);
            }
               
            else
            {
                context.AddSuccess(this);
            }
        }
    }

    private bool IsEmpty(object member)
    {
        return member switch
        {
            null => true,
            string      stringValue when string.IsNullOrWhiteSpace(stringValue) => true, // May not need this since string is IEnumerable
            ICollection collection  when collection.Count == 0 => true,
            Array       array       when array.Length == 0 => true,
            IEnumerable enumerable  when !enumerable.Cast<object>().Any() => true,
            _ => false
        };
    }


    private object GetValue(T instance)
    {
        try
        {
            return expression.Compile().Invoke(instance);
        }
        catch
        {
            return default(TValue);
        }
    }
}
