using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Configurable;

internal class ValidationConditionItemConfiguration
{

    [JsonPropertyName("$conditionId")]
    public string Id { get; set; }

    [JsonPropertyName("$conditions")]
    public IEnumerable<ValidationConditionConfiguration> Conditions { get; set; }
}

