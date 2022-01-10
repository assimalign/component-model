using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Assimalign.ComponentModel.Validation.Configurable;

using Assimalign.ComponentModel.Validation;
using Assimalign.ComponentModel.Validation.Configurable.Serialization;

public static class ValidationExtensions
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
            options.Converters.Add(new EnumConverter<ItemType>());
            return JsonSerializer.Deserialize<ValidationConfigurableJsonProfile<T>>(json, options);
        }));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="stream"></param>
    /// <returns></returns>
    public static IValidationConfigurableBuilder AddJsonSource<T>(this IValidationConfigurableBuilder builder, Stream stream, JsonSerializerOptions options = null)
        where T : class
    {
        return builder.Add(new ValidationConfigurableJsonSource<T>(() =>
        {
            options ??= GetDefaultJsonSerializationOptions();
            options.Converters.Add(new EnumConverter<ItemType>());
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

    private static void WriteObject<T>(ValidationProfile<T> profile)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            using (Utf8JsonWriter writer = new Utf8JsonWriter(stream))
            {
                // Begin writing JSON Validation Schema
                writer.WriteStartObject();


                // Begin Writing Validation Items
                writer.WritePropertyName("$validationItems");
                writer.WriteStartArray();

                foreach (var item in profile.ValidationItems)
                {


                }

                writer.WriteEndArray();

                // End writing JSON Validation Schema
                writer.WriteEndObject();
            }
        }
    }
}

