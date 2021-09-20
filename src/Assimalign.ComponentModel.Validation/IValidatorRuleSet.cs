using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation
{
    using Assimalign.ComponentModel.Validation.Rules;


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidatorRuleSet<T> : ICollection<IValidationRule<T>>, IList<IValidationRule<T>>
    {
        /// <summary>
        /// 
        /// </summary>
        void Evaluate(IValidatorContext<T> context, T instance);

    }
}
