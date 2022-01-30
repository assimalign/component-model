using System;
using System.Diagnostics;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Assimalign.ComponentModel.Validation.Configurable;


/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class ValidationConfigurableJsonItem<T> : IValidationItem
    where T : class
{
    private ValidationMode validationMode;
    private Func<T, bool> itemCondition;
    private Func<T, object> itemMember;
    private Expression<Func<T, object>> itemMemberExpression;
    IValidationRuleStack IValidationItem.ItemRuleStack => new ValidationRuleStack(this.ItemRuleStack);


    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("$itemMember")]
    public string ItemMember { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("$itemType")]
    public ValidationConfigurableItemType ItemType { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("$itemRules")]
    public Stack<ValidationConfigurableJsonRule<T>> ItemRuleStack { get; set; }
   


    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public  void Evaluate(IValidationContext context)
    {
        if (context.Instance is T instance)
        {
            // 1. Lets check if there is a condition and if so let's see.
            //    if this validation item can be evaluated
            if (this.itemCondition is not null && !this.itemCondition.Invoke(instance))
            {
                return;
            }
            if (this.ItemType == ValidationConfigurableItemType.Inline)
            {
                EvaluateInline(instance, context);
                return;
            }
            if (this.ItemType == ValidationConfigurableItemType.Recursive)
            {
                EvaluateRecursive(instance, context);
                return;
            }
        }
    }
    private void EvaluateInline(T instance, IValidationContext context)
    {
        var stopwatch = new Stopwatch();
        var value = this.GetMemberValue(instance);

        foreach (var rule in ItemRuleStack)
        {
            if (this.validationMode == ValidationMode.Stop && context.Errors.Any())
            {
                return;
            }

            stopwatch.Start();

            if (rule.TryValidate(value, out var validationContext))
            {
                foreach (var error in validationContext.Errors)
                {
                    context.AddFailure(error);
                }

                stopwatch.Stop();
                context.AddInvocation(new ValidationInvocation(rule.Name, true, stopwatch.ElapsedTicks));
            }
            else
            {
                stopwatch.Stop();
                context.AddInvocation(new ValidationInvocation(rule.Name, false, stopwatch.ElapsedTicks));
            }

            stopwatch.Reset();
        }
    }
    private void EvaluateRecursive(T instance, IValidationContext context)
    {
        var value = this.GetMemberValue(instance);

        if (value is IEnumerable enumerable)
        {
            foreach (var item in enumerable)
            {
                var stopwatch = new Stopwatch();

                foreach (var rule in ItemRuleStack)
                {
                    if (this.validationMode == ValidationMode.Stop && context.Errors.Any())
                    {
                        return;
                    }

                    stopwatch.Start();

                    if (rule.TryValidate(item, out var validationContext))
                    {
                        foreach (var error in validationContext.Errors)
                        {
                            context.AddFailure(error);
                        }

                        stopwatch.Stop();
                        context.AddInvocation(new ValidationInvocation(rule.Name, true, stopwatch.ElapsedTicks));
                    }
                    else
                    {
                        stopwatch.Stop();
                        context.AddInvocation(new ValidationInvocation(rule.Name, false, stopwatch.ElapsedTicks));
                    }

                    stopwatch.Reset();
                }
            }
        }
    }
    private object GetMemberValue(T instance) // Let's safely get the member value encase of null reference exception
    {
        try
        {
            return this.itemMember.Invoke(instance);
        }
        catch
        {
            return null;
        }
    }
    internal void Configure(params object[] parameters)
    {
        foreach (var parameter in parameters)
        {
            if (parameter is ValidationMode validationMode)
            {
                this.validationMode = validationMode;
            }
            if (parameter is Expression<Func<T, bool>> itemCondition)
            {
                this.itemCondition = itemCondition.Compile();
            }
        }

        if (this.itemMemberExpression is null)
        {
            var parameterExpression = Expression.Parameter(typeof(T));
            var memberPaths = this.ItemMember.Split('.');
            var memberExpression = (Expression)parameterExpression;

            for (int i = 0; i < memberPaths.Length; i++)
            {
                memberExpression = Expression.Property(memberExpression, memberPaths[i]);
            }

            this.itemMemberExpression = Expression.Lambda<Func<T, object>>(memberExpression, parameterExpression);
            this.itemMember = itemMemberExpression.Compile();


            foreach (var rule in this.ItemRuleStack)
            {
                rule.Configure(itemMemberExpression, ItemType, validationMode);
            }
        }
    }
}