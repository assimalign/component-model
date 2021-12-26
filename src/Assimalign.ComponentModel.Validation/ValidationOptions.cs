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
        private readonly IList<IValidationRule> rules;
        
            
        internal IDictionary<int, IValidationProfile> Profiles { get; } = new Dictionary<int, IValidationProfile>();


        public ValidationOptions()
        {
            this.rules = new List<IValidationRule>();
            //this.profiles = new Dictionary<int, IValidationProfile>();
        }

        


        /// <summary>
        /// The registered profiles for the 
        /// </summary>
        //public IEnumerable<IValidationProfile> Profiles => this.profiles.Values;

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<IValidationRule> ValidationRules => rules;

        /// <summary>
        /// 
        /// </summary>
        public bool ProfileNameCaseSensitive { get; set; }

        /// <summary>
        /// Will throw a <see cref="ValidationException"/> rather than return <see cref="ValidationResult"/>.
        /// </summary>
        public bool ThrowExceptionOnFailure { get; set; }








        /// <summary>
        /// Register an additional Validator not supported within the library.
        /// </summary>
        /// <typeparam name="TValidatorRule"></typeparam>
        public void AddRule<TValidatorRule>()
            where TValidatorRule : IValidationRule, new()
        {
            rules.Add(new TValidatorRule());
        }


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

            var index = HashCode.Combine(profile.Name, profile.ValidationType);

            Profiles[index] = profile;
        }
    }
}
