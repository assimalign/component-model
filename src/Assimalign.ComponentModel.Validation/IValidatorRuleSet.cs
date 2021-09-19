using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation
{
    using Assimalign.ComponentModel.Validation.Rules;


    public interface IValidatorRuleSet<T> : ICollection<IValidationRule<T>>, IList<IValidationRule<T>>
    {
        /// <summary>
        /// 
        /// </summary>
        Func<T, bool> RunWhen { get; }

    }
}
