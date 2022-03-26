using System;
using System.Collections.Generic;

namespace Assimalign.ComponentModel.Mapping;

/// <summary>
/// 
/// </summary>
public interface IMapperProfile
{
    /// <summary>
    /// 
    /// </summary>
    Type TargetType { get; }
    /// <summary>
    /// Represents the Source Type to be used to copy values to the target type.
    /// </summary>
    Type SourceType { get; }
    /// <summary>
    /// A collection of actions to be invoked on mapping.
    /// </summary>
    IMapperActionCollection MapActions { get; }
    /// <summary>
    /// Invokes the Profile descriptor which creates the mapper profile
    /// </summary>
    /// <param name="descriptor"></param>
    void Configure(IMapperActionDescriptor descriptor);
}

/// <summary>
/// 
/// </summary>
/// <typeparam name="TTarget"></typeparam>
/// <typeparam name="TSource"></typeparam>
public interface IMapperProfile<TTarget, TSource> : IMapperProfile
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="descriptor"></param>
    void Configure(IMapperActionDescriptor<TTarget, TSource> descriptor);
}