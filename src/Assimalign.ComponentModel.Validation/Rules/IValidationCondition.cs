using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Rules
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidationCondition<T> : IValidationRule<T>
    {

        /// <summary>
        /// 
        /// </summary>
        IEnumerable<IValidationRule<T>> Rules { get; set; }
    }
}
