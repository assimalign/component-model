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
    /// A FIFO (First-In First-Out)
    /// </summary>
    IMapperActionStack MapActions { get; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    IMapperActionDescriptor MapAction(IMapperAction action);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="configure"></param>
    /// <returns></returns>
    IMapperActionDescriptor MapAction(Action<IMapperContext> configure);
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
    /// Adds a nested profile which references complex types of <typeparamref name="TSource"/> and <typeparamref name="TTarget"/>.
    /// </summary>
    /// <typeparam name="TTargetMember"></typeparam>
    /// <typeparam name="TSourceMember"></typeparam>
    /// <param name="target"></param>
    /// <param name="source"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    IMapperActionDescriptor<TTarget, TSource> MapProfile<TTargetMember, TSourceMember>(Expression<Func<TTarget, IEnumerable<TTargetMember>>> target, Expression<Func<TSource, IEnumerable<TSourceMember>>> source, Action<IMapperActionDescriptor<TTargetMember, TSourceMember>> configure);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTargetMember"></typeparam>
    /// <typeparam name="TSourceMember"></typeparam>
    /// <param name="target"></param>
    /// <param name="source"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    IMapperActionDescriptor<TTarget, TSource> MapProfile<TTargetMember, TSourceMember>(Expression<Func<TTarget, TTargetMember>> target, Expression<Func<TSource, TSourceMember>> source, Action<IMapperActionDescriptor<TTargetMember, TSourceMember>> configure);
    /// <summary>
    /// Tries to map all Members of <typeparamref name="TTarget"/> and <typeparamref name="TSource"/> that share the same name.
    /// </summary>
    /// <returns></returns>
    IMapperActionDescriptor<TTarget, TSource> MapAllMembers();
    /// <summary>
    /// Tries to map only Field Members of <typeparamref name="TTarget"/> and <typeparamref name="TSource"/> that share the same name.
    /// </summary>
    /// <returns></returns>
    IMapperActionDescriptor<TTarget, TSource> MapAllFields();
    /// <summary>
    /// Tries to map only Property Members of <typeparamref name="TTarget"/> and <typeparamref name="TSource"/> that share the same name.
    /// </summary>
    /// <returns></returns>
    IMapperActionDescriptor<TTarget, TSource> MapAllProperties();
    /// <summary>
    /// 
    /// <remarks>
    ///     For property chaining use a period '.' between each property for nested objects.
    /// </remarks>
    /// </summary>
    /// <param name="target"></param>
    /// <param name="source"></param>
    /// <returns></returns>
    IMapperActionDescriptor<TTarget, TSource> MapMember(string target, string source);
    /// <summary>
    /// Creates a Member To Member mapping action. 
    /// </summary>
    /// <typeparam name="TMember">MUST be a Member of type <typeparamref name="TSource"/></typeparam>
    /// <typeparam name="TMemberValue">The return value from the <typeparamref name="TSource"/>.</typeparam>
    /// <param name="source"></param>
    /// <param name="target">An expression the MUST be a <see cref="MemberExpression"/>.</param>
    /// <returns><see cref="IMapperActionDescriptor{TTarget, TSource}"/></returns>
    /// <exception cref="ArgumentException">'MapMebers' should only except <see cref="MemberExpression"/>'s for both the source and target.</exception>
    IMapperActionDescriptor<TTarget, TSource> MapMember<TMember, TMemberValue>(Expression<Func<TTarget, TMember>> target, Expression<Func<TSource, TMemberValue>> source);
}