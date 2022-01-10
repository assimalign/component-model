using System;
using System.Diagnostics;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Assimalign.ComponentModel.Validation.Configurable;

using Assimalign.ComponentModel.Validation.Internal.Extensions;
using Assimalign.ComponentModel.Validation.Properties;
using Assimalign.ComponentModel.Validation.Configurable.Serialization;


internal sealed class ValidationConfigurableJsonItem<T> : IValidationItem
{
    private Func<T, bool> condition;
    private Func<T, object> member;
    private Expression<Func<T, object>> expression;
    private ValidationMode validationMode;




    [JsonPropertyName(ValidationJsonProperty.ValidationItemMember)]
    public string ItemMember { get; set; }

    [JsonPropertyName(ValidationJsonProperty.ValidationItemType)]
    public ItemType ItemType { get; set; }

    [JsonPropertyName(ValidationJsonProperty.ValidationItemRules)]
    public IEnumerable<ValidationConfigurableJsonRule<T>> ItemRuleStack { get; set; }
    
    [JsonIgnore]
    IValidationRuleStack IValidationItem.ItemRuleStack
    {
        get
        {
            return new ValidationRuleStack(this.ItemRuleStack);
        }
    }




    public void Evaluate(IValidationContext context)
    {
        if (context.Instance is T instance)
        {
            // 1. Lets check if there is a condition and if so let's see.
            //    if this validation item can be evaluated
            if (this.condition is not null && !this.condition.Invoke(instance))
            {
                return;
            }
            if (this.ItemType == ItemType.Inline)
            {
                EvaluateInline(instance, context);
                return;
            }
            if (this.ItemType == ItemType.Recursive)
            {
                EvaluateRecursive(instance, context);
                return;
            }
        }
    }
    private void EvaluateInline(T instance, IValidationContext context)
    {
        var stopwatch = new Stopwatch();
        var value = this.GetMemberValue(instance);

        foreach (var rule in ItemRuleStack)
        {
            if (this.validationMode == ValidationMode.Stop && context.Errors.Any())
            {
                return;
            }

            stopwatch.Start();

            if (rule.TryValidate(value, out var validationContext))
            {
                foreach (var error in validationContext.Errors)
                {
                    context.AddFailure(error);
                }

                stopwatch.Stop();
                context.AddInvocation(new ValidationInvocation(rule.Name, true, stopwatch.ElapsedTicks));
            }
            else
            {
                stopwatch.Stop();
                context.AddInvocation(new ValidationInvocation(rule.Name, false, stopwatch.ElapsedTicks));
            }

            stopwatch.Reset();
        }
    }
    private void EvaluateRecursive(T instance, IValidationContext context)
    {
        var value = this.GetMemberValue(instance);

        if (value is IEnumerable enumerable)
        {
            foreach (var item in enumerable)
            {
                foreach (var rule in ItemRuleStack)
                {
                    if (rule.TryValidate(item, out var validationContext))
                    {
                        foreach (var error in validationContext.Errors)
                        {
                            context.AddFailure(error);
                        }
                    }
                }
            }
        }
    }


    // Let's safely get the member value encase of null reference exception
    private object GetMemberValue(T instance)
    {
        try
        {
            return this.member.Invoke(instance);
        }
        catch
        {
            return null;
        }
    }

    #region Methods for Configuring Validation Item from Validation Profile (AKA. Though efficient ABSOLUTE TRASH Implementation [Need to check this before I wreck this])

    // Sets the Member Expression on Deserialization of JSON Validation Rules
    internal Expression<Func<T, object>> GetMemberExpression()
    {
        if (this.expression is null)
        {
            var parameterExpression = Expression.Parameter(typeof(T));
            var memberPaths = this.ItemMember.Split('.');
            var memberExpression = (Expression)parameterExpression;
            for (int i = 0; i < memberPaths.Length; i++)
            {
                memberExpression = Expression.Property(memberExpression, memberPaths[i]);
            }
            return Expression.Lambda<Func<T, object>>(memberExpression, parameterExpression);
        }
        else
        {
            return this.expression;
        }
    }
    internal void ConfigureItemMember()
    {
        this.expression = GetMemberExpression();
        this.member = expression.Compile();
    }
    internal void ConfigureItemValidationMode(ValidationMode validationMode)
    {
        this.validationMode = validationMode;
    }
    internal void ConfigureItemCondition(Expression<Func<T, bool>> itemCondition)
    {
        this.condition = itemCondition.Compile();
    }
    internal void ConfigureErrorDefaults()
    {
        foreach(var rule in this.ItemRuleStack)
        {
            rule.Error ??= new ValidationConfigurableJsonError()
            {
                Message = "",
                Source = this.expression.ToString()
            };
        }
    }
    internal void ConfigureRuleValueTypeConversion()
    {
        var returnType = expression.Body.Type.IsEnumerableType(out var enumerableType) ? 
            enumerableType : 
            expression.Body.Type; 

        foreach(var rule in this.ItemRuleStack)
        {
            rule.SetRuleConversion(returnType);
        }
    }
    #endregion
}