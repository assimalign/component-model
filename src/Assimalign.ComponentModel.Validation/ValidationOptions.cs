using System;
using System.Collections.Generic;


namespace Assimalign.ComponentModel.Validation;


/// <summary>
/// 
/// </summary>
public sealed class ValidationOptions
{
    private readonly SortedList<int, IValidationRule> rules;
    private readonly IDictionary<int, IValidationProfile> profiles;

    /// <summary>
    /// 
    /// </summary>
    public ValidationOptions()
    {
        this.rules = new SortedList<int, IValidationRule>();
        this.profiles = new Dictionary<int, IValidationProfile>();
    }


    /// <summary>
    /// Will throw a <see cref="ValidationFailureException"/> rather than return <see cref="ValidationResult"/>.
    /// </summary>
    public bool ThrowExceptionOnFailure { get; set; }


    ///// <summary>
    ///// 
    ///// </summary>
    // public int ValidationFailureLimit { get; set; }

    /// <summary>
    /// The collection of profiles.
    /// </summary>
    public IEnumerable<IValidationProfile> Profiles => this.profiles.Values;

    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    /// <param name="profile"></param>
    public void AddProfile(IValidationProfile profile)
    {
        if (profile is null)
        {
            throw new ArgumentNullException(nameof(profile));
        }

        var index = HashCode.Combine(profile.ValidationType);

        profile.Configure();

        if (this.profiles.ContainsKey(index))
        {
            throw new InvalidOperationException($"A Validation Profile for type: {profile.GetType().Name} has already been registered. " +
                $"If needing to implement two different validation profiles for type {profile.GetType().Name} then use the ValidationFacotry.");
        }

        this.profiles[index] = profile;
    }

    internal bool TryGetProfile(int index, out IValidationProfile profile)
    {
        return this.profiles.TryGetValue(index, out profile);
    }
}

