using System;

namespace Assimalign.ComponentModel.Mapping;

/// <summary>
/// 
/// </summary>
public interface IMapperBuilder
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="configure"></param>
    /// <returns></returns>
    IMapperBuilder Configure(Action<IMapperProfileDescriptor> configure);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TTarget"></typeparam>
    /// <param name="configure"></param>
    /// <returns></returns>
    IMapperBuilder Configure<TSource, TTarget>(Action<IMapperProfileDescriptor<TSource, TTarget>> configure);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IMapper Build();
}