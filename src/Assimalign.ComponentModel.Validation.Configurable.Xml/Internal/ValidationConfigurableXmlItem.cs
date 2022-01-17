﻿using System;
using System.Diagnostics;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Assimalign.ComponentModel.Validation.Configurable;

using Assimalign.ComponentModel.Validation.Internal.Extensions;
using Assimalign.ComponentModel.Validation.Properties;
using Assimalign.ComponentModel.Validation.Configurable.Serialization;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class ValidationConfigurableXmlItem<T> : IValidationItem
{
    private Func<T, bool> itemCondition;
    private Func<T, object> itemMember;
    private Expression<Func<T, object>> itemMemberExpression;
    private ValidationMode validationMode;


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
    IValidationRuleStack IValidationItem.ItemRuleStack => new ValidationRuleStack(this.ItemRuleStack);


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
                foreach (var rule in ItemRuleStack)
                {
                    if (rule.TryValidate(item, out var validationContext))
                    {
                        foreach (var error in validationContext.Errors)
                        {
                            context.AddFailure(error);
                        }
                    }
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

            if (this.itemMemberExpression.Body.Type.IsSystemValueType(out var valueType))
            {
                foreach (var rule in this.ItemRuleStack)
                {
                    rule.Configure(valueType, itemMemberExpression);
                }
            }
            if (this.itemMemberExpression.Body.Type.IsEnumerableType(out var enumerableType))
            {
                foreach (var rule in this.ItemRuleStack)
                {
                    rule.Configure(enumerableType, itemMemberExpression);
                }
            }
        }
    }
}