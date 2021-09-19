using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation
{
    public sealed class ValidationResult
    {
        /// <summary>
        /// 
        /// </summary>
        internal ValidationResult() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errors"></param>
        internal ValidationResult(IEnumerable<ValidationError> errors)
        {
            this.Errors = errors;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsValid => Errors.Count() == 0;


        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<ValidationError> Errors { get; }
    }
}
