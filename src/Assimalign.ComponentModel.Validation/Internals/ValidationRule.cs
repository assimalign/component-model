using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation
{
    using Assimalign.ComponentModel.Validation.Exceptions;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal abstract class ValidationRule<T> : IValidationRule
    {

        /// <summary>
        /// 
        /// </summary>
        protected ValidationRule()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Validator { get; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Func<T, bool> Validation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public virtual void Evaluate(IValidationContext context)
        {
            
        }
    }
}
