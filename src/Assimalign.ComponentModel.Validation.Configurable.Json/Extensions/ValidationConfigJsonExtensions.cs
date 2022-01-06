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

public static class ValidationConfigJsonExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="json"></param>
    /// <returns></returns>
    public static IValidationConfigBuilder ConfigureJson<T>(this IValidationConfigBuilder builder, string json)
        where T : class
    {
        var provider = ValidationConfigSource.Create(json)
            .Build(); ;

        return builder.Add(provider);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="stream"></param>
    /// <returns></returns>
    public static IValidationConfigBuilder ConfigureJson<T>(this IValidationConfigBuilder builder, Stream stream)
        where T : class
    {

        builder.Configure()


        return builder;
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

