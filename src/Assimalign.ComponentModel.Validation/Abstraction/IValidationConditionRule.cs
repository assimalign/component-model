using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace Assimalign.ComponentModel.Validation.Abstraction
{
    /// <summary>
    /// Represents a validation rule for which other validation rules should be applied. 
    /// </summary>
    /// <remarks>
    /// The logical flow is best described as if (Condition is true/false) then { Run Validation Rules }
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    public interface IValidationConditionRule<T> : IValidationRule
    {
        /// <summary>
        /// The Condition in which the child validation rule collection should be applied.
        /// </summary>
        Expression<Func<T, bool>> Condition { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IValidationRuleSet ConditionRuleSet { get; }

        /// <summary>
        /// The rule set to run if condition is not valid.
        /// </summary>
        IValidationRuleSet DefaultRuleSet { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition">What condition is required</param>
        /// <param name="configure">The validation to </param>
        /// <returns></returns>
        IValidationConditionRule<T> When(Expression<Func<T, bool>> condition, Action<IValidationRuleInitializer<T>> configure);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configure"></param>
        void Otherwise(Action<IValidationRuleInitializer<T>> configure);
    }
}
