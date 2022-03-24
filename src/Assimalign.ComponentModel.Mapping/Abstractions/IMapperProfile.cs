using System;
using System.Collections.Generic;

namespace Assimalign.ComponentModel.Mapping;

/// <summary>
/// 
/// </summary>
public interface IMapperProfile : 
    IEquatable<IMapperProfile>,
    IEqualityComparer<IMapperProfile>
{
    /// <summary>
    /// 
    /// </summary>
    Type SourceType { get; }
    /// <summary>
    /// 
    /// </summary>
    Type TargetType { get; }
    /// <summary>
    /// Invokes the Profile descriptor which creates the mapper profile
    /// </summary>
    /// <param name="descriptor"></param>
    void Configure(IMapperProfileDescriptor descriptor);
}

/// <summary>
/// 
/// </summary>
/// <typeparam name="TSource"></typeparam>
/// <typeparam name="TTarget"></typeparam>
public interface IMapperProfile<TSource, TTarget> : IMapperProfile
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="descriptor"></param>
    void Configure(IMapperProfileDescriptor<TSource, TTarget> descriptor);
}