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
        Func<T, bool> Condition { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IValidationRuleSet Rules { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TMember">A member expression is either a field or property of a Type.</typeparam>
        /// <param name="expression"></param>
        IValidationMemberRule<T, TMember> RuleFor<TMember>(Expression<Func<T, TMember>> expression);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCollection"></typeparam>
        /// <param name="expression"></param>
        IValidationCollectionRule<T, TCollection> RuleForEach<TCollection>(Expression<Func<T, TCollection>> expression)
            where TCollection : IEnumerable;
    }
}
