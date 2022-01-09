using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Configurable;

internal class ValidationConfigurableJsonProfile<T> : IValidationProfile
{
    [JsonConstructor]
    public ValidationConfigurableJsonProfile()
    {
        this.ValidationType = typeof(T);
        this.ValidationConditions ??= new List<ValidationConfigurableJsonCondition<T>>();
        this.ValidationItems ??= new List<ValidationConfigurableJsonItem<T>>();
    }

    

    [JsonPropertyName("$validationMode")]
    public ValidationMode ValidationMode { get; set; }

    [JsonPropertyName("$description")]
    public string Description { get; set; }
    
    [JsonPropertyName("$validationItems")]
    public IList<ValidationConfigurableJsonItem<T>> ValidationItems { get; set; }

    [JsonIgnore]
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
        var parameterExpression = Expression.Parameter(ValidationType, ValidationType.Name);

        foreach (var validationItem in this.ValidationItems)
        {

        }

        // Need to push the conditional validation items into the current validation stack
        foreach (var validationCondition in this.ValidationConditions)
        {
            var condition = validationCondition.GetCondition();

            foreach (var validationItem in validationCondition.ValidationItems)
            {
                var memberPaths = validationItem.ItemMember.Split('.');
                var memberExpression = (Expression)parameterExpression;

                for (int i = 0; i < memberPaths.Length; i++)
                {
                    memberExpression = Expression.Property(memberExpression, memberPaths[i]);
                }

                var lambdaExpression = Expression.Lambda<Func<T, object>>(memberExpression, parameterExpression);

                validationItem.ItemCondition = condition;
                validationItem.ItemExpression = lambdaExpression;

                this.ValidationItems.Add(validationItem);
            }
        }
    }
}
