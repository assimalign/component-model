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
    /// <typeparam name="TValue"></typeparam>
    public interface IValidationRuleBuilder<T, TValue>
    {


        /// <summary>
        /// The Rule being configured for the instance if the validation rule initializer.
        /// </summary>
        IValidationRule Current { get; }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// IValidationCollectionRule<T, TCollection> OneOf(Func<IValidationCollectionRule<T, TCollection>, IEnumerable<IValidationCollectionRule<T, TCollection>>> evaluations);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="validation"></param>
        /// <returns></returns>
        IValidationRuleBuilder<T, TValue> Custom(Action<TValue, IValidationContext> validation);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        IValidationRuleBuilder<T, TValue> Length(int min, int max);

        /// <summary>
        /// Requires the string member to be of an exact length.
        /// </summary>
        /// <param name="exact"></param>
        /// <returns></returns>
        IValidationRuleBuilder<T, TValue> Length(int exact);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        IValidationRuleBuilder<T, TValue> MaxLength(int max);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="min"></param>
        /// <returns></returns>
        IValidationRuleBuilder<T, TValue> MinLength(int min);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IValidationRuleBuilder<T, TValue> Null();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IValidationRuleBuilder<T, TValue> NotNull();

        /// <summary>
        /// Validates whether the member is not empty.
        /// </summary>
        /// <returns></returns>
        IValidationRuleBuilder<T, TValue> NotEmpty();

        /// <summary>
        /// Validates whether the member is empty.
        /// </summary>
        /// <returns></returns>
        IValidationRuleBuilder<T, TValue> Empty();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TNumber">is of </typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        IValidationRuleBuilder<T, TValue> GreaterThan<TNumber>(TNumber value) where TNumber : struct, IComparable;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TNumber"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        IValidationRuleBuilder<T, TValue> GreaterThanOrEqualTo<TNumber>(TNumber value) where TNumber : struct, IComparable;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TNumber"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        IValidationRuleBuilder<T, TValue> LessThan<TNumber>(TNumber value) where TNumber : struct, IComparable;


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TNumber"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        IValidationRuleBuilder<T, TValue> LessThanOrEqualTo<TNumber>(TValue value) where TNumber : struct, IComparable;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TNumber"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        IValidationRuleBuilder<T, TValue> Equal<TNumber>(TNumber value) where TNumber : IComparable;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TNumber"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        IValidationRuleBuilder<T, TValue> NotEqual<TNumber>(TNumber value) where TNumber : IComparable;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="validator"></param>
        /// <returns></returns>
        IValidationRuleBuilder<T, TValue> UseValidator(IValidator<TValue> validator);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TBound"></typeparam>
        /// <param name="lowerBound"></param>
        /// <param name="upperBound"></param>
        /// <returns></returns>
        IValidationRuleBuilder<T, TValue> Between<TBound>(TBound lowerBound, TBound upperBound)
            where TBound : IComparable<TBound>;


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TBound"></typeparam>
        /// <param name="lowerBound"></param>
        /// <param name="upperBound"></param>
        /// <returns></returns>
        IValidationRuleBuilder<T, TValue> BetweenOrEqualTo<TBound>(TBound lowerBound, TBound upperBound)
            where TBound : IComparable<TBound>;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        IValidationRuleBuilder<T, TValue> WithErrorCode(string code);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        IValidationRuleBuilder<T, TValue> WithErrorMessage(string message);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="child"></param>
        /// <returns></returns>
        IValidationRuleBuilder<T, TValue> ChildRules(Action<IValidationRuleInitializer<TValue>> child);
    }
}
