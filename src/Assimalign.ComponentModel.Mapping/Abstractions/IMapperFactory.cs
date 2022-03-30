using System;
using System.Collections.Generic;

namespace Assimalign.ComponentModel.Mapping;

///<summary>
///
///</summary>
public interface IMapperFactory
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="strategyName"></param>
    /// <param name="builder"></param>
    /// <returns></returns>
    void Configure(string strategyName, Action<IMapperFactoryBuilder> builder);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="strategyName"></param>
    /// <returns></returns>
    IMapper Create(string strategyName);
}