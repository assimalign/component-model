using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Mapping;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TSource"></typeparam>
public interface IMapperProfileTargetDescriptor<TSource, TTarget>
{
    /// <summary>
    /// 
    /// </summary>
    IMapperProfileDescriptor<TSource, TTarget> Ingore();

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTargetMember"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    IMapperProfileDescriptor<TSource, TTarget> MapTarget<TTargetMember>(Expression<Func<TTarget, TTargetMember>> expression);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="member"></param>
    /// <remarks>If chaining the path to members of child types then use '.' as a separator for each member name.</remarks>
    /// <returns></returns>
    IMapperProfileDescriptor<TSource, TTarget> MapTarget(string member);
}

