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


public sealed class ValidationConfigurableJsonRule<T> : IValidationRule
{
    private RuleType ruleType;

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("$rule")]
    public string Name { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("$error")]
    public ValidationConfigurableJsonError Error { get; set; }

    /// <summary>
    /// This will store our parameter values used to run the validation rules.
    /// We will need to do some conversion either at method call 'Configure()' or at executions 
    /// time (Let's avoid executions time if we can, RIGHT! By order of the Peaky Blinders)
    /// </summary>
    [JsonExtensionData] 
    public IDictionary<string, object> Parameters { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public bool TryValidate(object value, out IValidationContext context)
    {
        return this.ruleType switch
        {
            RuleType.EqualTo => TryValidateEqualTo(value, out context),
            RuleType.NotEqualTo => TryValidateNotEqualTo(value, out context),
            RuleType.Empty => TryValidateEmpty(value, out context),
            RuleType.NotEmpty => TryValidateNotEmpty(value, out context),
            RuleType.Between => TryValidateBetween(value, out context),
            RuleType.BetweenOrEqualTo => TryValidateBetweenOrEqualTo(value, out context),
            RuleType.GreaterThan => TryValidateGreaterThan(value, out context),
            RuleType.GreaterThanOrEqualTo => TryValidateGreaterThanOrEqualTo(value, out context),
            RuleType.LessThan => TryValidateLessThan(value, out context),
            RuleType.LessThanOrEqualTo => TryValidateLessThanOrEqualTo(value, out context),
            RuleType.EmailAddress => TryValidateEmailAddress(value, out context),
            RuleType.Null => TryValidateNull(value, out context),
            RuleType.NotNull => TryValidateNotNull(value, out context),
            RuleType.Length => TryValidateLength(value, out context),
            RuleType.LengthBetween => TryValidateLengthBetween(value, out context),
            RuleType.LengthMax => TryValidateLengthMax(value, out context),
            RuleType.LengthMin => TryValidateLengthMin(value, out context),
            RuleType.Child => TryValidateChild(value, out context),
            RuleType.Matches => TryValidateMatches(value, out context),
            _ => throw new Exception()
        };
    }
    private bool TryValidateEqualTo(object value, out IValidationContext context)
    {
        //this.Error ??= new ValidationConfigurableJsonError()
        //{

        //};

        //if (value is null && this.Value is null)
        //{
        //    context = new ValidationContext<object>(value);
        //    return true;
        //}
        //else if (!value.Equals(this.Value))
        //{
        //    context = new ValidationContext<object>(value);
        //    context.AddFailure(this.Error);
        //    return true;
        //}



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
        var compare = this.Parameters["$value"];

        if (value is null)
        {
            context = new ValidationContext<object>(value);
            context.AddFailure(this.Error);
            return true;
        }
        else if (value is IComparable comparable)
        {
            context = new ValidationContext<object>(value);
            
            if (comparable.CompareTo(compare) <= 0)
            {
                context = new ValidationContext<object>(value);
                context.AddFailure(this.Error);
            }
            return true;
        }
        else
        {
            context = null;
            return false;
        }
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
        if (value is null)
        {

        }


        context = null;
        return false;
    }


    internal void SetRuleConversion(Type type)
    {
        foreach(var kv in this.Parameters)
        {
          
        }
        if (ruleType == RuleType.Child)
        {
            if (this.Parameters.TryGetValue("$value", out var element))
            {
                var item = typeof(ValidationConfigurableJsonItem<>).MakeGenericType(type);
            }
        }
        if (ruleType == RuleType.GreaterThan)
        {
            if (type.IsValueType && type.GetInterface("IComparable") is null)
            {
                // TODO: Throw Exception
            }

            

            var parameter = (JsonElement)this.Parameters["$value"];

            if (parameter.ValueKind == JsonValueKind.Number)
            {
                if (type == typeof(short))
                    this.Parameters["$value"] = parameter.GetInt16();
            }
        }
    }
}