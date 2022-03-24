using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Assimalign.ComponentModel.Mapping;

/// <summary>
/// 
/// </summary>
public interface IMapperProfileDescriptor
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    IMapperProfileDescriptor AddMapperAction(IMapperAction item);
}

/// <summary>
/// A <see cref="IMapperProfileDescriptor{TSource, TTarget}"/> describes the mapping (or condition)
/// that are contracted to the binding type.
/// </summary>
/// <typeparam name="TSource"></typeparam>
/// <typeparam name="TTarget"></typeparam>
public interface IMapperProfileDescriptor<TSource, TTarget> : IMapperProfileDescriptor
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSourceMember"></typeparam>
    /// <typeparam name="TTargetMember"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    IMapperProfileDescriptor<TSource, TTarget> AddProfile<TSourceMember, TTargetMember>(
        Expression<Func<TSource, IEnumerable<TSourceMember>>> source,
        Expression<Func<TTarget, IEnumerable<TTargetMember>>> target,
        Action<IMapperProfileDescriptor<TSourceMember, TTargetMember>> configure)
            where TSourceMember : class
            where TTargetMember : class;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSourceMember"></typeparam>
    /// <typeparam name="TTargetMember"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    IMapperProfileDescriptor<TSource, TTarget> AddProfile<TSourceMember, TTargetMember>(
        Expression<Func<TSource, TSourceMember>> source,
        Expression<Func<TTarget, TTargetMember>> target,
        Action<IMapperProfileDescriptor<TSourceMember, TTargetMember>> configure)
            where TSourceMember : class
            where TTargetMember : class;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    IMapperProfileDescriptor<TSource, TTarget> MapMembers(string source, string target);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSourceMember"></typeparam>
    /// <typeparam name="TTargetMember"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    IMapperProfileDescriptor<TSource, TTarget> MapMembers<TSourceMember, TTargetMember>(
        Expression<Func<TSource, TSourceMember>> source, 
        Expression<Func<TTarget, TTargetMember>> target)
        where TSourceMember : TTargetMember;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="member"></param>
    /// <returns></returns>
    /// <remarks>
    /// In some cases the target and source properties might differ in type, format, or require some 
    /// form of transformation. This method allows for mapping a specific 
    /// </remarks>
    IMapperProfileTargetDescriptor<TSource, TTarget> ForSource(string member);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSourceMember"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    IMapperProfileTargetDescriptor<TSource, TTarget> ForSource<TSourceMember>(Expression<Func<TSource, TSourceMember>> expression);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="member"></param>
    /// <returns></returns>
    IMapperProfileSourceDescriptor<TSource, TTarget> ForTarget(string member);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTargetMember"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    IMapperProfileSourceDescriptor<TSource, TTarget> ForTarget<TTargetMember>(Expression<Func<TTarget, TTargetMember>> expression);

    /// <summary>
    /// This will disable default mapping of the 'Target' and 'Source' on compiling the profile.
    /// If disabled then each property must be mapped individually.
    /// </summary>
    void DisableDefaultMapping();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="action"></param>
    IMapperProfileDescriptor<TSource, TTarget> BeforeMap(Action<TSource, TTarget> action);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="action"></param>
    IMapperProfileDescriptor<TSource, TTarget> AfterMap(Action<TSource, TTarget> action);
}
