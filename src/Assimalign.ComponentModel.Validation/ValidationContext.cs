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
    public sealed class ValidationContext<T> : IValidationContext
    {
        private readonly Stack<ValidationError> errors = new Stack<ValidationError>();
        private readonly IList<IValidationRule> successes = new List<IValidationRule>();

        /// <summary>
        /// 
        /// </summary>
        public ValidationContext(T instance)
        {
           
        }

        /// <summary>
        /// 
        /// </summary>
        public object ValidationInstance { get; }
        
        /// <summary>
        /// A collection of validation failures that occurred.
        /// </summary>
        public IEnumerable<ValidationError> Errors => errors;

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<IValidationRule> Successes => successes;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="error"></param>
        public void AddFailure(ValidationError error) => errors.Push(error);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="failureMessage"></param>
        public void AddFailure(string failureMessage)
        {
            errors.Push(new ValidationError()
            {
                Message = failureMessage
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="failureMessage"></param>
        public void AddFailure(string propertyName, string failureMessage)
        {
            throw new NotImplementedException();
        }
    }
}
