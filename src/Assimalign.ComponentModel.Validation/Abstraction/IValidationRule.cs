using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Assimalign.ComponentModel.Validation.Abstraction
{

    /// <summary>
    /// 
    /// </summary>
    public interface IValidationRule
    {
        /// <summary>
        /// A unique name for the rule to evaluate.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        string Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        void Evaluate(IValidationContext context);
    }
}
