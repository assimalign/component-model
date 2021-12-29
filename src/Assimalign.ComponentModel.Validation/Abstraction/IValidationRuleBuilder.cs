using System;
using System.Collections;
using System.Collections.Generic;

namespace Assimalign.ComponentModel.Validation;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TValue"></typeparam>
public interface IValidationRuleBuilder<TValue>
{
    /// <summary>
    /// The validation rule being configured within the rule builder.
    /// </summary>
    IValidationItem ValidationItem { get; }

    /// <summary>
    /// 
    /// </summary>
    IValidationRuleStack ValidationRuleStack {get;}

    /// <summary>
    /// Adds a custom validation rule that encapsulates custom logic.
    /// </summary>
    /// <param name="validation"></param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    IValidationRuleBuilder<TValue> Custom(Action<TValue, IValidationContext> validation);

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be null.
    /// </summary>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    IValidationRuleBuilder<TValue> Null();

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be null with 
    /// a configurable custom error.
    /// </summary>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    IValidationRuleBuilder<TValue> Null(Action<IValidationError> configure);

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must not be null.
    /// </summary>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    IValidationRuleBuilder<TValue> NotNull();

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must not be null with 
    /// a configurable custom error.
    /// </summary>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    IValidationRuleBuilder<TValue> NotNull(Action<IValidationError> configure);

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be greater than <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="TArgument">is of </typeparam>
    /// <param name="value"></param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    IValidationRuleBuilder<TValue> GreaterThan<TArgument>(TArgument value)
        where TArgument : notnull, IComparable;

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be greater than <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="TArgument"></typeparam>
    /// <param name="value"></param>
    /// <param name="confiure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    IValidationRuleBuilder<TValue> GreaterThan<TArgument>(TArgument value, Action<IValidationError> confiure)
        where TArgument : notnull, IComparable;

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be greater than or equal to <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="TArgument"></typeparam>
    /// <param name="value"></param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    IValidationRuleBuilder<TValue> GreaterThanOrEqualTo<TArgument>(TArgument value)
        where TArgument : notnull, IComparable;

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be greater than or equal to  <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="TArgument"></typeparam>
    /// <param name="value"></param>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    IValidationRuleBuilder<TValue> GreaterThanOrEqualTo<TArgument>(TArgument value, Action<IValidationError> configure)
        where TArgument : notnull, IComparable;

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be less than <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="TArgument"></typeparam>
    /// <param name="value"></param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    IValidationRuleBuilder<TValue> LessThan<TArgument>(TArgument value)
        where TArgument : notnull, IComparable;

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be less than <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="TArgument"></typeparam>
    /// <param name="value"></param>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    IValidationRuleBuilder<TValue> LessThan<TArgument>(TArgument value, Action<IValidationError> configure)
        where TArgument : notnull, IComparable;

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be less than or equal to <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="TArgument"></typeparam>
    /// <param name="value"></param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    IValidationRuleBuilder<TValue> LessThanOrEqualTo<TArgument>(TArgument value)
        where TArgument : notnull, IComparable;

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be less than or equal to <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="TArgument"></typeparam>
    /// <param name="value"></param>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    IValidationRuleBuilder<TValue> LessThanOrEqualTo<TArgument>(TArgument value, Action<IValidationError> configure)
        where TArgument : notnull, IComparable;

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be equal to <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="TArgument"></typeparam>
    /// <param name="value"></param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    IValidationRuleBuilder<TValue> EqualTo<TArgument>(TArgument value)
        where TArgument : notnull, IEquatable<TArgument>;

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be equal to <paramref name="value"/>>.
    /// </summary>
    /// <typeparam name="TArgument"></typeparam>
    /// <param name="value"></param>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    IValidationRuleBuilder<TValue> EqualTo<TArgument>(TArgument value, Action<IValidationError> configure)
        where TArgument : notnull, IEqualityComparer, IEquatable<TArgument>;

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must not be equal to <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="TArgument"></typeparam>
    /// <param name="value"></param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    IValidationRuleBuilder<TValue> NotEqualTo<TArgument>(TArgument value)
        where TArgument : notnull, IEquatable<TArgument>;

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must not be equal to <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="TArgument"></typeparam>
    /// <param name="value"></param>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    IValidationRuleBuilder<TValue> NotEqualTo<TArgument>(TArgument value, Action<IValidationError> configure)
        where TArgument : notnull, IEquatable<TArgument>;

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be between <paramref name="lowerBound"/> and <paramref name="upperBound"/>.
    /// </summary>
    /// <typeparam name="TBound"></typeparam>
    /// <param name="lowerBound"></param>
    /// <param name="upperBound"></param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    IValidationRuleBuilder<TValue> Between<TBound>(TBound lowerBound, TBound upperBound)
        where TBound : notnull, IComparable;

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be between <paramref name="lowerBound"/> and <paramref name="upperBound"/>.
    /// </summary>
    /// <typeparam name="TBound"></typeparam>
    /// <param name="lowerBound"></param>
    /// <param name="upperBound"></param>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    IValidationRuleBuilder<TValue> Between<TBound>(TBound lowerBound, TBound upperBound, Action<IValidationError> configure)
        where TBound : notnull, IComparable;

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be between or equal to <paramref name="lowerBound"/> and <paramref name="upperBound"/>.
    /// </summary>
    /// <typeparam name="TBound"></typeparam>
    /// <param name="lowerBound"></param>
    /// <param name="upperBound"></param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    IValidationRuleBuilder<TValue> BetweenOrEqualTo<TBound>(TBound lowerBound, TBound upperBound)
        where TBound : notnull, IComparable;

    /// <summary>
    /// Creates a rule specifying that <typeparamref name="TValue"/> must be between or equal to <paramref name="lowerBound"/> and <paramref name="upperBound"/>.
    /// </summary>
    /// <typeparam name="TBound"></typeparam>
    /// <param name="lowerBound"></param>
    /// <param name="upperBound"></param>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    IValidationRuleBuilder<TValue> BetweenOrEqualTo<TBound>(TBound lowerBound, TBound upperBound, Action<IValidationError> configure)
        where TBound : notnull, IComparable;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configure">A delegate to configure a custom validation error.</param>
    /// <returns><see cref="IValidationRuleBuilder{TValue}"/></returns>
    IValidationRuleBuilder<TValue> ChildRules(Action<IValidationRuleDescriptor<TValue>> configure);
}