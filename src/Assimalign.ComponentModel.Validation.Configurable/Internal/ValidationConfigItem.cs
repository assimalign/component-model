using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Assimalign.ComponentModel.Validation.Configurable.Internal;

internal abstract class ValidationConfigItem 
{
    public ValidationConfigItem()
    {
        this.ItemRuleStack = new ValidationRuleStack();
    }

    [JsonPropertyName("$itemMember")]
    public string Member { get; set; }

    [JsonPropertyName("$itemRules")]
    public IEnumerable<ValidationConfigItemRule> Rules { get; set; }

    [JsonPropertyName("$itemType")]
    public ValidationConfigItemType ItemType { get; set; }

    [JsonPropertyName("$itemCondition")]
    public ValidationConfigCondition Condition { get; set; }

    [JsonIgnore]
    public IValidationRuleStack ItemRuleStack { get; }

    public void Evaluate(IValidationContext context)
    {
        throw new NotImplementedException();
    }
}

//internal abstract class ValidationConfig

internal class ValidationConfigRuleEqualTo : IValidationRule
{

    [JsonPropertyName("$value")]
    public object Value { get; set; }

    public string Name => throw new NotImplementedException();

    public bool TryValidate(object value, out IValidationContext context)
    {
        throw new NotImplementedException();
    }
}