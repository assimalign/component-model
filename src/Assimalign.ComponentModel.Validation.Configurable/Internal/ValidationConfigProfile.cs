using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Configurable.Internal;

internal class ValidationConfigProfile
{
    [JsonPropertyName("$description")]
    public string Description { get; set; }

    [JsonPropertyName("$validationMode")]
    public ValidationMode ValidationMode { get; set; }

    [JsonPropertyName("$validationConditions")]
    public IEnumerable<ValidationConfigCondition> Conditions { get; set; }

    [JsonPropertyName("$validationItems")]
    public IEnumerable<ValidationConfigItem> Items { get; set; }

}