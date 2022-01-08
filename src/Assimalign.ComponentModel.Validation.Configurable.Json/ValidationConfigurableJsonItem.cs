using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Assimalign.ComponentModel.Validation.Configurable;

using Assimalign.ComponentModel.Validation;
using Assimalign.ComponentModel.Validation.Configurable.Serialization;


internal class ValidationConfigurableJsonItem<T> : IValidationItem
{
    [JsonConstructor]
    public ValidationConfigurableJsonItem()
    {
        this.ItemRuleStack = new ValidationRuleStack();
    }

    [JsonPropertyName("$itemMember")]
    public string ItemMember { get; set; }

    [JsonPropertyName("$itemType")]
    public ItemType ItemType { get; set; }

    [JsonPropertyName("$itemRules")]
    public IValidationRuleStack ItemRuleStack { get; set; }



    [JsonIgnore]
    public Expression<Func<T, object>> ItemExpression { get; set; }

    public void Evaluate(IValidationContext context)
    {
        throw new NotImplementedException();
    }
}