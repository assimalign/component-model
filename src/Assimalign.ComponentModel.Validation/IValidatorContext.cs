using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation
{
    using Assimalign.ComponentModel.Validation.Rules;


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidatorContext<T>
    {
       /// <summary>
       /// 
       /// </summary>
        IEnumerable<IValidationRule<T>> RulesInvoked { get; }

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
        /// <param name="message"></param>
        void AddMessage(string message);

    }
}
