using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Configurable.Internal;

internal class ValidationConfigConditionItem
{

    [JsonPropertyName("$conditionId")]
    public string Id { get; set; }

    [JsonPropertyName("$conditions")]
    public IEnumerable<ValidationConfigCondition> Conditions { get; set; }
}

