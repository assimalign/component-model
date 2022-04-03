using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Assimalign.ComponentModel.Mapping;

/// <summary>
/// The <see cref="IMapperActionDescriptor"/> represents a builder interface for pushing 
/// <see cref="IMapperAction"/>'s into the <see cref="IMapperActionStack"/> which lives on 
/// and <see cref="IMapperProfile"/>. 
/// </summary>
public interface IMapperActionDescriptor
{
    /// <summary>
    /// A FIFO (First-In First-Out) collection of Mapper Actions. 
    /// </summary>
    IMapperActionStack MapActions { get; }
    /// <summary>
    /// Adds an <see cref="IMapperAction"/> to the MapAction stack.
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    IMapperActionDescriptor MapAction(IMapperAction action);
}

/// <summary>
/// A <see cref="IMapperActionDescriptor{TSource, TTarget}"/> describes the mapping (or condition)
/// that are contracted to the binding type.
/// </summary>
/// <typeparam name="TSource"></typeparam>
/// <typeparam name="TTarget"></typeparam>
public interface IMapperActionDescriptor<TTarget, TSource> : IMapperActionDescriptor
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="configure"></param>
    /// <returns><see cref="IMapperActionDescriptor{TTarget, TSource}"/></returns>
    IMapperActionDescriptor<TTarget, TSource> MapAction(Action<IMapperContext> configure);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configure"></param>
    /// <returns><see cref="IMapperActionDescriptor{TTarget, TSource}"/></returns>
    IMapperActionDescriptor<TTarget, TSource> MapAction(Action<TTarget, TSource> configure);
    /// <summary>
    /// Creates a Member Expression from the property names provided.
    /// <remarks>
    ///     For property chaining use a period '.' between each property for nested objects.
    /// </remarks>
    /// </summary>
    /// <param name="target"></param>
    /// <param name="source"></param>
    /// <returns><see cref="IMapperActionDescriptor{TTarget, TSource}"/></returns>
    IMapperActionDescriptor<TTarget, TSource> MapMember(string target, string source);

    /// <summary>
    /// Creates a Member To Member mapping action. 
    /// </summary>
    /// <typeparam name="TTargetMember">MUST be a Member of type <typeparamref name="TSource"/></typeparam>
    /// <typeparam name="TSourceMember">The return value from the <typeparamref name="TSource"/>.</typeparam>
    /// <param name="source"></param>
    /// <param name="target">An expression the MUST be a <see cref="MemberExpression"/>.</param>
    /// <returns><see cref="IMapperActionDescriptor{TTarget, TSource}"/></returns>
    /// <exception cref="ArgumentException">'MapMebers' should only except <see cref="MemberExpression"/>'s for both the source and target.</exception>
    IMapperActionDescriptor<TTarget, TSource> MapMember<TTargetMember, TSourceMember>(Expression<Func<TTarget, TTargetMember>> target, Expression<Func<TSource, TSourceMember>> source);
}