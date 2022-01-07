using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Assimalign.ComponentModel.Validation.Configurable;

using Assimalign.ComponentModel.Validation.Configurable.Serialization;


internal class ValidationConfigJsonItem<T> : IValidationItem
{
    private Func<T, object> itemExpressionCompiled;
    private Expression<Func<T, object>> itemExpression;
    private Func<T, bool> itemConditionCompiled;
    private Expression<Func<T, bool>> itemCondition;
    private IValidationRuleStack itemRuleStack;

    public ValidationConfigJsonItem()
    {
        this.itemRuleStack = new ValidationRuleStack();
    }



    [JsonPropertyName("$itemMember")]
    public string ItemMember { get; set; }

    [JsonPropertyName("$itemType")]
    [JsonConverter(typeof(ValidationConfigJsonItemTypeConverter))]
    public ValidationConfigJsonItemType ItemType { get; set; }

    [JsonPropertyName("$itemConditionId")]
    public string ItemConditionId { get; set; }

    [JsonPropertyName("$itemRules")]
    public IEnumerable<ValidationConfigJsonRule> ItemRules { get; set; }






    [JsonIgnore]
    public IValidationRuleStack ItemRuleStack => this.itemRuleStack;

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
    public Expression<Func<T, bool>> ItemCondition
    {
        get => this.itemCondition;
        set
        {
            this.itemCondition = value;
            this.itemConditionCompiled = value.Compile();
        }
    }

    public void Evaluate(IValidationContext context)
    {
        if (context.Instance is T instance)
        {
            if (ItemCondition is not null && !itemConditionCompiled.Invoke(instance))
            {
                return;
            }
            else
            {
                var value = this.itemExpressionCompiled.Invoke(instance);

                foreach (var rule in this.ItemRuleStack)
                {
                    if (rule.TryValidate(value, out var ruleContext))
                    {

                    }
                    else
                    {

                    }
                }
            }
        }
    }
}