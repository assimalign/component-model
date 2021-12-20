using System;

namespace Assimalign.ComponentModel.Mapping;


/// <summary>
/// 
/// </summary>
public interface IMapper
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TTarget"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    TTarget Map<TSource, TTarget>(TSource source)
        where TTarget : new();

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TTarget"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    TTarget Map<TSource, TTarget>(TSource source, TTarget target);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="sourceType"></param>
    /// <param name="targetType"></param>
    /// <returns></returns>
    object Map(object source, Type sourceType, Type targetType);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="sourceType"></param>
    /// <param name="targetType"></param>
    /// <returns></returns>
    object Map(object source, object target, Type sourceType, Type targetType);
}