using System;
using System.Collections;
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
    /// <typeparam name="TCollection"></typeparam>
    public interface IValidationCollectionRule<T, TCollection> : IValidationRule 
        where TCollection : IEnumerable
    {
        /// <summary>
        /// 
        /// </summary>
        Expression<Func<T, IEnumerable<TCollection>>> Delegate { get; set; }

        /// <summary>
        /// The validation rules to apply to the member.
        /// </summary>
        IEnumerable<IValidationRule> Rules { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rule"></param>
        void AddRule(IValidationRule rule);
    }
}
