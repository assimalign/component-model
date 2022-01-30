using System;
using System.Collections.Generic;

namespace Assimalign.ComponentModel.Validation;

/// <summary>
/// 
/// </summary>
public sealed class ValidationOptions
{
    private readonly IDictionary<int, IValidationProfile> profiles;

    /// <summary>
    /// Default constructor for instantiating <see cref="ValidationOptions"/>.
    /// </summary>
    public ValidationOptions()
    {
        this.profiles = new Dictionary<int, IValidationProfile>();
    }


    /// <summary>
    /// Will throw a <see cref="ValidationFailureException"/> rather 
    /// than return <see cref="ValidationResult"/>.
    /// </summary>
    public bool ThrowExceptionOnFailure { get; set; }

    /// <summary>
    /// By default, when more then one rule is chained to a validation item
    /// the first failure will exit the chain. Set this property to true if 
    /// the desired behavior is to iterate through all rules in the validation chain.
    /// <br/>
    /// <br/>
    /// <example>
    /// <b>An example of default behavior:</b>
    /// <code>
    /// RuleFor(p => p.Property)
    ///       .NotNull()     // If this Rule Fails
    ///       .NotEmpty()    // Then this one will not run  
    /// </code>
    /// </example>
    /// </summary>
    public bool ContinueThroughValidationChain { get; set; }

    /// <summary>
    /// The collection of <see cref="IValidationProfile"/>.
    /// </summary>
    public IEnumerable<IValidationProfile> Profiles => this.profiles.Values;

    /// <summary>
    /// Adds a <see cref="IValidationProfile"/> to the collection of profiles 
    /// which will be used for an <see cref="IValidator"/> instance.
    /// </summary>
    /// <typeparam name="TValidationProfile"><see cref="IValidationProfile"/></typeparam>
    public ValidationOptions AddProfile<TValidationProfile>() where TValidationProfile : IValidationProfile, new()
    {
        return this.AddProfile(new TValidationProfile());
    }

    /// <summary>
    /// Adds a <see cref="IValidationProfile"/> to the collection of profiles 
    /// which will be used for an <see cref="IValidator"/> instance.
    /// </summary>
    /// <remarks></remarks>
    /// <param name="profile"></param>
    public ValidationOptions AddProfile(IValidationProfile profile)
    {
        if (profile is null)
        {
            throw new ArgumentNullException(nameof(profile));
        }

        var index = profile.ValidationType.GetHashCode();

        if (this.profiles.ContainsKey(index))
        {
            throw new InvalidOperationException($"A Validation Profile for type: {profile.GetType().Name} has already been registered.");
        }

        profile.Configure();

        this.profiles[index] = profile;

        return this;
    }
}

