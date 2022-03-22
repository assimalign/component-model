using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Assimalign.ComponentModel.Mapping.Abstractions;

/// <summary>
/// 
/// </summary>
public interface IMapperProfileDescriptor
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TTarget"></typeparam>
    /// <returns></returns>
    IMapperProfileDescriptor<TSource, TTarget> Create<TSource, TTarget>();
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
    /// Creates a Child Mapper Profile for the specified 'TSource' and 'TTarget' member that is of type IEnumerable.
    /// </summary>
    /// <typeparam name="TSourceMember"></typeparam>
    /// <typeparam name="TTargetMember"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    IMapperProfileDescriptor<TSourceMember, TTargetMember> AddProfile<TSourceMember, TTargetMember>(
        Expression<Func<TSource, IEnumerable<TSourceMember>>> source,
        Expression<Func<TTarget, IEnumerable<TTargetMember>>> target);

    /// <summary>
    /// Creates a Child Mapper Profile for the specified 'TSource' and 'TTarget' member.
    /// </summary>
    /// <typeparam name="TSourceMember"></typeparam>
    /// <typeparam name="TTargetMember"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    IMapperProfileDescriptor<TSourceMember, TTargetMember> AddProfile<TSourceMember, TTargetMember>(
        Expression<Func<TSource, TSourceMember>> source, 
        Expression<Func<TTarget, TTargetMember>> target);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    IMapperProfileDescriptor<TSource, TTarget> ForMember(string source, string target);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSourceMember"></typeparam>
    /// <typeparam name="TTargetMember"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    IMapperProfileDescriptor<TSource, TTarget> ForMember<TSourceMember, TTargetMember>(
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
    IMapperProfileTargetDescriptor<TTarget> ForSource(string member);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSourceMember"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    IMapperProfileTargetDescriptor<TTarget> ForSource<TSourceMember>(Expression<Func<TSource, TSourceMember>> expression);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="member"></param>
    /// <returns></returns>
    IMapperProfileSourceDescriptor<TSource> ForTarget(string member);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTargetMember"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    IMapperProfileSourceDescriptor<TSource> ForTarget<TTargetMember>(Expression<Func<TTarget, TTargetMember>> expression);

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
