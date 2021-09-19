using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation
{
    using Assimalign.ComponentModel.Validation.Rules;

    public interface IValidatorContext<T>
    {
        /// <summary>
        /// 
        /// </summary>
        IEnumerable<ValidationError> Errors {  get; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="error"></param>
        void AddError(ValidationError error);
        
        
    }
}
