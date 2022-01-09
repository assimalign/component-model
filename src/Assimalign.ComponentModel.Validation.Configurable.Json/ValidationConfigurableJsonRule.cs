using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Assimalign.ComponentModel.Validation.Configurable;

using Assimalign.ComponentModel.Validation.Configurable.Serialization;


internal class ValidationConfigurableJsonRule<T> : IValidationRule
{
    private ValidationConfigurableJsonRuleName name;

    [JsonPropertyName("$rule")]
    public string Name
    {
        get => this.Name.ToString();
        set
        {
            if (Enum.TryParse(typeof(ValidationConfigurableJsonRuleName), value, true, out var enumValue))
            {
                name = (ValidationConfigurableJsonRuleName)enumValue;
            }
            else
            {
                throw new Exception();
            }
        }
    }
 
    [JsonPropertyName("$lower")]
    [JsonConverter(typeof(ObjectConverter))]
    public object Lower { get; set; }

    [JsonPropertyName("$upper")]
    [JsonConverter(typeof(ObjectConverter))]
    public object Upper { get; set; }

    [JsonPropertyName("$value")]
    [JsonConverter(typeof(ObjectConverter))]
    public object Value { get; set; }

    [JsonPropertyName("$error")]
    public ValidationConfigurableJsonError Error { get; set; }

    public bool TryValidate(object value, out IValidationContext context)
    {
        return this.name switch
        {
            ValidationConfigurableJsonRuleName.EqualTo => TryValidateEqualTo(value, out context),
            ValidationConfigurableJsonRuleName.NotEqualTo => TryValidateNotEqualTo(value, out context),
            ValidationConfigurableJsonRuleName.Empty => TryValidateEmpty(value, out context),
            ValidationConfigurableJsonRuleName.NotEmpty => TryValidateNotEmpty(value, out context),
            ValidationConfigurableJsonRuleName.Between => TryValidateBetween(value, out context),
            ValidationConfigurableJsonRuleName.BetweenOrEqualTo => TryValidateBetweenOrEqualTo(value, out context),
            ValidationConfigurableJsonRuleName.GreaterThan => TryValidateGreaterThan(value, out context),
            ValidationConfigurableJsonRuleName.GreaterThanOrEqualTo => TryValidateGreaterThanOrEqualTo(value, out context),
            ValidationConfigurableJsonRuleName.LessThan => TryValidateLessThan(value, out context),
            ValidationConfigurableJsonRuleName.LessThanOrEqualTo => TryValidateLessThanOrEqualTo(value, out context),
            ValidationConfigurableJsonRuleName.EmailAddress => TryValidateEmailAddress(value, out context),
            ValidationConfigurableJsonRuleName.Null => TryValidateNull(value, out context),
            ValidationConfigurableJsonRuleName.NotNull => TryValidateNotNull(value, out context),
            ValidationConfigurableJsonRuleName.Length => TryValidateLength(value, out context),
            ValidationConfigurableJsonRuleName.LengthBetween => TryValidateLengthBetween(value, out context),
            ValidationConfigurableJsonRuleName.LengthMax => TryValidateLengthMax(value, out context),
            ValidationConfigurableJsonRuleName.LengthMin => TryValidateLengthMin(value, out context),
            ValidationConfigurableJsonRuleName.Child => TryValidateChild(value, out context),
            ValidationConfigurableJsonRuleName.Matches => TryValidateMatches(value, out context),
            _ => throw new Exception()
        };
    }

    private bool TryValidateEqualTo(object value, out IValidationContext context)
    {
        this.Error ??= new ValidationConfigurableJsonError()
        {

        };

        if (value is null && this.Value is null)
        {
            context = new ValidationContext<object>(value);
            return true;
        }
        else if (!value.Equals(this.Value))
        {
            context = new ValidationContext<object>(value);
            context.AddFailure(this.Error);
            return true;
        }



        context = null;
        return false;
    }

    private bool TryValidateNotEqualTo(object value, out IValidationContext context)
    {
        throw new NotImplementedException();
    }

    private bool TryValidateEmpty(object value, out IValidationContext context)
    {
        throw new NotImplementedException();
    }

    private bool TryValidateNotEmpty(object value, out IValidationContext context)
    {
        Error ??= new ValidationConfigurableJsonError()
        {

        };

        if (value is null)
        {
            context = new ValidationContext<object>(value);
            context.AddFailure(this.Error);
            return true;

        }

        switch (value)
        {
            case string stringValue when string.IsNullOrWhiteSpace(stringValue):
                {
                    context = new ValidationContext<object>(value);
                    context.AddFailure(this.Error);
                    return true;
                }
            case ICollection collection when collection.Count > 0:
                {
                    context = new ValidationContext<object>(value);
                    context.AddFailure(this.Error);
                    return true;
                }
            case Array array when array.Length > 0:
                {
                    context = new ValidationContext<object>(value);
                    context.AddFailure(this.Error);
                    return true;
                }
            case IEnumerable enumerable when enumerable.Cast<object>().Any():
                {
                    context = new ValidationContext<object>(value);
                    context.AddFailure(this.Error);
                    return true;
                }
            default:
                {
                    context = null;
                    return false;
                }
            
        };
    }

    private bool TryValidateBetween(object value, out IValidationContext context)
    {
        throw new NotImplementedException();
    }
    private bool TryValidateBetweenOrEqualTo(object value, out IValidationContext context)
    {
        throw new NotImplementedException();
    }

    private bool TryValidateGreaterThan(object value, out IValidationContext context)
    {
        throw new NotImplementedException();
    }
    private bool TryValidateGreaterThanOrEqualTo(object value, out IValidationContext context)
    {
        throw new NotImplementedException();
    }
    private bool TryValidateLessThan(object value, out IValidationContext context)
    {
        throw new NotImplementedException();
    }
    private bool TryValidateLessThanOrEqualTo(object value, out IValidationContext context)
    {
        throw new NotImplementedException();
    }
    private bool TryValidateEmailAddress(object value, out IValidationContext context)
    {
        throw new NotImplementedException();
    }
    private bool TryValidateNull(object value, out IValidationContext context)
    {
        throw new NotImplementedException();
    }
    private bool TryValidateNotNull(object value, out IValidationContext context)
    {
        throw new NotImplementedException();
    }
    private bool TryValidateLength(object value, out IValidationContext context)
    {
        throw new NotImplementedException();
    }
    private bool TryValidateLengthBetween(object value, out IValidationContext context)
    {
        throw new NotImplementedException();
    }
    private bool TryValidateLengthMax(object value, out IValidationContext context)
    {
        throw new NotImplementedException();
    }
    private bool TryValidateLengthMin(object value, out IValidationContext context)
    {
        throw new NotImplementedException();
    }
    private bool TryValidateChild(object value, out IValidationContext context)
    {
        throw new NotImplementedException();
    }
    private bool TryValidateMatches(object value, out IValidationContext context)
    {
        throw new NotImplementedException();
    }
}
