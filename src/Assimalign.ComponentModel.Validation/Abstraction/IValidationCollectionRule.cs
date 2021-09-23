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
        /// A collection validator to be used on top of any additional rules  
        /// </summary>
        IValidator<TCollection> Validator { get; }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// IValidationCollectionRule<T, TCollection> OneOf(Func<IValidationCollectionRule<T, TCollection>, IEnumerable<IValidationCollectionRule<T, TCollection>>> evaluations);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IValidationCollectionRule<T, TCollection> Null();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IValidationCollectionRule<T, TCollection> NotNull();

        /// <summary>
        /// Validates whether the member is not empty.
        /// </summary>
        /// <returns></returns>
        IValidationCollectionRule<T, TCollection> NotEmpty();

        /// <summary>
        /// Validates whether the member is empty.
        /// </summary>
        /// <returns></returns>
        IValidationCollectionRule<T, TCollection> Empty();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TValue">is of </typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        IValidationCollectionRule<T, TCollection> GreaterThan<TValue>(TValue value) where TValue : struct;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        IValidationCollectionRule<T, TCollection> GreaterThanOrEqualTo<TValue>(TValue value) where TValue : struct;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        IValidationCollectionRule<T, TCollection> LessThan<TValue>(TValue value) where TValue : struct;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        IValidationCollectionRule<T, TCollection> LessThanOrEqualTo<TValue>(TValue value) where TValue : struct;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        IValidationCollectionRule<T, TCollection> Equal<TValue>(TValue value) where TValue : IComparable;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        IValidationCollectionRule<T, TCollection> NotEqual<TValue>(TValue value) where TValue : IComparable;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="validator"></param>
        /// <returns></returns>
        IValidationCollectionRule<T, TCollection> UseValidator(IValidator<TCollection> validator);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rule"></param>
        IValidationCollectionRule<T, TCollection> AddRule(IValidationRule rule);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TLeftBound"></typeparam>
        /// <typeparam name="TRightBound"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        IValidationMemberRule<T, TCollection> Between<TLeftBound, TRightBound>(TLeftBound left, TRightBound right)
            where TLeftBound : struct
            where TRightBound : struct;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TLeftBound"></typeparam>
        /// <typeparam name="TRightBound"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        IValidationMemberRule<T, TCollection> BetweenOrEqualTo<TLeftBound, TRightBound>(TLeftBound left, TRightBound right)
            where TLeftBound : struct
            where TRightBound : struct;
    }
}
