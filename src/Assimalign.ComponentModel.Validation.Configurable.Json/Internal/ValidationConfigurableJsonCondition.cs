using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Assimalign.ComponentModel.Validation.Configurable;

using Assimalign.ComponentModel.Validation.Configurable.Serialization;
using Assimalign.ComponentModel.Validation.Configurable.Internal.Extensions;

internal sealed class ValidationConfigurableJsonCondition<T>
{
    [JsonPropertyName("$and")]
    public IEnumerable<ValidationConfigurableJsonConditionItem<T>> And { get; set; }

    [JsonPropertyName("$or")]
    public IEnumerable<ValidationConfigurableJsonConditionItem<T>> Or { get; set; }

    [JsonPropertyName("$member")]
    public string Member { get; set; }

    [JsonPropertyName("$operator")]
    [JsonConverter(typeof(EnumConverter<OperatorType>))]
    public OperatorType Operator { get; set; }

    [JsonPropertyName("$value")]
    [JsonConverter(typeof(ObjectConverter))]
    public object Value { get; set; }
}