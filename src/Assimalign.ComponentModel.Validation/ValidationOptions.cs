using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ValidationOptions
    {
        private readonly SortedList<int, IValidationRule> rules;
        private readonly IDictionary<int, IValidationProfile> profiles; 


        public ValidationOptions()
        {
            this.rules = new SortedList<int, IValidationRule>();
            this.profiles = new Dictionary<int, IValidationProfile>();
        }


        /// <summary>
        /// Will throw a <see cref="ValidationFailureException"/> rather than return <see cref="ValidationResult"/>.
        /// </summary>
        public bool ThrowExceptionOnFailure { get; set; }

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

            profile.Configure();

            var index = HashCode.Combine(profile.Name.ToLower(), profile.ValidationType);

            this.profiles[index] = profile;
        }

        internal bool TryGetProfile(int index, out IValidationProfile profile)
        {
            return this.profiles.TryGetValue(index, out profile);
        }
    }
}
