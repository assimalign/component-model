using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Assimalign.ComponentModel.Validation.Configurable;

using Assimalign.ComponentModel.Validation.Configurable.Serialization;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class ValidationConfigurableJsonCondition<T>
    where T : class
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("$and")]
    public IEnumerable<ValidationConfigurableJsonConditionItem<T>> And { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("$or")]
    public IEnumerable<ValidationConfigurableJsonConditionItem<T>> Or { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("$member")]
    public string Member { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("$operator")]
    public OperatorType Operator { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("$value")]
    [JsonConverter(typeof(ObjectConverter))]
    public object Value { get; set; }
}