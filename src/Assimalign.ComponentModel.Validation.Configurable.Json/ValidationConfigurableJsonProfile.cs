using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Configurable;

using Assimalign.ComponentModel.Validation.Configurable.Internal.Exceptions;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class ValidationConfigurableJsonProfile<T> : IValidationProfile
    where T : class
{
    private bool isConfigured;
    Type IValidationProfile.ValidationType => typeof(T);
    IEnumerable<IValidationItem> IValidationProfile.ValidationItems
    {
        get
        {
            foreach (var item in this.ValidationItems)
            {
                yield return item;
            }

            foreach (var condition in this.ValidationConditions)
            {
                foreach (var item in condition.ValidationItems)
                {
                    yield return item;
                }
            }
        }
    }

    /// <summary>
    /// The default constructor for Validation Configurable JSON.
    /// </summary>
    [JsonConstructor]
    public ValidationConfigurableJsonProfile()
    {
        this.ValidationItems ??= new List<ValidationConfigurableJsonItem<T>>();
        this.ValidationConditions ??= new List<ValidationConfigurableJsonConditionItem<T>>();
    }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("$description")]
    public string Description { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("$validationMode")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ValidationMode ValidationMode { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("$validationItems")]
    public IEnumerable<ValidationConfigurableJsonItem<T>> ValidationItems { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("$validationConditions")]
    public IEnumerable<ValidationConfigurableJsonConditionItem<T>> ValidationConditions { get; set; }


    /// <summary>
    /// 
    /// </summary>
    public void Configure()
    {
        if (isConfigured)
        {
            return;
        }
        try
        {
            foreach (var validationCondition in this.ValidationConditions)
            {
                var condition = validationCondition.GetCondition();

                foreach (var validationItem in validationCondition.ValidationItems)
                {
                    validationItem.Configure(condition, this.ValidationMode);
                }
            }

            foreach (var validationItem in this.ValidationItems)
            {
                validationItem.Configure(this.ValidationMode);
            }

            isConfigured = true; // Let's set this so some idiot doesn't try to call this more than once
        }
        catch (Exception exception) when (exception is not ValidationConfigurableException)
        {
            throw ValidationConfigurableJsonInternalException.FromException(
                message: $"An un-handled exception was thrown while configuring {nameof(ValidationConfigurableJsonProfile<T>)}.", 
                exception: exception);
        }        
    }
}