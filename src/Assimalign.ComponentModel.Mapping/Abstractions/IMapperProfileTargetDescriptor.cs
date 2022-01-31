﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Mapping.Abstractions;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TSource"></typeparam>
public interface IMapperProfileTargetDescriptor<TSource>
{
    /// <summary>
    /// 
    /// </summary>
    void Ingore();

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSourceMember"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    void MapTarget<TSourceMember>(Expression<Func<TSource, TSourceMember>> expression);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="member"></param>
    /// <remarks>If chaining the path to members of child types then use '.' as a separator for each member name.</remarks>
    /// <returns></returns>
    void MapTarget(string member);
}

