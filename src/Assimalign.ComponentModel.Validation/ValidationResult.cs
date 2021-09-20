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
        public IEnumerable<ValidationError> Errors { get; internal set; }


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
        public static ValidationResult Create<T>(IValidatorContext<T> context)
        {
            return new ValidationResult()
            {
                Errors = context.Errors,
                RulesInvoked = context.RulesInvoked.Select(x=>x.Name),
            };
        }
    }
}
