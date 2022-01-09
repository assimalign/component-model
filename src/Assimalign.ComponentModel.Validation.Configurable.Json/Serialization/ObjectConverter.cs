using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Assimalign.ComponentModel.Validation.Configurable.Serialization;

internal class ObjectConverter : JsonConverter<object>
{
    public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.TokenType switch
        {
            JsonTokenType.String when DateTime.TryParse(reader.GetString(), out var dateTime) => dateTime,
            JsonTokenType.String when TimeSpan.TryParse(reader.GetString(), out var timeSpan) => timeSpan,
            JsonTokenType.True => reader.GetBoolean(),
            JsonTokenType.False => reader.GetBoolean(),
            JsonTokenType.Number when reader.ValueSpan.ToString().Contains('.') => reader.GetDouble(),
            JsonTokenType.Number when reader.TryGetInt16(out var shortValue) => shortValue,
            JsonTokenType.Number when reader.TryGetInt32(out var intValue) => intValue,
            JsonTokenType.Number when reader.TryGetInt64(out var longValue) => longValue,
            JsonTokenType.Null => null,
            _ => null
        };                                                                                                                                                                                                                                      
    }

    public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
