using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Configurable;

internal sealed class ValidationConfigurableJsonProfile<T> : IValidationProfile
{
    [JsonConstructor]
    public ValidationConfigurableJsonProfile()
    {
        this.ValidationType = typeof(T);
        this.ValidationConditions ??= new List<ValidationConfigurableJsonCondition<T>>();
        this.ValidationItems ??= new List<ValidationConfigurableJsonItem<T>>();
    }

    /// <summary>
    /// A informational summary of the validation profile 
    /// for <see cref="ValidationConfigurableJsonProfile{T}"/>.
    /// </summary>
    [JsonPropertyName("$description")]
    public string Description { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("$validationMode")]
    public ValidationMode ValidationMode { get; set; }
    
    [JsonPropertyName("$validationItems")]
    public IList<ValidationConfigurableJsonItem<T>> ValidationItems { get; set; }

    [JsonPropertyName("$validationConditions")]
    public IList<ValidationConfigurableJsonCondition<T>> ValidationConditions { get; set; }
    
    [JsonIgnore]
    public Type ValidationType { get; }

    [JsonIgnore]
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
                foreach(var item in condition.ValidationItems)
                {
                    yield return item;
                }
            }
        }
    }



    public void Configure()
    {
        ConfigureValidationItems();
        ConfigureValidationConditions();    
    }

    private void ConfigureValidationConditions()
    {
        // Need to push the conditional validation items into the current validation stack
        foreach (var validationCondition in this.ValidationConditions)
        {
            var condition = validationCondition.GetCondition();

            foreach (var validationItem in validationCondition.ValidationItems)
            {
                validationItem.SetItemValidationMode(this.ValidationMode);
                validationItem.SetItemValidationCondition(condition);
                validationItem.SetItemValidationErrorDefaults();
                validationItem.SetItemValidationRuleConversion();
            }
        }
    }

    private void ConfigureValidationItems()
    {
        foreach (var validationItem in this.ValidationItems)
        {
            validationItem.SetItemValidationMode(this.ValidationMode);
            validationItem.SetItemValidationErrorDefaults();
            validationItem.SetItemValidationRuleConversion();
        }
    }
}
