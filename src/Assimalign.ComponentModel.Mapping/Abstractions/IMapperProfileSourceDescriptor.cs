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
/// <typeparam name="TTarget"></typeparam>
public interface IMapperProfileSourceDescriptor<TTarget>
{
    /// <summary>
    /// 
    /// </summary>
    void Ingore();

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMember"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    IMapperProfileSourceDescriptor<TTarget> MapSource<TMember>(Expression<Func<TTarget, TMember>> expression);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="member"></param>
    /// <remarks>If chaining the path to members of child types then use '.' as a separator for each member name.</remarks>
    /// <returns></returns>
    IMapperProfileSourceDescriptor<TTarget> MapSource(string member);
}
