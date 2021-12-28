using System;
using System.Collections;
using System.Collections.Generic;

namespace Assimalign.ComponentModel.Validation;


/// <summary>
/// An abstraction of the validation rule builder that is applied to the given <see cref="IValidationContext"/>
/// when the <typeparamref name="T"/> is being validated.
/// </summary>
/// <typeparam name="T">The type the validation </typeparam>
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
    /// Creates a rule specifying the required minimum and maximum length of an <see cref="IEnumerable"/>.
    /// </summary>
    /// <param name="min">The minimum length of the <see cref="IEnumerable"/> being validated.</param>
    /// <param name="max">The maximum length of the <see cref="IEnumerable"/> being validated.</param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    /// <exception cref="InvalidOperationException">The Length rule should only be applied to <see cref="IEnumerable"/> types.</exception>
    IValidationRuleBuilder<T, TValue> Length(int min, int max);

    /// <summary>
    /// Creates a rule specifying the required minimum and maximum length of an <see cref="IEnumerable"/> with 
    /// a configurable custom error.
    /// </summary>
    /// <param name="min">The minimum length of the <see cref="IEnumerable"/> being validated.</param>
    /// <param name="max">The maximum length of the <see cref="IEnumerable"/> being validated.</param>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    /// <exception cref="InvalidOperationException">The Length rule should only be applied to <see cref="IEnumerable"/> types.</exception>
    IValidationRuleBuilder<T, TValue> Length(int min, int max, Action<IValidationError> configure);

    /// <summary>
    /// Creates a rule specifying the exact length of an <see cref="IEnumerable"/>.
    /// </summary>
    /// <param name="exact">The required length of the <see cref="IEnumerable"/> being validated.</param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    ///  <exception cref="InvalidOperationException">The Length rule should only be applied to <see cref="IEnumerable"/> types.</exception>
    IValidationRuleBuilder<T, TValue> Length(int exact);

    /// <summary>
    /// Creates a rule specifying the exact length of an <see cref="IEnumerable"/> with 
    /// a configurable custom error.
    /// </summary>
    /// <param name="exact">The required length of the <see cref="IEnumerable"/> being validated.</param>
    /// <param name="configure">A delegate to configure a custom validation error.></param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    /// <exception cref="InvalidOperationException">The Length rule should only be applied to <see cref="IEnumerable"/> types.</exception>
    IValidationRuleBuilder<T, TValue> Length(int exact, Action<IValidationError> configure);

    /// <summary>
    /// Creates a rule specifying the maximum length of an <see cref="IEnumerable"/>.
    /// </summary>
    /// <param name="max">The maximum length of the <see cref="IEnumerable"/> being validated.</param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    /// <exception cref="InvalidOperationException">The Length rule should only be applied to <see cref="IEnumerable"/> types.</exception>
    IValidationRuleBuilder<T, TValue> MaxLength(int max);

    /// <summary>
    /// Creates a rule specifying the maximum length of an <see cref="IEnumerable"/> with 
    /// a configurable custom error.
    /// </summary>
    /// <param name="max">The maximum length of the <see cref="IEnumerable"/> being validated.</param>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    /// <exception cref="InvalidOperationException">The Length rule should only be applied to <see cref="IEnumerable"/> types.</exception>
    IValidationRuleBuilder<T, TValue> MaxLength(int max, Action<IValidationError> configure);

    /// <summary>
    /// Creates a rule specifying the minimum length of an <see cref="IEnumerable"/>.
    /// </summary>
    /// <param name="min">The minimum length of the <see cref="IEnumerable"/> being validated.</param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    /// <exception cref="InvalidOperationException">The Length rule should only be applied to <see cref="IEnumerable"/> types.</exception>
    IValidationRuleBuilder<T, TValue> MinLength(int min);

    /// <summary>
    /// Creates a rule specifying the minimum length of an <see cref="IEnumerable"/> with 
    /// a configurable custom error.
    /// </summary>
    /// <param name="min">The minimum length of the <see cref="IEnumerable"/> being validated.</param>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    /// <exception cref="InvalidOperationException">The Length rule should only be applied to <see cref="IEnumerable"/> types.</exception>
    IValidationRuleBuilder<T, TValue> MinLength(int min, Action<IValidationError> configure);

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be null.
    /// </summary>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> Null();

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be null with 
    /// a configurable custom error.
    /// </summary>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> Null(Action<IValidationError> configure);

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must not be null.
    /// </summary>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> NotNull();

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must not be null with 
    /// a configurable custom error.
    /// </summary>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> NotNull(Action<IValidationError> configure);

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must not be empty.
    /// </summary>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> NotEmpty();

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must not be empty with 
    /// a configurable custom error.
    /// </summary>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> NotEmpty(Action<IValidationError> configure);

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must not be empty.
    /// </summary>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> Empty();

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be empty with 
    /// a configurable custom error.
    /// </summary>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> Empty(Action<IValidationError> configure);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TArgument">is of </typeparam>
    /// <param name="value"></param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> GreaterThan<TArgument>(TArgument value) 
        where TArgument : notnull, IComparable;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TArgument"></typeparam>
    /// <param name="value"></param>
    /// <param name="confiure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> GreaterThan<TArgument>(TArgument value, Action<IValidationError> confiure)
        where TArgument : notnull, IComparable;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TArgument"></typeparam>
    /// <param name="value"></param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> GreaterThanOrEqualTo<TArgument>(TArgument value)
        where TArgument : notnull, IComparable;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TArgument"></typeparam>
    /// <param name="value"></param>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> GreaterThanOrEqualTo<TArgument>(TArgument value, Action<IValidationError> configure)
        where TArgument : notnull, IComparable;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TArgument"></typeparam>
    /// <param name="value"></param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> LessThan<TArgument>(TArgument value)
        where TArgument : notnull, IComparable;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TArgument"></typeparam>
    /// <param name="value"></param>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> LessThan<TArgument>(TArgument value, Action<IValidationError> configure)
        where TArgument : notnull, IComparable;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TArgument"></typeparam>
    /// <param name="value"></param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> LessThanOrEqualTo<TArgument>(TArgument value) 
        where TArgument : notnull, IComparable;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TArgument"></typeparam>
    /// <param name="value"></param>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> LessThanOrEqualTo<TArgument>(TArgument value, Action<IValidationError> configure)
        where TArgument : notnull, IComparable;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TArgument"></typeparam>
    /// <param name="value"></param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> EqualTo<TArgument>(TArgument value);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TArgument"></typeparam>
    /// <param name="value"></param>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> EqualTo<TArgument>(TArgument value, Action<IValidationError> configure);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TArgument"></typeparam>
    /// <param name="value"></param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> NotEqualTo<TArgument>(TArgument value);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TArgument"></typeparam>
    /// <param name="value"></param>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> NotEqualTo<TArgument>(TArgument value, Action<IValidationError> configure);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TBound"></typeparam>
    /// <param name="lowerBound"></param>
    /// <param name="upperBound"></param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> Between<TBound>(TBound lowerBound, TBound upperBound)
        where TBound : notnull, IComparable;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TBound"></typeparam>
    /// <param name="lowerBound"></param>
    /// <param name="upperBound"></param>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> Between<TBound>(TBound lowerBound, TBound upperBound, Action<IValidationError> configure)
        where TBound : notnull, IComparable;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TBound"></typeparam>
    /// <param name="lowerBound"></param>
    /// <param name="upperBound"></param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> BetweenOrEqualTo<TBound>(TBound lowerBound, TBound upperBound)
        where TBound : notnull, IComparable;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TBound"></typeparam>
    /// <param name="lowerBound"></param>
    /// <param name="upperBound"></param>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> BetweenOrEqualTo<TBound>(TBound lowerBound, TBound upperBound, Action<IValidationError> configure)
        where TBound : notnull, IComparable;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{T, TValue}"/></returns>
    IValidationRuleBuilder<T, TValue> ChildRules(Action<IValidationRuleDescriptor<TValue>> configure);
}