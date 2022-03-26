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
    IMapperBuilder Configure(Action<IMapperActionDescriptor> configure);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTarget"></typeparam>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="configure"></param>
    /// <returns></returns>
    IMapperBuilder Configure<TTarget, TSource>(Action<IMapperActionDescriptor<TTarget, TSource>> configure);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IMapper Build();
}