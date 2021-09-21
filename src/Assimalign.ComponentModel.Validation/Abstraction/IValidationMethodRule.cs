using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation
{
    /// <summary>
    /// Evaluate Method returns at runtime.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidationMethodRule<T> : IValidationRule
    {
        /// <summary>
        /// 
        /// </summary>
        MethodInfo Method { get; set; }
    }
}
