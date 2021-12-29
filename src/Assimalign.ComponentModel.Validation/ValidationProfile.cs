﻿using System;
using System.Collections.Generic;


namespace Assimalign.ComponentModel.Validation;

using Assimalign.ComponentModel.Validation.Internal;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class ValidationProfile<T> : IValidationProfile<T>
{
    private readonly Type validationType;
    private readonly IList<IValidationItem> validationItems;

    /// <summary>
    /// 
    /// </summary>
    public ValidationProfile()
    {
        this.validationType = typeof(T);
        this.validationItems = new List<IValidationItem>();
    }

    /// <summary>
    /// A collection validation rules to apply to the instance of <typeparamref name="T"/>
    /// for a given context.
    /// </summary>
    public IEnumerable<IValidationItem> ValidationItems => this.validationItems;

    /// <summary>
    /// The type of <typeparamref name="T"/> being validated.
    /// </summary>
    public Type ValidationType => this.validationType;

    /// <summary>
    /// 
    /// </summary>
    public ValidationMode ValidationMode { get; set; } = ValidationMode.Continue;

    /// <summary>
    /// 
    /// </summary>
    void IValidationProfile.Configure()
    {
        this.Configure(new ValidationRuleDescriptor<T>()
        {
            ValidationRules = this.ValidationRules,
            ValidationMode = this.ValidationMode
        });
    }

    /// <summary>
    /// Configures the validation rules to be applied on the type <typeparamref name="T"/>
    /// </summary>
    /// <param name="descriptor"></param>
    public abstract void Configure(IValidationRuleDescriptor<T> descriptor);
}


/// <summary>
/// 
/// </summary>
public abstract class ValidationProfile : IValidationProfile
{
    private readonly Type type;
    private readonly IValidationRuleStack rules;

    /// <summary>
    /// 
    /// </summary>
    public ValidationProfile(Type type)
    {
        this.type = type;
        this.rules = new ValidationRuleStack();
    }

    /// <summary>
    /// 
    /// </summary>
    public Type ValidationType => this.type;

    /// <summary>
    /// 
    /// </summary>
    public IValidationRuleStack ValidationRules => this.rules;

    /// <summary>
    /// 
    /// </summary>
    public ValidationMode ValidationMode { get; set; } = ValidationMode.Continue;


    /// <summary>
    /// 
    /// </summary>
    public abstract void Configure();
}