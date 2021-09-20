using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Validation.Rules
{
    /// <summary>
    /// A validation rule is a predicate which evaluates true or false.
    /// </summary>
    public interface IValidationRule<T>
    {
        /// <summary>
        /// The validator which executed the rule.
        /// </summary>
        string Validator { get; }

        /// <summary>
        /// A unique name for the rule to evaluate.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The predicate which evaluates the rule.
        /// </summary>
        LambdaExpression Validation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="instance"></param>
        void Evaluate(IValidatorContext<T> context, T instance);
    }
}
