using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Configurable.Internal;

internal class ValidationConfigJsonProfile<T> : IValidationProfile
    where T : class
{
    [JsonPropertyName("$description")]
    public string Description { get; set; }

    [JsonPropertyName("$validationMode")]
    public ValidationMode ValidationMode { get; set; }

    [JsonPropertyName("$validationConditions")]
    public IEnumerable<ValidationConfigJsonCondition> Conditions { get; set; }

    [JsonPropertyName("$validationItems")]
    public IEnumerable<ValidationConfigJsonItem> Items { get; set; }

    [JsonIgnore]
    public Type ValidationType { get; }

    [JsonIgnore]
    public IEnumerable<IValidationItem> ValidationItems => this.Items;

    public void Configure()
    {
        throw new NotImplementedException();
    }
}