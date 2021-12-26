using System;

namespace Assimalign.ComponentModel.Validation;

using Assimalign.ComponentModel.Validation.Internal;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class ValidationProfile<T> : IValidationProfile<T>
{
    private readonly Type validationType;
    private readonly string validationName;
    private readonly IValidationRuleStack validationRules;

    /// <summary>
    /// 
    /// </summary>
    public ValidationProfile(string profileName = nameof(T))
    {
        this.validationType = typeof(T);
        this.validationName = profileName;
        this.validationRules = new ValidationRuleStack();
    }

    /// <summary>
    /// The name of the Validation Profile. Useful for separating different validation profiles for the same types.
    /// </summary>
    public string Name => this.validationName;

    /// <summary>
    /// A collection validation rules to apply to the instance of <typeparamref name="T"/>
    /// for a given context.
    /// </summary>
    public IValidationRuleStack ValidationRules => this.validationRules;

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
            ValidationRules = this.ValidationRules
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
    private readonly string name;
    private readonly IValidationRuleStack rules;

    /// <summary>
    /// 
    /// </summary>
    public ValidationProfile(string name, Type type)
    {
        this.name = name;
        this.type = type;
        this.rules = new ValidationRuleStack();
    }

    /// <summary>
    /// 
    /// </summary>
    public string Name => this.name;

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

