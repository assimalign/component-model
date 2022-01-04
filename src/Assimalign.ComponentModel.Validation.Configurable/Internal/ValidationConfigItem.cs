using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Assimalign.ComponentModel.Validation.Configurable.Internal;

using Assimalign.ComponentModel.Validation.Configurable.Internal.Serialization;

[JsonConverter(typeof(ItemRuleConverter))]
internal class ValidationConfigItem 
{
    [JsonPropertyName("$itemMember")]
    public string Member { get; set; }

    [JsonPropertyName("$itemType")]
    public ValidationConfigItemType ItemType { get; set; }

    [JsonPropertyName("$itemConditionId")]
    public string ConditionId { get; set; }

    [JsonPropertyName("$itemRules")]
    public IEnumerable<ValidationConfigRule> Rules { get; set; }
}