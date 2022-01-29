using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Assimalign.ComponentModel.Validation.Configurable;

using Assimalign.ComponentModel.Validation.Properties;
using Assimalign.ComponentModel.Validation.Configurable.Serialization;
using Assimalign.ComponentModel.Validation.Configurable.Internal.Exceptions;
using Assimalign.ComponentModel.Validation.Configurable.Internal.Extensions;
using System.Reflection;


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
        const string pattern = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-||_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+([a-z]+|\d|-|\.{0,1}|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])?([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))$"; ;

        context = new ValidationContext<object>(value);

        if (value is null)
        {
            context.AddFailure(this.Error);
            return true;
        }
        else if (value is string stringValue)
        {
            if (!Regex.IsMatch(stringValue, pattern))
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
                case string stringValue:
                    {
                        if (stringValue.Length != exact)
                        {
                            context.AddFailure(this.Error);
                        }
                        return true;
                    } // May not need this since string is IEnumerable
                case Array array:
                    {
                        if (array.Length != exact)
                        {
                            context.AddFailure(this.Error);
                        }
                        return true;
                    }
                case ICollection collection:
                    {
                        if (collection.Count != exact)
                        {
                            context.AddFailure(this.Error);
                        }
                        return true;
                    }
                case IEnumerable enumerable:
                    {
                        if (enumerable.Cast<object>().Count() != exact)
                        {
                            context.AddFailure(this.Error);
                        }
                        return true;
                    }
                default:
                    {
                        context = null;
                        return false;
                    }
            };
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
        context = null;

        if (value is null)
        {
            context = new ValidationContext<object>(default);
            context.AddFailure(this.Error);
            return true;
        }
        if (this.Parameters.TryGetValue("$max", out var parameter))
        {
            var max = (int)parameter;
            context = new ValidationContext<object>(value);

            switch (value)
            {
                case string stringValue:
                    {
                        if (stringValue.Length > max)
                        {
                            context.AddFailure(this.Error);
                        }
                        return true;
                    } // May not need this since string is IEnumerable
                case Array array:
                    {
                        if (array.Length > max)
                        {
                            context.AddFailure(this.Error);
                        }
                        return true;
                    }
                case ICollection collection:
                    {
                        if (collection.Count > max)
                        {
                            context.AddFailure(this.Error);
                        }
                        return true;
                    }
                case IEnumerable enumerable:
                    {
                        if (enumerable.Cast<object>().Count() > max)
                        {
                            context.AddFailure(this.Error);
                        }
                        return true;
                    }
                default:
                    {
                        context = null;
                        return false;
                    }
            };
        }
        else
        {
            return false;
        }
    }
    private bool TryValidateLengthMin(object value, out IValidationContext context)
    {
        context = null;

        if (value is null)
        {
            context = new ValidationContext<object>(default);
            context.AddFailure(this.Error);
            return true;
        }
        if (this.Parameters.TryGetValue("$min", out var parameter))
        {
            var min = (int)parameter;
            context = new ValidationContext<object>(value);

            switch (value)
            {
                case string stringValue:
                    {
                        if (stringValue.Length < min)
                        {
                            context.AddFailure(this.Error);
                        }
                        return true;
                    } // May not need this since string is IEnumerable
                case Array array:
                    {
                        if (array.Length < min)
                        {
                            context.AddFailure(this.Error);
                        }
                        return true;
                    }
                case ICollection collection:
                    {
                        if (collection.Count < min)
                        {
                            context.AddFailure(this.Error);
                        }
                        return true;
                    }
                case IEnumerable enumerable:
                    {
                        if (enumerable.Cast<object>().Count() < min)
                        {
                            context.AddFailure(this.Error);
                        }
                        return true;
                    }
                default:
                    {
                        context = null;
                        return false;
                    }
            };
        }
        else
        {
            return false;
        }
    }
    private bool TryValidateChild(object value, out IValidationContext context)
    {
        var items = (IEnumerable<IValidationItem>)this.Parameters["$validationItems"];

        context = new ValidationContext<object>(value)
        {
            Options = new Dictionary<string, object>()
        };

        foreach (var item in items)
        {
            

            var childContext = new ValidationContext<object>(value)
            {
                Options = new Dictionary<string, object>()
            };

            item.Evaluate(childContext);

            foreach (var error in childContext.Errors)
            {
                context.AddFailure(error);
            }
        }

        return true;
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
        //var type = parameters.FirstOrDefault(x => x is Type) as Type;
        var itemType = (ValidationConfigurableItemType)parameters.FirstOrDefault(x => x is ValidationConfigurableItemType);
        var itemExpression = parameters.FirstOrDefault(x => x is Expression<Func<T, object>>) as Expression<Func<T, object>>;
        var itemValidationMode = (ValidationMode)parameters.FirstOrDefault(x => x is ValidationMode);

        this.Parameters ??= new Dictionary<string, object>();

        switch (this.Name)
        {
            case "EqualTo":
                {
                    Type type = null;

                    if (itemType == ValidationConfigurableItemType.Recursive)
                    {
                        type = itemExpression.Body.Type.GetEnumerableType().UnwrapNullableType();
                    }
                    else if (itemType == ValidationConfigurableItemType.Inline)
                    {
                        type = itemExpression.Body.Type.UnwrapNullableType();
                    }
                    if (!this.Parameters.ContainsKey("$value"))
                    {
                        throw new ValidationConfigurableJsonMissingParameterException("EqualTo", "$value", itemExpression);
                    }
                    this.Parameters["$value"] = this.GetJsonElementValue((JsonElement)this.Parameters["$value"], type);
                    this.Error ??= new ValidationConfigurableJsonError()
                    {
                        Code = Resources.DefaultValidationErrorCode,
                        Message = string.Format(Resources.DefaultValidationMessageEqualToRule, itemExpression, this.Parameters["$value"]),
                        Source = itemExpression.Body.ToString()
                    };
                    this.Validate = TryValidateEqualTo;
                    break;

                }
            case "NotEqualTo":
                {
                    Type type = null;

                    if (itemType == ValidationConfigurableItemType.Recursive)
                    {
                        type = itemExpression.Body.Type.GetEnumerableType().UnwrapNullableType();
                    }
                    else if (itemType == ValidationConfigurableItemType.Inline)
                    {
                        type = itemExpression.Body.Type.UnwrapNullableType();
                    }
                    if (!this.Parameters.ContainsKey("$value"))
                    {
                        throw new ValidationConfigurableJsonMissingParameterException("NotEqualTo", $"value", itemExpression);
                    }
                    this.Parameters["$value"] = this.GetJsonElementValue((JsonElement)this.Parameters["$value"], type);
                    this.Error ??= new ValidationConfigurableJsonError()
                    {
                        Code = Resources.DefaultValidationErrorCode,
                        Message = string.Format(Resources.DefaultValidationMessageNotEqualToRule, itemExpression, this.Parameters["$value"]),
                        Source = itemExpression.Body.ToString()
                    };
                    this.Validate = TryValidateNotEqualTo;
                    break;

                }
            case "Empty":
                {
                    Type type = null;

                    if (itemType == ValidationConfigurableItemType.Recursive)
                    {
                        type = itemExpression.Body.Type.GetEnumerableType().UnwrapNullableType();
                    }
                    else if (itemType == ValidationConfigurableItemType.Inline)
                    {
                        type = itemExpression.Body.Type.UnwrapNullableType();
                    }
                    if (type.GetInterface("IEnumerable") is null)
                    {
                        throw new ValidationConfigurableJsonInvalidEvaluationException(itemExpression, "Empty");
                    }
                    this.Error ??= new ValidationConfigurableJsonError()
                    {
                        Code = Resources.DefaultValidationErrorCode,
                        Message = string.Format(Resources.DefaultValidationMessageEmptyRule, itemExpression),
                        Source = itemExpression.Body.ToString()
                    };
                    this.Validate = TryValidateEmpty;
                    break;
                }
            case "NotEmpty":
                {
                    Type type = null;

                    if (itemType == ValidationConfigurableItemType.Recursive)
                    {
                        type = itemExpression.Body.Type.GetEnumerableType().UnwrapNullableType();
                    }
                    else if (itemType == ValidationConfigurableItemType.Inline)
                    {
                        type = itemExpression.Body.Type.UnwrapNullableType();
                    }
                    if (type.GetInterface("IEnumerable") is null)
                    {
                        throw new ValidationConfigurableJsonInvalidEvaluationException(itemExpression, "NotEmpty");
                    }
                    this.Error ??= new ValidationConfigurableJsonError()
                    {
                        Code = Resources.DefaultValidationErrorCode,
                        Message = string.Format(Resources.DefaultValidationMessageNotEmptyRule, itemExpression),
                        Source = itemExpression.Body.ToString()
                    };
                    this.Validate = TryValidateNotEmpty;
                    break;
                }
            case "Between":
                {
                    Type type = null;

                    if (itemType == ValidationConfigurableItemType.Recursive)
                    {
                        type = itemExpression.Body.Type.GetEnumerableType().UnwrapNullableType();
                    }
                    else if (itemType == ValidationConfigurableItemType.Inline)
                    {
                        type = itemExpression.Body.Type.UnwrapNullableType();
                    }
                    if (type.GetInterface("IComparable") is null)
                    {
                        throw new ValidationConfigurableJsonInvalidEvaluationException(itemExpression, "Between");
                    }
                    if (!this.Parameters.ContainsKey("$lower") || !this.Parameters.ContainsKey("$upper"))
                    {
                        throw new ValidationConfigurableJsonMissingParameterException("BetweenOrEqualTo", "$lower", "$upper", itemExpression);
                    }
                    this.Parameters["$lower"] = this.GetJsonElementValue((JsonElement)this.Parameters["$lower"], type);
                    this.Parameters["$upper"] = this.GetJsonElementValue((JsonElement)this.Parameters["$upper"], type);
                    this.Error ??= new ValidationConfigurableJsonError()
                    {
                        Code = Resources.DefaultValidationErrorCode,
                        Message = string.Format(Resources.DefaultValidationMessageBetweenRule, itemExpression, this.Parameters["$lower"], this.Parameters["$upper"]),
                        Source = itemExpression.Body.ToString()
                    };
                    this.Validate = TryValidateBetween;
                    break;
                }
            case "BetweenOrEqualTo":
                {
                    Type type = null;

                    if (itemType == ValidationConfigurableItemType.Recursive)
                    {
                        type = itemExpression.Body.Type.GetEnumerableType().UnwrapNullableType();
                    }
                    else if (itemType == ValidationConfigurableItemType.Inline)
                    {
                        type = itemExpression.Body.Type.UnwrapNullableType();
                    }
                    if (type.GetInterface("IComparable") is null)
                    {
                        throw new ValidationConfigurableJsonInvalidEvaluationException(itemExpression, "BetweenOrEqualTo");
                    }
                    if (!this.Parameters.ContainsKey("$lower") || !this.Parameters.ContainsKey("$upper"))
                    {
                        throw new ValidationConfigurableJsonMissingParameterException("BetweenOrEqualTo", "$lower", "$upper", itemExpression);
                    }
                    this.Parameters["$lower"] = this.GetJsonElementValue((JsonElement)this.Parameters["$lower"], type);
                    this.Parameters["$upper"] = this.GetJsonElementValue((JsonElement)this.Parameters["$upper"], type);
                    this.Error ??= new ValidationConfigurableJsonError()
                    {
                        Code = Resources.DefaultValidationErrorCode,
                        Message = string.Format(Resources.DefaultValidationMessageBetweenOrEqualToRule, itemExpression, this.Parameters["$lower"], this.Parameters["$upper"]),
                        Source = itemExpression.Body.ToString()
                    };
                    this.Validate = TryValidateBetweenOrEqualTo;
                    break;
                }
            case "GreaterThan":
                {
                    Type type = null;

                    if (itemType == ValidationConfigurableItemType.Recursive)
                    {
                        type = itemExpression.Body.Type.GetEnumerableType().UnwrapNullableType();
                    }
                    else if (itemType == ValidationConfigurableItemType.Inline)
                    {
                        type = itemExpression.Body.Type.UnwrapNullableType();
                    }
                    if (type.GetInterface("IComparable") is null)
                    {
                        throw new ValidationConfigurableJsonInvalidEvaluationException(itemExpression, "GreaterThan");
                    }
                    if (!this.Parameters.ContainsKey("$value"))
                    {
                        throw new ValidationConfigurableJsonMissingParameterException("GreaterThan", "$value", itemExpression);
                    }
                    this.Parameters["$value"] = this.GetJsonElementValue((JsonElement)this.Parameters["$value"], type);
                    this.Error ??= new ValidationConfigurableJsonError()
                    {
                        Code = Resources.DefaultValidationErrorCode,
                        Message = string.Format(Resources.DefaultValidationMessageGreaterThanRule, itemExpression, this.Parameters["$value"]),
                        Source = itemExpression.Body.ToString()
                    };
                    this.Validate = TryValidateGreaterThan;
                    break;
                }
            case "GreaterThanOrEqualTo":
                {
                    Type type = null;

                    if (itemType == ValidationConfigurableItemType.Recursive)
                    {
                        type = itemExpression.Body.Type.GetEnumerableType().UnwrapNullableType();
                    }
                    else if (itemType == ValidationConfigurableItemType.Inline)
                    {
                        type = itemExpression.Body.Type.UnwrapNullableType();
                    }
                    if (type.GetInterface("IComparable") is null)
                    {
                        throw new ValidationConfigurableJsonInvalidEvaluationException(itemExpression, "GreaterThanOrEqualTo");
                    }
                    if (!this.Parameters.ContainsKey("$value"))
                    {
                        throw new ValidationConfigurableJsonMissingParameterException("GreaterThanOrEqualTo", "$value", itemExpression);
                    }
                    this.Parameters["$value"] = this.GetJsonElementValue((JsonElement)this.Parameters["$value"], type);
                    this.Error ??= new ValidationConfigurableJsonError()
                    {
                        Code = Resources.DefaultValidationErrorCode,
                        Message = string.Format(Resources.DefaultValidationMessageGreaterThanOrEqualToRule, itemExpression, this.Parameters["$value"]),
                        Source = itemExpression.Body.ToString()
                    };
                    this.Validate = TryValidateGreaterThanOrEqualTo;
                    break;
                }
            case "LessThan":
                {
                    Type type = null;

                    if (itemType == ValidationConfigurableItemType.Recursive)
                    {
                        type = itemExpression.Body.Type.GetEnumerableType().UnwrapNullableType();
                    }
                    else if (itemType == ValidationConfigurableItemType.Inline)
                    {
                        type = itemExpression.Body.Type.UnwrapNullableType();
                    }
                    if (type.GetInterface("IComparable") is null)
                    {
                        throw new ValidationConfigurableJsonInvalidEvaluationException(itemExpression, "LessThan");
                    }
                    if (!this.Parameters.ContainsKey("$value"))
                    {
                        throw new ValidationConfigurableJsonMissingParameterException("LessThan", "$value", itemExpression);
                    }
                    this.Parameters["$value"] = this.GetJsonElementValue((JsonElement)this.Parameters["$value"], type);
                    this.Error ??= new ValidationConfigurableJsonError()
                    {
                        Code = Resources.DefaultValidationErrorCode,
                        Message = string.Format(Resources.DefaultValidationMessageLessThanRule, itemExpression, this.Parameters["$value"]),
                        Source = itemExpression.Body.ToString()
                    };
                    this.Validate = TryValidateLessThan;
                    break;
                }
            case "LessThanOrEqualTo":
                {
                    Type type = null;

                    if (itemType == ValidationConfigurableItemType.Recursive)
                    {
                        type = itemExpression.Body.Type.GetEnumerableType().UnwrapNullableType();
                    }
                    else if (itemType == ValidationConfigurableItemType.Inline)
                    {
                        type = itemExpression.Body.Type.UnwrapNullableType();
                    }
                    if (type.GetInterface("IComparable") is null)
                    {
                        throw new ValidationConfigurableJsonInvalidEvaluationException(itemExpression, "LessThanOrEqualTo");
                    }
                    if (!this.Parameters.ContainsKey("$value"))
                    {
                        throw new ValidationConfigurableJsonMissingParameterException("LessThanOrEqualTo", "$value", itemExpression);
                    }
                    this.Parameters["$value"] = this.GetJsonElementValue((JsonElement)this.Parameters["$value"], type);
                    this.Error ??= new ValidationConfigurableJsonError()
                    {
                        Code = Resources.DefaultValidationErrorCode,
                        Message = string.Format(Resources.DefaultValidationMessageLessThanOrEqualToRule, itemExpression, this.Parameters["$value"]),
                        Source = itemExpression.Body.ToString()
                    };
                    this.Validate = TryValidateLessThanOrEqualTo;
                    break;
                }
            case "EmailAddress":
                {
                    Type type = null;

                    if (itemType == ValidationConfigurableItemType.Recursive)
                    {
                        type = itemExpression.Body.Type.GetEnumerableType().UnwrapNullableType();
                    }
                    else if (itemType == ValidationConfigurableItemType.Inline)
                    {
                        type = itemExpression.Body.Type.UnwrapNullableType();
                    }
                    if (type != typeof(string))
                    {
                        throw new ValidationConfigurableJsonInvalidEvaluationException(itemExpression, "EmailAddress");
                    }
                    if (!this.Parameters.ContainsKey("$value"))
                    {
                        throw new ValidationConfigurableJsonMissingParameterException("EmailAddress", "$value", itemExpression);
                    }
                    this.Error ??= new ValidationConfigurableJsonError()
                    {
                        Code = Resources.DefaultValidationErrorCode,
                        Message = string.Format(Resources.DefaultValidationMessageEmailAddressRule, itemExpression),
                        Source = itemExpression.Body.ToString()
                    };
                    this.Validate = TryValidateEmailAddress;
                    break;
                }
            case "Null":
                {
                    this.Error ??= new ValidationConfigurableJsonError()
                    {
                        Code = Resources.DefaultValidationErrorCode,
                        Message = string.Format(Resources.DefaultValidationMessageNullRule, itemExpression),
                        Source = itemExpression.Body.ToString()
                    };
                    this.Validate = TryValidateNull;
                    break;
                }
            case "NotNull":
                {
                    this.Error ??= new ValidationConfigurableJsonError()
                    {
                        Code = Resources.DefaultValidationErrorCode,
                        Message = string.Format(Resources.DefaultValidationMessageNotNullRule, itemExpression),
                        Source = itemExpression.Body.ToString()
                    };
                    this.Validate = TryValidateNotNull;
                    break;
                }
            case "Length":
                {
                    Type type = null;

                    if (itemType == ValidationConfigurableItemType.Recursive)
                    {
                        type = itemExpression.Body.Type.GetEnumerableType().UnwrapNullableType();
                    }
                    else if (itemType == ValidationConfigurableItemType.Inline)
                    {
                        type = itemExpression.Body.Type.UnwrapNullableType();
                    }
                    if (type.GetInterface("IEnumerable") is null)
                    {
                        throw new ValidationConfigurableJsonInvalidEvaluationException(itemExpression, "Length");
                    }
                    if (!this.Parameters.ContainsKey("$exact"))
                    {
                        throw new ValidationConfigurableJsonMissingParameterException("Length", "$exact", itemExpression);
                    }
                    this.Error ??= new ValidationConfigurableJsonError()
                    {
                        Code = Resources.DefaultValidationErrorCode,
                        Message = string.Format(Resources.DefaultValidationMessageLengthRule, itemExpression),
                        Source = itemExpression.Body.ToString()
                    };
                    this.Parameters["$exact"] = this.GetJsonElementValue((JsonElement)this.Parameters["$exact"], typeof(int));
                    this.Validate = TryValidateLength;
                    break;
                }
            case "LengthBetween":
                {
                    Type type = null;

                    if (itemType == ValidationConfigurableItemType.Recursive)
                    {
                        type = itemExpression.Body.Type.GetEnumerableType().UnwrapNullableType();
                    }
                    else if (itemType == ValidationConfigurableItemType.Inline)
                    {
                        type = itemExpression.Body.Type.UnwrapNullableType();
                    }
                    if (type.GetInterface("IEnumerable") is null)
                    {
                        throw new ValidationConfigurableJsonInvalidEvaluationException(itemExpression, "LengthBetween");
                    }
                    if (!this.Parameters.ContainsKey("$min") || !this.Parameters.ContainsKey("$max"))
                    {
                        throw new ValidationConfigurableJsonMissingParameterException("LengthBetween", "$min", "$max", itemExpression);
                    }
                    this.Parameters["$max"] = this.GetJsonElementValue((JsonElement)this.Parameters["$max"], typeof(int));
                    this.Parameters["$min"] = this.GetJsonElementValue((JsonElement)this.Parameters["$min"], typeof(int));
                    this.Validate = TryValidateLengthBetween;
                    break;
                }
            case "LengthMax":
                {
                    Type type = null;

                    if (itemType == ValidationConfigurableItemType.Recursive)
                    {
                        type = itemExpression.Body.Type.GetEnumerableType().UnwrapNullableType();
                    }
                    else if (itemType == ValidationConfigurableItemType.Inline)
                    {
                        type = itemExpression.Body.Type.UnwrapNullableType();
                    }
                    if (type.GetInterface("IEnumerable") is null)
                    {
                        throw new ValidationConfigurableJsonInvalidEvaluationException(itemExpression, "LengthMax");
                    }
                    if (!this.Parameters.ContainsKey("$max"))
                    {
                        throw new ValidationConfigurableJsonMissingParameterException("LengthMax", "$max", itemExpression);
                    }
                    this.Parameters["$max"] = this.GetJsonElementValue((JsonElement)this.Parameters["$max"], typeof(int));
                    this.Error ??= new ValidationConfigurableJsonError()
                    {
                        Code = Resources.DefaultValidationErrorCode,
                        Message = string.Format(Resources.DefaultValidationMessageMaxLengthRule, itemExpression, this.Parameters["$max"]),
                        Source = itemExpression.Body.ToString()
                    };
                    this.Validate = TryValidateLengthMax;
                    break;
                }
            case "LengthMin":
                {
                    Type type = null;

                    if (itemType == ValidationConfigurableItemType.Recursive)
                    {
                        type = itemExpression.Body.Type.GetEnumerableType().UnwrapNullableType();
                    }
                    else if (itemType == ValidationConfigurableItemType.Inline)
                    {
                        type = itemExpression.Body.Type.UnwrapNullableType();
                    }
                    if (type.GetInterface("IEnumerable") is null)
                    {
                        throw new ValidationConfigurableJsonInvalidEvaluationException(itemExpression, "LengthMin");
                    }
                    if (!this.Parameters.ContainsKey("$min"))
                    {
                        throw new ValidationConfigurableJsonMissingParameterException("LengthMin", "$min", itemExpression);
                    }
                    this.Parameters["$min"] = this.GetJsonElementValue((JsonElement)this.Parameters["$min"], typeof(int));
                    this.Error ??= new ValidationConfigurableJsonError()
                    {
                        Code = Resources.DefaultValidationErrorCode,
                        Message = string.Format(Resources.DefaultValidationMessageMinLengthRule, itemExpression, this.Parameters["$min"]),
                        Source = itemExpression.Body.ToString()
                    };
                    this.Validate = TryValidateLengthMin;
                    break;
                }
            case "Child":
                {
                    Type type = null;

                    if (itemType == ValidationConfigurableItemType.Recursive)
                    {
                        type = itemExpression.Body.Type.GetEnumerableType().UnwrapNullableType();
                    }
                    else if (itemType == ValidationConfigurableItemType.Inline)
                    {
                        type = itemExpression.Body.Type.UnwrapNullableType();
                    }
                    if (!this.Parameters.ContainsKey("$validationItems"))
                    {
                        throw new ValidationConfigurableJsonMissingParameterException("Child", "$validationItems", itemExpression);
                    }
                    var element = (JsonElement)this.Parameters["$validationItems"];
                    var generic = typeof(IEnumerable<>).MakeGenericType(
                        typeof(ValidationConfigurableJsonItem<>
                    ).MakeGenericType(type));
                    var content = element.GetRawText();
                    var options = new JsonSerializerOptions();

                    options.Converters.Add(new EnumConverter<ValidationConfigurableItemType>());
                    options.Converters.Add(new EnumConverter<ValidationMode>());

                    var item = JsonSerializer.Deserialize(content, generic, options);

                    foreach (var value in (IEnumerable)item)
                    {
                        var method = value.GetType()
                            .GetMethod("Configure", BindingFlags.Instance | BindingFlags.NonPublic)
                            .Invoke(value, new object[]
                            {
                                new object[]
                                {
                                    itemValidationMode
                                }
                            });
                    }

                    this.Parameters["$validationItems"] = item;
                    this.Validate = TryValidateChild;
                    break;
                }
            case "Matches":
                this.Validate = TryValidateMatches;
                break;
        };
    }
    private object GetJsonElementValue(JsonElement element, Type type)
    {
        return type.Name switch
        {
            "Boolean" => element.GetBoolean(),
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