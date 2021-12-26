using System;

namespace Assimalign.ComponentModel.Validation;


/// <summary>
/// Default rule builder for the passed context.
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
    /// Adds a custom validation rule that encapsulates custom logic.
    /// </summary>
    /// <param name="validation"></param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> Custom(Action<TValue, IValidationContext> validation);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> Length(int min, int max);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    IValidationRuleBuilder<T, TValue> Length(int min, int max, Action<IValidationError> configure);

    /// <summary>
    /// Requires the string member to be of an exact length.
    /// </summary>
    /// <param name="exact"></param>
    /// <returns></returns>
    IValidationRuleBuilder<T, TValue> Length(int exact);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="exact"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    IValidationRuleBuilder<T, TValue> Length(int exact, Action<IValidationError> configure);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="max"></param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> MaxLength(int max);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="max"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    IValidationRuleBuilder<T, TValue> MaxLength(int max, Action<IValidationError> configure);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="min"></param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> MinLength(int min);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="min"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    IValidationRuleBuilder<T, TValue> MinLength(int min, Action<IValidationError> configure);

    /// <summary>
    /// 
    /// </summary>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> Null();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configure"></param>
    /// <returns></returns>
    IValidationRuleBuilder<T, TValue> Null(Action<IValidationError> configure);

    /// <summary>
    /// 
    /// </summary>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> NotNull();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configure"></param>
    /// <returns></returns>
    IValidationRuleBuilder<T, TValue> NotNull(Action<IValidationError> configure);

    /// <summary>
    /// Validates whether the member is not empty.
    /// </summary>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> NotEmpty();

    /// <summary>
    /// Validates whether the member is empty.
    /// </summary>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> Empty();

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TNumber">is of </typeparam>
    /// <param name="value"></param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> GreaterThan<TNumber>(TNumber value) where TNumber : struct, IComparable;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <param name="confiure"></param>
    /// <returns></returns>
    IValidationRuleBuilder<T, TValue> GreaterThan<TNumber>(TNumber value, Action<IValidationError> confiure) 
        where TNumber : struct, IComparable;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> GreaterThanOrEqualTo<TNumber>(TNumber value) where TNumber : struct, IComparable;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> LessThan<TNumber>(TNumber value) where TNumber : struct, IComparable;


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> LessThanOrEqualTo<TNumber>(TValue value) where TNumber : struct, IComparable;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> Equal<TNumber>(TNumber value) where TNumber : IComparable;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> NotEqual<TNumber>(TNumber value) where TNumber : IComparable;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="validator"></param>
    /// <returns></returns>
    //IValidationRuleBuilder<T, TValue> UseValidator(IValidator<TValue> validator);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TBound"></typeparam>
    /// <param name="lowerBound"></param>
    /// <param name="upperBound"></param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> Between<TBound>(TBound lowerBound, TBound upperBound)
        where TBound : IComparable<TBound>;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TBound"></typeparam>
    /// <param name="lowerBound"></param>
    /// <param name="upperBound"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    IValidationRuleBuilder<T, TValue> Between<TBound>(TBound lowerBound, TBound upperBound, Action<IValidationError> configure)
        where TBound : IComparable<TBound>;


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TBound"></typeparam>
    /// <param name="lowerBound"></param>
    /// <param name="upperBound"></param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> BetweenOrEqualTo<TBound>(TBound lowerBound, TBound upperBound)
        where TBound : IComparable<TBound>;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TBound"></typeparam>
    /// <param name="lowerBound"></param>
    /// <param name="upperBound"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    IValidationRuleBuilder<T, TValue> BetweenOrEqualTo<TBound>(TBound lowerBound, TBound upperBound, Action<IValidationError> configure)
        where TBound : IComparable<TBound>;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="code"></param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> WithErrorCode(string code);


    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> WithErrorMessage(string message);


    /// <summary>
    /// 
    /// </summary>
    /// <param name="child"></param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> ChildRules(Action<IValidationRuleDescriptor<TValue>> child);
}
