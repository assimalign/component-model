using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidationConditionRule<T> : IValidationRule
    {
        /// <summary>
        /// 
        /// </summary>
        Func<T, bool> Condition { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IEnumerable<IValidationRule> Rules { get; set; }
    }
}
