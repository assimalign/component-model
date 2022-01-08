using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Assimalign.ComponentModel.Validation.Configurable.Serialization;

internal class ValidationConfigurableJsonItemConverter<T> : JsonConverter<IValidationItem>
{
    public override IValidationItem Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var item = new ValidationConfigurableJsonItem<T>();

        while (reader.TokenType != JsonTokenType.EndObject)
        {
            reader.Read();

            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                var propertyName = reader.GetString();

                switch (propertyName.ToLower())
                {
                    case "$itemmember":
                        {
                            reader.Read();
                            item.ItemMember = reader.GetString();
                            break;
                        }
                    case "$itemtype":
                        {
                            reader.Read();
                            var itemType = reader.GetString();
                            if (itemType.ToLower() == "inline")
                            {
                                item.ItemType = ItemType.Inline;
                            }
                            else if (itemType.ToLower() == "recursive")
                            {
                                item.ItemType = ItemType.Recursive;
                            }
                            else
                            {
                                throw new JsonException("");
                            }
                            break;
                        }
                    case "$itemrules":
                        {
                            var converter = options.GetConverter(typeof(IValidationRule)) as JsonConverter<IValidationRule>;

                            if (converter is not null)
                            {
                                reader.Read();

                                while (reader.TokenType != JsonTokenType.EndArray)
                                {
                                    reader.Read();

                                    if (reader.TokenType == JsonTokenType.StartObject)
                                    {
                                        var rule = converter.Read(ref reader, typeof(IValidationRule), options);
                                        item.ItemRuleStack.Push(rule);
                                    }
                                }
                            }
                            else
                            {
                                throw new JsonException($"Missing '{nameof(ValidationConfigurableJsonRuleConverter<T>)}' for 'IValidationRule'");
                            }
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

        return item;
    }

    public override void Write(Utf8JsonWriter writer, IValidationItem value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}

