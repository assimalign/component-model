using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Configurable;

internal class ValidationConfigJsonProfile<T> : IValidationProfile
    where T : class
{
    private IList<ValidationConfigJsonItem<T>> validationItems;

    public ValidationConfigJsonProfile()
    {
        this.ValidationType = typeof(T);
    }


    [JsonPropertyName("$description")]
    public string Description { get; set; }

    [JsonPropertyName("$validationMode")]
    public ValidationMode ValidationMode { get; set; }

    [JsonPropertyName("$validationConditions")]
    public IEnumerable<ValidationConfigJsonCondition> Conditions { get; set; }

    [JsonPropertyName("$validationItems")]
    public IEnumerable<ValidationConfigJsonItem<T>> ValidationItems
    {
        get => this.validationItems;
        set => this.validationItems = value.ToList();
    }
    

    [JsonIgnore]
    public Type ValidationType { get; }

    [JsonIgnore]
    IEnumerable<IValidationItem> IValidationProfile.ValidationItems => this.validationItems;

    public void Configure()
    {
        var parameterExpression = Expression.Parameter(typeof(T), "x");
        
        foreach(var validationItem in this.ValidationItems)
        {
            var memberPaths = validationItem.ItemMember.Split('.');
            var memberExpression = (Expression)parameterExpression;
            
            for (int i = 0; i < memberPaths.Length; i++)
            {
                memberExpression = Expression.Property(memberExpression, memberPaths[i]);
            }

            var lambdaExpression = Expression.Lambda<Func<T, object>>(memberExpression, parameterExpression);

            validationItem.ItemExpression = lambdaExpression;

            foreach (var validationItemRule in validationItem.ItemRules)
            {

            }


            validationItems.Add(validationItem);
        }
    }
}