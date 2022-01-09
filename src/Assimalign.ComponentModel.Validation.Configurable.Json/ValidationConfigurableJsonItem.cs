using System;
using System.Diagnostics;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Assimalign.ComponentModel.Validation.Configurable;

using Assimalign.ComponentModel.Validation.Configurable.Serialization;


internal sealed class ValidationConfigurableJsonItem<T> : IValidationItem
{
    private string itemMember;
    private string itemExpressionBody;
    private Func<T, bool> itemConditionFunc;
    private Func<T, object> itemExpressionFunc;
    private Expression<Func<T, object>> itemExpression;
    private ValidationMode validationMode;




    [JsonPropertyName("$itemMember")]
    public string ItemMember
    {
        get => this.itemMember;
        set
        {
            var expression = GetMemberExpression(value);
            this.itemMember = value;
            this.itemExpression = expression;
            this.itemExpressionFunc = expression.Compile();
            this.itemExpressionBody = expression.ToString();
        }
    }

    [JsonPropertyName("$itemType")]
    [JsonConverter(typeof(EnumConverter<ItemType>))]
    public ItemType ItemType { get; set; }

    [JsonPropertyName("$itemRules")]
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
            if (this.itemConditionFunc is not null && !this.itemConditionFunc.Invoke(instance))
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
            return this.itemExpressionFunc(instance);
        }
        catch
        {
            return null;
        }
    }

    // Sets the Member Expression on Deserialization of JSON Validation Rules
    private Expression<Func<T, object>> GetMemberExpression(string member)
    {
        var parameterExpression = Expression.Parameter(typeof(T));
        var memberPaths = member.Split('.');
        var memberExpression = (Expression)parameterExpression;
        for (int i = 0; i < memberPaths.Length; i++)
        {
            memberExpression = Expression.Property(memberExpression, memberPaths[i]);
        }
        return Expression.Lambda<Func<T, object>>(memberExpression, parameterExpression);
    }


    #region Methods for Configuring Validation Item from Validation Profile
    internal void SetItemValidationMode(ValidationMode validationMode)
    {
        this.validationMode = validationMode;
    }
    internal void SetItemValidationCondition(Expression<Func<T, bool>> itemCondition)
    {
        this.itemConditionFunc = itemCondition.Compile();
    }
    internal void SetItemValidationErrorDefaults()
    {
        foreach(var rule in this.ItemRuleStack)
        {
            rule.Error ??= new ValidationConfigurableJsonError()
            {
                Message = "",
                Source = this.itemExpressionBody
            };
        }
    }
    internal void SetItemValidationRuleConversion()
    {

    }
    #endregion
}