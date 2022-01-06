using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Configurable.Internal;

internal class ValidationConfigJsonConditionItem
{

    [JsonPropertyName("$conditionId")]
    public string Id { get; set; }

    [JsonPropertyName("$conditions")]
    public IEnumerable<ValidationConfigJsonCondition> Conditions { get; set; }
}

