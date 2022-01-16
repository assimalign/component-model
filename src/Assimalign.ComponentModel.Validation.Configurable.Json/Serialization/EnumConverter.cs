using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Assimalign.ComponentModel.Validation.Configurable.Serialization;


internal class EnumConverter<TEnum> : JsonConverter<TEnum>
    where TEnum : Enum
{
    public override TEnum? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var value = reader.GetString();

            if (Enum.TryParse(typeof(TEnum),value, true, out var enumValue))
            {
                return (TEnum)enumValue;
            }
            else
            {
                return default(TEnum);
            }
        }
        else
        {
            throw new JsonException("");
        }
    }

    public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}

