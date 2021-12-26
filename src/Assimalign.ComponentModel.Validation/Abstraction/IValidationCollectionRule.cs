using System;
using System.Collections;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Validation;

/// <summary>
/// A rule for validating each value in a collection.
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
    /// The set of validation rules to apply to each value in the collection.
    /// </summary>
    IValidationRuleStack CollectionRules { get; }


    /// <summary>
    /// A collection validator to be used on top of any additional rules  
    /// </summary>
    //IValidator<TCollection> Validator { get; }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="rule"></param>
    IValidationCollectionRule<T, TCollection> AddRule(IValidationRule rule);
}

