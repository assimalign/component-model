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

public static class ValidationConfigJsonExtensions
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
            options ??= new JsonSerializerOptions();
            //options.Converters.Add(new ValidationConfigurableJsonRuleConverter<T>());
            //options.Converters.Add(new ValidationConfigurableJsonItemConverter<T>());
            //options.Converters.Add(new ValidationConfigurableJsonConditionConverter<T>());
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
            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();

                return JsonSerializer.Deserialize<ValidationConfigurableJsonProfile<T>>(json, options);
            }
        }));
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

