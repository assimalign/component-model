using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Assimalign.ComponentModel.Validation.Configurable;

using Assimalign.ComponentModel.Validation.Properties;
using Assimalign.ComponentModel.Validation.Configurable.Serialization;
using Assimalign.ComponentModel.Validation.Configurable.Internal.Exceptions;


/// <summary>
/// 
/// </summary>
/// <param name="value"></param>
/// <param name="context"></param>
/// <returns></returns>
internal delegate bool Validate(object value, out IValidationContext context);

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
internal sealed class ValidationConfigurableJsonRule<T> : IValidationRule
{
    private Validate Validate;


    /// <summary>
    /// The name of the validation rule to apply to <typeparamref name="T"/>.
    /// </summary>
    [JsonPropertyName("$rule")]
    public string Name { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("$error")]
    public ValidationConfigurableJsonError Error { get; set; }

    /// <summary>
    /// This will store the parameter values used to run the validation rules.
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
    public bool TryValidate(object value, out IValidationContext context) => Validate(value, out context);
    private bool TryValidateEqualTo(object value, out IValidationContext context)
    {
        var compareValue = this.Parameters["$value"];

        if (value is null)
        {
            context = new ValidationContext<object>(value);
            return true;
        }
        else if (!value.Equals(compareValue))
        {
            context = new ValidationContext<object>(value);
            context.AddFailure(this.Error);
            return true;
        }
        else
        {
            context = null;
            return false;
        }
    }
    private bool TryValidateNotEqualTo(object value, out IValidationContext context)
    {
        var compareValue = this.Parameters["$value"];

        if (value is null && compareValue is not null)
        {
            context = new ValidationContext<object>(value);
            return true;
        }
        else if (value.Equals(compareValue))
        {
            context = new ValidationContext<object>(value);
            context.AddFailure(this.Error);
            return true;
        }
        else
        {
            context = null;
            return false;
        }
    }
    private bool TryValidateEmpty(object value, out IValidationContext context)
    {
        switch (value)
        {
            case null:
                {
                    context = new ValidationContext<object>(value);
                    return true;
                }
            case string stringValue when !string.IsNullOrWhiteSpace(stringValue):
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
    private bool TryValidateNotEmpty(object value, out IValidationContext context)
    {
        switch (value)
        {
            case null:
                {
                    context = new ValidationContext<object>(value);
                    context.AddFailure(this.Error);
                    return true;
                }
            case string stringValue when string.IsNullOrWhiteSpace(stringValue):
                {
                    context = new ValidationContext<object>(value);
                    context.AddFailure(this.Error);
                    return true;
                }
            case ICollection collection when collection.Count <= 0:
                {
                    context = new ValidationContext<object>(value);
                    context.AddFailure(this.Error);
                    return true;
                }
            case Array array when array.Length <= 0:
                {
                    context = new ValidationContext<object>(value);
                    context.AddFailure(this.Error);
                    return true;
                }
            case IEnumerable enumerable when !enumerable.Cast<object>().Any():
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
        context = new ValidationContext<object>(value);

        if (value is null)
        {
            context.AddFailure(this.Error);
            return true;
        }
        else if (value is IComparable comparable)
        {
            var upper = this.Parameters["$upper"];
            var lower = this.Parameters["$lower"];

            if (comparable.CompareTo(lower) <= 0 && comparable.CompareTo(upper) >= 0)
            {
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
    private bool TryValidateBetweenOrEqualTo(object value, out IValidationContext context)
    {
        context = new ValidationContext<object>(value);

        if (value is null)
        {
            context.AddFailure(this.Error);
            return true;
        }
        else if (value is IComparable comparable)
        {
            var upper = this.Parameters["$upper"];
            var lower = this.Parameters["$lower"];

            if (comparable.CompareTo(lower) < 0 && comparable.CompareTo(upper) > 0)
            {
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
    private bool TryValidateGreaterThan(object value, out IValidationContext context)
    {
        context = new ValidationContext<object>(value);

        if (value is null)
        {
            context.AddFailure(this.Error);
            return true;
        }
        else if (value is IComparable comparable)
        {
            var compare = this.Parameters["$value"];

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
        context = new ValidationContext<object>(value);

        if (value is null)
        {
            context.AddFailure(this.Error);
            return true;
        }
        else if (value is IComparable comparable)
        {
            var compare = this.Parameters["$value"];

            if (comparable.CompareTo(compare) < 0)
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
    private bool TryValidateLessThan(object value, out IValidationContext context)
    {
        context = new ValidationContext<object>(value);

        if (value is null)
        {
            context.AddFailure(this.Error);
            return true;
        }
        else if (value is IComparable comparable)
        {
            var compare = this.Parameters["$value"];

            if (comparable.CompareTo(compare) >= 0)
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
    private bool TryValidateLessThanOrEqualTo(object value, out IValidationContext context)
    {
        context = new ValidationContext<object>(value);

        if (value is null)
        {
            context.AddFailure(this.Error);
            return true;
        }
        else if (value is IComparable comparable)
        {
            var compare = this.Parameters["$value"];

            if (comparable.CompareTo(compare) > 0)
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
    private bool TryValidateEmailAddress(object value, out IValidationContext context)
    {
        throw new NotImplementedException();
    }
    private bool TryValidateNull(object value, out IValidationContext context)
    {
        context = new ValidationContext<object>(value);

        if (value is not null)
        {
            context.AddFailure(this.Error);
            return true;
        }

        return true;
    }
    private bool TryValidateNotNull(object value, out IValidationContext context)
    {
        context = new ValidationContext<object>(value);

        if (value is null)
        {
            context.AddFailure(this.Error);
            return true;
        }

        return true;
    }
    private bool TryValidateLength(object value, out IValidationContext context)
    {
        context = null;
        if (value is null)
        {
            context = new ValidationContext<object>(default);
            context.AddFailure(this.Error);
            return true;
        }
        if (this.Parameters.TryGetValue("$exact", out var parameter))
        {
            var element = (JsonElement)parameter;
            var exact = element.GetInt32();
            context = new ValidationContext<object>(value);

            switch (value)
            {
                case string stringValue when stringValue.Length != exact:
                    {
                        context.AddFailure(this.Error);
                        return true;
                    } // May not need this since string is IEnumerable
                case ICollection collection when collection.Count != exact:
                    {
                        context.AddFailure(this.Error);
                        return true;
                    }
                case Array array when array.Length != exact:
                    {
                        context.AddFailure(this.Error);
                        return true;
                    }
                case IEnumerable enumerable when enumerable.Cast<object>().Count() != exact:
                    {
                        context.AddFailure(this.Error);
                        return true;
                    }
            };

            context = null;
            return false;
        }
        else
        {
            return false;
        }
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

    internal void Configure(params object[] parameters)
    {
        var type = parameters.FirstOrDefault(x => x is Type) as Type;
        var expression = parameters.FirstOrDefault(x => x is Expression<Func<T, object>>) as Expression<Func<T, object>>;
        this.Parameters ??= new Dictionary<string, object>();

        switch (this.Name)
        {
            case "EqualTo":
                {
                    if (!this.Parameters.ContainsKey("$value"))
                    {
                        throw new ValidationConfigurableJsonMissingParameterException("");
                    }
                    this.Parameters["$value"] = this.GetJsonElementValue((JsonElement)this.Parameters["$value"], type);
                    this.Error ??= new ValidationConfigurableJsonError()
                    {
                        Code = Resources.DefaultValidationErrorCode,
                        Message = string.Format(Resources.DefaultValidationMessageEqualToRule, expression, this.Parameters["$value"]),
                        Source = expression.Body.ToString()
                    };
                    this.Validate = TryValidateEqualTo;
                    break;

                }
            case "NotEqualTo":
                {
                    if (!this.Parameters.ContainsKey("$value"))
                    {
                        throw new ValidationConfigurableJsonMissingParameterException("");
                    }
                    this.Parameters["$value"] = this.GetJsonElementValue((JsonElement)this.Parameters["$value"], type);
                    this.Error ??= new ValidationConfigurableJsonError()
                    {
                        Code = Resources.DefaultValidationErrorCode,
                        Message = string.Format(Resources.DefaultValidationMessageNotEqualToRule, expression, this.Parameters["$value"]),
                        Source = expression.Body.ToString()
                    };
                    this.Validate = TryValidateNotEqualTo;
                    break;

                }
            case "Empty":
                {
                    this.Error ??= new ValidationConfigurableJsonError()
                    {
                        Code = Resources.DefaultValidationErrorCode,
                        Message = string.Format(Resources.DefaultValidationMessageEmptyRule, expression),
                        Source = expression.Body.ToString()
                    };
                    this.Validate = TryValidateEmpty;
                    break;
                }
            case "NotEmpty":
                {
                    this.Error ??= new ValidationConfigurableJsonError()
                    {
                        Code = Resources.DefaultValidationErrorCode,
                        Message = string.Format(Resources.DefaultValidationMessageNotEmptyRule, expression),
                        Source = expression.Body.ToString()
                    };
                    this.Validate = TryValidateNotEmpty;
                    break;
                }
            case "Between":
                {
                    if (type.GetInterface("IComparable") is null)
                    {
                        throw new ValidationConfigurableJsonInvalidEvaluationException(expression, "Between");
                    }
                    if (!this.Parameters.ContainsKey("$lower") || !this.Parameters.ContainsKey("$upper"))
                    {
                        throw new ValidationConfigurableJsonMissingParameterException("");
                    }
                    this.Parameters["$lower"] = this.GetJsonElementValue((JsonElement)this.Parameters["$lower"], type);
                    this.Parameters["$upper"] = this.GetJsonElementValue((JsonElement)this.Parameters["$upper"], type);
                    this.Error ??= new ValidationConfigurableJsonError()
                    {
                        Code = Resources.DefaultValidationErrorCode,
                        Message = string.Format(Resources.DefaultValidationMessageBetweenRule, expression, this.Parameters["$lower"], this.Parameters["$upper"]),
                        Source = expression.Body.ToString()
                    };
                    this.Validate = TryValidateBetween;
                    break;
                }
            case "BetweenOrEqualTo":
                {
                    if (type.GetInterface("IComparable") is null)
                    {
                        throw new ValidationConfigurableJsonInvalidEvaluationException("", "BetweenOrEqualTo");
                    }
                    if (!this.Parameters.ContainsKey("$lower") || !this.Parameters.ContainsKey("$upper"))
                    {
                        throw new ValidationConfigurableJsonMissingParameterException("");
                    }
                    this.Parameters["$lower"] = this.GetJsonElementValue((JsonElement)this.Parameters["$lower"], type);
                    this.Parameters["$upper"] = this.GetJsonElementValue((JsonElement)this.Parameters["$upper"], type);
                    this.Error ??= new ValidationConfigurableJsonError()
                    {
                        Code = Resources.DefaultValidationErrorCode,
                        Message = string.Format(Resources.DefaultValidationMessageBetweenOrEqualToRule, expression, this.Parameters["$lower"], this.Parameters["$upper"]),
                        Source = expression.Body.ToString()
                    };
                    this.Validate = TryValidateBetweenOrEqualTo;
                    break;
                }
            case "GreaterThan":
                {
                    if (type.GetInterface("IComparable") is null)
                    {
                        throw new ValidationConfigurableJsonInvalidEvaluationException("", "GreaterThan");
                    }
                    if (!this.Parameters.ContainsKey("$value"))
                    {
                        throw new ValidationConfigurableJsonMissingParameterException("");
                    }
                    this.Parameters["$value"] = this.GetJsonElementValue((JsonElement)this.Parameters["$value"], type);
                    this.Validate = TryValidateGreaterThan;
                    break;
                }
            case "GreaterThanOrEqualTo":
                {
                    if (type.GetInterface("IComparable") is null)
                    {
                        throw new ValidationConfigurableJsonInvalidEvaluationException("", "Between");
                    }
                    if (!this.Parameters.ContainsKey("$value"))
                    {
                        throw new ValidationConfigurableJsonMissingParameterException("");
                    }
                    this.Parameters["$value"] = this.GetJsonElementValue((JsonElement)this.Parameters["$value"], type);
                    this.Validate = TryValidateGreaterThanOrEqualTo;
                    break;
                }

            case "LessThan":
                {
                    if (type.GetInterface("IComparable") is null)
                    {
                        throw new ValidationConfigurableJsonInvalidEvaluationException("", "Between");
                    }
                    if (!this.Parameters.ContainsKey("$value"))
                    {
                        throw new ValidationConfigurableJsonMissingParameterException("");
                    }
                    this.Parameters["$value"] = this.GetJsonElementValue((JsonElement)this.Parameters["$value"], type);
                    this.Validate = TryValidateLessThan;
                    break;
                }
            case "LessThanOrEqualTo":
                {
                    if (type.GetInterface("IComparable") is null)
                    {
                        throw new ValidationConfigurableJsonInvalidEvaluationException("", "Between");
                    }
                    if (!this.Parameters.ContainsKey("$value"))
                    {
                        throw new ValidationConfigurableJsonMissingParameterException("");
                    }
                    this.Parameters["$value"] = this.GetJsonElementValue((JsonElement)this.Parameters["$value"], type);
                    this.Validate = TryValidateLessThanOrEqualTo;
                    break;
                }
            case "EmailAddress":
                this.Validate = TryValidateEmailAddress;
                break;
            case "Null":
                this.Validate = TryValidateNull;
                break;
            case "NotNull":
                this.Validate = TryValidateNotNull;
                break;
            case "Length":
                this.Parameters["$value"] = this.GetJsonElementValue((JsonElement)this.Parameters["$value"], typeof(int));
                this.Validate = TryValidateLength;
                break;
            case "LengthBetween":
                this.Validate = TryValidateLengthBetween;
                break;
            case "LengthMax":
                this.Validate = TryValidateLengthMax;
                break;
            case "LengthMin":
                this.Validate = TryValidateLengthMin;
                break;
            case "Child":
                this.Validate = TryValidateChild;
                break;
            case "Matches":
                this.Validate = TryValidateMatches;
                break;
        };
    }
    private object GetJsonElementValue(JsonElement element, Type type)
    {
        return type.Name switch
        {
            "Boolean" when this.Name != "" => element.GetBoolean(),
            "UInt16" => element.GetUInt16(),
            "UInt32" => element.GetUInt32(),
            "UInt64" => element.GetUInt64(),
            "Int16" => element.GetInt16(),
            "Int32" => element.GetInt32(),
            "Int64" => element.GetInt64(),
            "Single" => element.GetSingle(),
            "Double" => element.GetDouble(),
            "Decimal" => element.GetDecimal(),
            "DateTime" => element.GetDateTime(),
            "DateTimeOffset" => element.GetDateTimeOffset(),
            "TimeSpan" => TimeSpan.Parse(element.GetString()),
            //"Char" => element.GetByte(),
#if NET6_0_OR_GREATER
            "DateOnly" => DateOnly.Parse(element.GetString()),
            "TimeOnly" => DateOnly.Parse(element.GetString()),
            "Held" => Half.Parse(element.GetString()),
#endif
            "Guid" => element.GetGuid(),
            "String" => element.GetString(),
            _ => null
        };
    }
}