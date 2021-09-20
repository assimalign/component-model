using Assimalign.ComponentModel.Validation.Rules;
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
    /// <typeparam name="T"></typeparam>
    public sealed class ValidatorContext<T> : IValidatorContext<T>
    {
        private readonly IList<ValidationError> errors = new List<ValidationError>();
        private readonly IList<IValidationRule<T>> invokedRules = new List<IValidationRule<T>>();

        /// <summary>
        /// 
        /// </summary>
        public ValidatorContext()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<ValidationError> Errors => errors;

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<IValidationRule<T>> RulesInvoked => invokedRules;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="error"></param>
        public void AddError(ValidationError error) => errors.Add(error);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="rule"></param>
        public void AddFailedRule(IValidationRule<T> rule)
        {
            throw new NotImplementedException();
        }
    }
}
