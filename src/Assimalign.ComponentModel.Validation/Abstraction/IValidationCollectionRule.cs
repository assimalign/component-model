using System;
using System.Collections;
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
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TCollection"></typeparam>
    public interface IValidationCollectionRule<T, TCollection> : IValidationRule 
        where TCollection : IEnumerable
    {
        /// <summary>
        /// 
        /// </summary>
        Expression<Func<T, TCollection>> Collection { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IValidationRuleSet Rules { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rule"></param>
        void AddRule(IValidationRule rule);
    }
}
