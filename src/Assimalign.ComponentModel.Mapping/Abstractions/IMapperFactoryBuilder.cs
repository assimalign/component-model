using System;

namespace Assimalign.ComponentModel.Mapping;

/// <summary>
/// 
/// </summary>
public interface IMapperFactoryBuilder
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTarget"></typeparam>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="configure"></param>
    /// <returns></returns>
    IMapperFactoryBuilder Configure<TTarget, TSource>(Action<IMapperProfile<TTarget, TSource>> configure);
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IMapper Build();
}