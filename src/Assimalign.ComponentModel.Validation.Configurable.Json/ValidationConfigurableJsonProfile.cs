using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Configurable;

internal class ValidationConfigurableJsonProfile<T> : IValidationProfile
{
    private IList<ValidationConfigurableJsonItem<T>> validationItems;

    [JsonConstructor]
    public ValidationConfigurableJsonProfile()
    {
        this.ValidationType = typeof(T);
    }

    [JsonIgnore]
    public Type ValidationType { get; }

    [JsonPropertyName("$validationMode")]
    public ValidationMode ValidationMode { get; set; }

    [JsonPropertyName("$description")]
    public string Description { get; set; }
    
    [JsonPropertyName("$validationItems")]
    public IEnumerable<IValidationItem> ValidationItems
    {
        get => this.validationItems;
        set
        {
            var list = new List<ValidationConfigurableJsonItem<T>>();

            foreach (var item in value)
            {
                if (item is ValidationConfigurableJsonItem<T> jsonItem)
                {
                    list.Add(jsonItem);
                }
                else
                {
                    throw new Exception("Invalid Configuration Type");
                }
            }

            this.validationItems = list;
        }
    }

    [JsonIgnore]
    [JsonPropertyName("$validationConditions")]
    public IEnumerable<IValidationCondition> ValidationConditions { get; set; }


    public void Configure()
    {
        var parameterExpression = Expression.Parameter(ValidationType, ValidationType.Name);

        foreach (var configValidationItem in this.validationItems)
        {
            var memberPaths = configValidationItem.ItemMember.Split('.');
            var memberExpression = (Expression)parameterExpression;

            for (int i = 0; i < memberPaths.Length; i++)
            {
                memberExpression = Expression.Property(memberExpression, memberPaths[i]);
            }

            var lambdaExpression = Expression.Lambda<Func<T, object>>(memberExpression, parameterExpression);

            configValidationItem.ItemExpression = lambdaExpression;
        }
    }
}
