using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Configurable;

internal class ValidationProfileConfiguration
{
    [JsonPropertyName("$description")]
    public string Description { get; set; }

    [JsonPropertyName("$validationMode")]
    public ValidationMode ValidationMode { get; set; }

    [JsonPropertyName("$validationConditions")]
    public IEnumerable<ValidationConditionConfiguration> Conditions { get; set; }

    [JsonPropertyName("$validationItems")]
    public IEnumerable<ValidationConfigurableJsonItem> ValidationItems { get; set; }
}