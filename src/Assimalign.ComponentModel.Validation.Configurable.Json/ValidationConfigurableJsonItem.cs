using System;
using Assimalign.ComponentModel.Validation.Configurable.Serialization;
using System.Collections;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Assimalign.ComponentModel.Validation.Configurable;

using Assimalign.ComponentModel.Validation;


internal class ValidationConfigurableJsonItem<T> : IValidationItem
{
    private string itemMember;
    private Func<T, object> itemExpressionCompiled;
    private Expression<Func<T, object>> itemExpression;


    [JsonPropertyName("$itemMember")]
    public string ItemMember
    {
        get => this.itemMember;
        set
        {
            this.ItemExpression = GetMemberExpression(value);
            this.itemMember = value;
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

    [JsonIgnore]
    public Expression<Func<T, object>> ItemExpression
    {
        get => this.itemExpression;
        set
        {
            this.itemExpression = value;
            this.itemExpressionCompiled = value.Compile();
        }
    }

    [JsonIgnore]
    public Expression<Func<T, bool>> ItemCondition { get; set; }


    public void Evaluate(IValidationContext context)
    {
        if (context.Instance is T instance)
        {
            var value = this.GetValue(instance);

            if (this.ItemType == ItemType.Inline)
            {
                foreach (var rule in ItemRuleStack)
                {
                    if (rule.TryValidate(value, out var validationContext))
                    {
                        foreach (var error in validationContext.Errors)
                        {
                            context.AddFailure(error);
                        }
                    }
                }
            }
            else if (this.ItemType == ItemType.Recursive)
            {
                if (value is IEnumerable enumerable)
                {
                    foreach(var item in enumerable)
                    {
                        foreach (var rule in ItemRuleStack)
                        {
                            if (rule.TryValidate(item, out var validationContext))
                            {
                                foreach(var error in validationContext.Errors)
                                {
                                    context.AddFailure(error);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private object GetValue(T instance)
    {
        try { return this.itemExpressionCompiled(instance); }
        catch { return null; }
    }


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
}