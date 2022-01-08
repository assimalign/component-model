using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Assimalign.ComponentModel.Validation.Configurable.Serialization;

internal class ValidationConfigurableJsonRuleConverter<T> : JsonConverter<IValidationRule>
{
    public override IValidationRule Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var rule = new ValidationConfigurableJsonRule<T>();

        while (reader.TokenType != JsonTokenType.EndObject)
        {
            reader.Read();

            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                var propertyName = reader.GetString();

                switch (propertyName.ToLower())
                {
                    case "$rule":
                        {
                            reader.Read();
                            var name = reader.GetString();
                            rule.Name = name;
                            break;
                        }
                    default:
                        {
                            reader.Read();
                            break;
                        }
                }
            }
        }


        return rule;
    }

    public override void Write(Utf8JsonWriter writer, IValidationRule value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}

