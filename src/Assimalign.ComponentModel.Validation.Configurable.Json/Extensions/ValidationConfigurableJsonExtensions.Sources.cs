using System;
using System.IO;
using System.Text.Json;

namespace Assimalign.ComponentModel.Validation.Configurable;

using Assimalign.ComponentModel.Validation;
using Assimalign.ComponentModel.Validation.Configurable.Serialization;

public static class ValidationConfigurableJsonExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="json"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static IValidationConfigurableBuilder AddJsonSource<T>(this IValidationConfigurableBuilder builder, string json, JsonSerializerOptions options = null)
        where T : class
    {
        return builder.Add(new ValidationConfigurableJsonSource<T>(() =>
        {
            options ??= GetDefaultJsonSerializationOptions();
            options.Converters.Add(new EnumConverter<OperatorType>());
            options.Converters.Add(new EnumConverter<ValidationConfigurableItemType>());
            options.Converters.Add(new EnumConverter<ValidationMode>());
            return JsonSerializer.Deserialize<ValidationConfigurableJsonProfile<T>>(json, options);
        }));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="stream"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static IValidationConfigurableBuilder AddJsonSource<T>(this IValidationConfigurableBuilder builder, Stream stream, JsonSerializerOptions options = null)
        where T : class
    {
        return builder.Add(new ValidationConfigurableJsonSource<T>(() =>
        {
            options ??= GetDefaultJsonSerializationOptions();
            options.Converters.Add(new EnumConverter<OperatorType>());
            options.Converters.Add(new EnumConverter<ValidationConfigurableItemType>());
            options.Converters.Add(new EnumConverter<ValidationMode>());
            return JsonSerializer.DeserializeAsync<ValidationConfigurableJsonProfile<T>>(stream, options)
                .GetAwaiter()
                .GetResult();
        }));
    }

    private static JsonSerializerOptions GetDefaultJsonSerializationOptions()
    {
        return new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            AllowTrailingCommas = true,
            ReadCommentHandling = JsonCommentHandling.Skip
        };
    }
}