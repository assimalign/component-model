using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Configurable.Internal.Serialization;

using Assimalign.ComponentModel.Validation.Configurable.Internal;

internal class ItemRuleConverter : JsonConverter<ValidationConfigJsonRule>
{
    public override ValidationConfigJsonRule Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        while (reader.TokenType != JsonTokenType.EndObject)
        {
            reader.Read();

            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                return reader.GetString() switch
                {
                    ValidationConfigRuleName.EqualTo => GetEqualToRule(ref reader),
                    ValidationConfigRuleName.NotEqualTo => GetNotEqualToRule(ref reader),

                    _ => throw new Exception()
                };
            }
        }

        throw new Exception();
    }

    public override void Write(Utf8JsonWriter writer, ValidationConfigJsonRule value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }


    private ValidationConfigJsonRule GetNotEqualToRule(ref Utf8JsonReader reader)
    {
        while (reader.TokenType != JsonTokenType.EndObject)
        {
            reader.Read();

            if (reader)
        }
    }

    private ValidationConfigJsonRule GetEqualToRule(ref Utf8JsonReader reader)
    {

    }
}

