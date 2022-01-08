using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Configurable.Serialization;


internal class ValidationConfigurableOperatorTypeConverter : JsonConverter<OperatorType>
{
    public override OperatorType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var enumValue = reader.GetString();

            if (Enum.TryParse(typeof(OperatorType), enumValue, out var enumType))
            {
                return (OperatorType)enumType;
            }
        }

        throw new JsonException();
    }

    public override void Write(Utf8JsonWriter writer, OperatorType value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
