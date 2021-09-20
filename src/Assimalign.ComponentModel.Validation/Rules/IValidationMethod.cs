using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Rules
{
    /// <summary>
    /// Evaluate Method returns at runtime.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidationMethod<T> : IValidationRule<T>
    {
        /// <summary>
        /// 
        /// </summary>
        string Method { get; set; }
    }
}
