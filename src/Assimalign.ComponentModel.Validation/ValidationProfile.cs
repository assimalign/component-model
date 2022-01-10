using System;
using System.Collections.Generic;


namespace Assimalign.ComponentModel.Validation;

using Assimalign.ComponentModel.Validation.Internal;

/// <summary>
/// A validation profile is used to describe the rules of <typeparamref name="T"/>.
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
    /// A flag indicating whether to cascade through all validation 
    /// rules after first item failure. <b>The default is 'Cascade'</b>
    /// </summary>
    public ValidationMode ValidationMode { get; set; } = ValidationMode.Cascade;

    /// <summary>
    /// 
    /// </summary>
    void IValidationProfile.Configure()
    {
        this.Configure(new ValidationRuleDescriptor<T>()
        {
            ValidationItems = this.ValidationItems as IList<IValidationItem>
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
    private readonly Type validationType;
    private readonly IList<IValidationItem> validationItems;

    /// <summary>
    /// 
    /// </summary>
    public ValidationProfile(Type type)
    {
        this.validationType = type;
        this.validationItems = new List<IValidationItem>();
    }

    /// <summary>
    /// 
    /// </summary>
    public Type ValidationType => this.validationType;

    /// <summary>
    /// A flag indicating whether to cascade through all validation rules after first failure.
    /// </summary>
    public ValidationMode ValidationMode { get; set; } = ValidationMode.Cascade;

    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<IValidationItem> ValidationItems => this.validationItems;

    /// <summary>
    /// 
    /// </summary>
    public abstract void Configure();
}