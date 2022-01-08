using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Assimalign.ComponentModel.Validation.Configurable.Serialization;

internal class ValidationConfigurableJsonConditionConverter<T> : JsonConverter<IValidationCondition>
{
    public override IValidationCondition Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, IValidationCondition value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}

