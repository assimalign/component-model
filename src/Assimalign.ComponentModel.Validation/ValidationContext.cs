using Assimalign.ComponentModel.Validation.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation
{
    using Assimalign.ComponentModel.Validation.Abstraction;
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ValidationContext<T> : IValidationContext
    {
        private readonly Stack<IValidationError> errors = new Stack<IValidationError>();
        private readonly IList<IValidationRule> successes = new List<IValidationRule>();

        /// <summary>
        /// 
        /// </summary>
        public ValidationContext(T instance)
        {
            if (instance is null)
            {
                throw new ArgumentNullException(nameof(instance));
            }
            ValidationInstance = instance;
        }

        /// <summary>
        /// 
        /// </summary>
        public object ValidationInstance { get; }
        
        /// <summary>
        /// A collection of validation failures that occurred.
        /// </summary>
        public IEnumerable<IValidationError> Errors => errors;

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<IValidationRule> Successes => successes;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="error"></param>
        public void AddFailure(IValidationError error) => errors.Push(new ValidationError(error));

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
        /// <param name="failureSource"></param>
        /// <param name="failureMessage"></param>
        public void AddFailure(string failureSource, string failureMessage)
        {
            errors.Push(new ValidationError()
            {
                Message = failureMessage,
                Source = failureSource
            });
        }
    }
}
