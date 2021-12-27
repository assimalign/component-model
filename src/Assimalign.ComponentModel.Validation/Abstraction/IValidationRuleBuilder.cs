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
    /// 
    /// </summary>
    /// <param name="configure"></param>
    /// <returns></returns>
    IValidationRuleBuilder<T, TValue> NotEmpty(Action<IValidationError> configure);

    /// <summary>
    /// Validates whether the member is empty.
    /// </summary>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> Empty();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configure"></param>
    /// <returns></returns>
    IValidationRuleBuilder<T, TValue> Empty(Action<IValidationError> configure);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TArgument">is of </typeparam>
    /// <param name="value"></param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> GreaterThan<TArgument>(TArgument value) 
        where TArgument : IComparable;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TArgument"></typeparam>
    /// <param name="value"></param>
    /// <param name="confiure"></param>
    /// <returns></returns>
    IValidationRuleBuilder<T, TValue> GreaterThan<TArgument>(TArgument value, Action<IValidationError> confiure)
        where TArgument : IComparable;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TArgument"></typeparam>
    /// <param name="value"></param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> GreaterThanOrEqualTo<TArgument>(TArgument value) 
        where TArgument : IComparable;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TArgument"></typeparam>
    /// <param name="value"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    IValidationRuleBuilder<T, TValue> GreaterThanOrEqualTo<TArgument>(TArgument value, Action<IValidationError> configure)
        where TArgument : IComparable;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TArgument"></typeparam>
    /// <param name="value"></param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> LessThan<TArgument>(TArgument value)
        where TArgument : IComparable;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TArgument"></typeparam>
    /// <param name="value"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    IValidationRuleBuilder<T, TValue> LessThan<TArgument>(TArgument value, Action<IValidationError> configure)
        where TArgument : IComparable;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TArgument"></typeparam>
    /// <param name="value"></param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> LessThanOrEqualTo<TArgument>(TArgument value) 
        where TArgument : IComparable;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TArgument"></typeparam>
    /// <param name="value"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    IValidationRuleBuilder<T, TValue> LessThanOrEqualTo<TArgument>(TArgument value, Action<IValidationError> configure)
        where TArgument : IComparable;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TArgument"></typeparam>
    /// <param name="value"></param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> Equal<TArgument>(TArgument value);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TArgument"></typeparam>
    /// <param name="value"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    IValidationRuleBuilder<T, TValue> Equal<TArgument>(TArgument value, Action<IValidationError> configure);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TArgument"></typeparam>
    /// <param name="value"></param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> NotEqual<TArgument>(TArgument value);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TArgument"></typeparam>
    /// <param name="value"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    IValidationRuleBuilder<T, TValue> NotEqual<TArgument>(TArgument value, Action<IValidationError> configure);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TBound"></typeparam>
    /// <param name="lowerBound"></param>
    /// <param name="upperBound"></param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> Between<TBound>(TBound lowerBound, TBound upperBound)
        where TBound : IComparable;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TBound"></typeparam>
    /// <param name="lowerBound"></param>
    /// <param name="upperBound"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    IValidationRuleBuilder<T, TValue> Between<TBound>(TBound lowerBound, TBound upperBound, Action<IValidationError> configure)
        where TBound : IComparable;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TBound"></typeparam>
    /// <param name="lowerBound"></param>
    /// <param name="upperBound"></param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> BetweenOrEqualTo<TBound>(TBound lowerBound, TBound upperBound)
        where TBound : IComparable;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TBound"></typeparam>
    /// <param name="lowerBound"></param>
    /// <param name="upperBound"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    IValidationRuleBuilder<T, TValue> BetweenOrEqualTo<TBound>(TBound lowerBound, TBound upperBound, Action<IValidationError> configure)
        where TBound : IComparable;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configure"></param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> ChildRules(Action<IValidationRuleDescriptor<TValue>> configure);
}
