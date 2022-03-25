using System;
using System.Collections.Generic;

namespace Assimalign.ComponentModel.Mapping;

/// <summary>
/// 
/// </summary>
public interface IMapperProfile
{
    /// <summary>
    /// Represents the Source Type to be used to copy values to the target type.
    /// </summary>
    Type SourceType { get; }
    /// <summary>
    /// 
    /// </summary>
    Type TargetType { get; }
    /// <summary>
    /// A collection of actions to be invoked on mapping.
    /// </summary>
    IEnumerable<IMapperAction> MapActions { get; }
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