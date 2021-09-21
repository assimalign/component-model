using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidationMemberRule<T> : IValidationRule
    {

        /// <summary>
        /// The member being evaluated for the rule
        /// </summary>
        MemberExpression Member { get; }

        /// <summary>
        /// 
        /// </summary>
        LambdaExpression MemberDelegate { get; set; }

        /// <summary>
        /// The validation rules to apply to the member.
        /// </summary>
        IEnumerable<IValidationRule> Rules { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rule"></param>
        void AddMemberRule(IValidationRule rule);

    }
}
