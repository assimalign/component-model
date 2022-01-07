using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Configurable.Serialization;


internal class ValidationConfigJsonItemTypeConverter : JsonConverter<ValidationConfigJsonItemType>
{
    public override ValidationConfigJsonItemType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var enumValue = reader.GetString();

            if (Enum.TryParse(typeof(ValidationConfigJsonItemType), enumValue, out var enumType))
            {
                return (ValidationConfigJsonItemType)enumType;
            }
        }

        throw new JsonException();
    }

    public override void Write(Utf8JsonWriter writer, ValidationConfigJsonItemType value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}

