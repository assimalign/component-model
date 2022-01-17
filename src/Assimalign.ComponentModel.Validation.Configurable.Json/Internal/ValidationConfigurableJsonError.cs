using System;
using System.Text.Json.Serialization;

namespace Assimalign.ComponentModel.Validation.Configurable;

internal sealed class ValidationConfigurableJsonError : IValidationError
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("$code")]
    public string Code { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("$message")]
    public string Message { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("$source")]
    public string Source { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $"Error {Code}: {Message} {Environment.NewLine} └─> Source: {Source}";
    }
}
