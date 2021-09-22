using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Abstraction
{
    /// <summary>
    /// 
    /// </summary>
    public interface IValidationContext
    {
        /// <summary>
        /// The instance to validate.
        /// </summary>
        object ValidationInstance { get; }

        /// <summary>
        /// 
        /// </summary>
        IEnumerable<IValidationRule> Successes { get; }

        /// <summary>
        /// 
        /// </summary>
        IEnumerable<ValidationError> Errors { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="error"></param>
        void AddFailure(ValidationError error);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="failureMessage"></param>
        void AddFailure(string failureMessage);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="failureMessage"></param>
        void AddFailure(string propertyName, string failureMessage);

    }
}
