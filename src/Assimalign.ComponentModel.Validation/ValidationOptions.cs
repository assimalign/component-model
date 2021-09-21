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
        private IList<IValidationRule> rules = new List<IValidationRule>();




        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<IValidationRule> RegisteredRules => rules;





        /// <summary>
        /// Register an additional Validator not supported within the library.
        /// </summary>
        /// <typeparam name="TValidatorRule"></typeparam>
        public void RegisterRule<TValidatorRule>()
            where TValidatorRule : IValidationRule, new()
        {
            rules.Add(new TValidatorRule());
        }



    }
}
