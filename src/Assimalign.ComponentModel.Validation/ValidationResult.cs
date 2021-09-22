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
    public sealed class ValidationResult
    {
        /// <summary>
        /// 
        /// </summary>
        internal ValidationResult() { }

        /// <summary>
        /// 
        /// </summary>
        public bool IsValid => Errors.Count() == 0;


        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<IValidationError> Errors { get; internal set; }


        /// <summary>
        /// A collection of rules failed evaluation 
        /// </summary>
        public IEnumerable<string> RulesInvoked { get; internal set; }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        public static ValidationResult Create(IValidationContext context)
        {
            return new ValidationResult()
            {
                Errors = context.Errors,
                //RulesInvoked = context.Successes.Select(x=>x.Name),
            };
        }
    }
}
