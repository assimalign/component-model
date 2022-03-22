using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Mapping.Abstractions;

/// <summary>
/// 
/// </summary>
public interface IMapperProfile
{
    /// <summary>
    /// 
    /// </summary>
    int ProfileId { get; }

    /// <summary>
    /// 
    /// </summary>
    MapperProfileContext Context { get; }

    /// <summary>
    /// Invokes the Profile descriptor which creates the mapper profile
    /// </summary>
    /// <param name="descriptor"></param>
    void Configure(IMapperProfileDescriptor descriptor);
}


/// <summary>
/// 
/// </summary>
/// <typeparam name="TContract"></typeparam>
/// <typeparam name="TBinding"></typeparam>
public interface IMapperProfile<TContract, TBinding> : IMapperProfile
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="descriptor"></param>
    void Configure(IMapperProfileDescriptor<TContract, TBinding> descriptor);
}

