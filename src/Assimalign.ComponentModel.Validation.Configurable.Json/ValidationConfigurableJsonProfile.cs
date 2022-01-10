using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Configurable;

internal sealed class ValidationConfigurableJsonProfile<T> : IValidationProfile
{
    private bool isConfigured;

    [JsonConstructor]
    public ValidationConfigurableJsonProfile()
    {
        this.ValidationType = typeof(T);
        this.ValidationConditions ??= new List<ValidationConfigurableJsonCondition<T>>();
        this.ValidationItems ??= new List<ValidationConfigurableJsonItem<T>>();
    }

    [JsonPropertyName("$description")]
    public string Description { get; set; }

    [JsonPropertyName("$validationMode")]
    public ValidationMode ValidationMode { get; set; }
    
    [JsonPropertyName("$validationItems")]
    public IEnumerable<ValidationConfigurableJsonItem<T>> ValidationItems { get; set; }

    [JsonPropertyName("$validationConditions")]
    public IEnumerable<ValidationConfigurableJsonCondition<T>> ValidationConditions { get; set; }
    


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
        if (isConfigured)
        {
            return;
        }

        ConfigureValidationItems();
        ConfigureValidationConditions();

        isConfigured = true; // Let's set this so some idiot doesn't try to call this more than once
    }

    private void ConfigureValidationConditions()
    {
        // Need to push the conditional validation items into the current validation stack
        foreach (var validationCondition in this.ValidationConditions)
        {
            var condition = validationCondition.GetCondition();

            foreach (var validationItem in validationCondition.ValidationItems)
            {
                validationItem.ConfigureItemMember();
                validationItem.ConfigureItemCondition(condition);
                validationItem.ConfigureItemValidationMode(this.ValidationMode);
                validationItem.ConfigureErrorDefaults();
                validationItem.ConfigureRuleValueTypeConversion();
            }
        }
    }

    private void ConfigureValidationItems()
    {
        foreach (var validationItem in this.ValidationItems)
        {
            validationItem.ConfigureItemMember();
            validationItem.ConfigureItemValidationMode(this.ValidationMode);
            validationItem.ConfigureErrorDefaults();
            validationItem.ConfigureRuleValueTypeConversion();
        }
    }
}
