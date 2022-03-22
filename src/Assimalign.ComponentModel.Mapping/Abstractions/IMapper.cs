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
    /// <typeparam name="TContract"></typeparam>
    /// <typeparam name="TBinding"></typeparam>
    /// <param name="contract"></param>
    /// <returns></returns>
    TBinding Map<TContract, TBinding>(TContract contract)
        where TBinding : new();

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TContract"></typeparam>
    /// <typeparam name="TBinding"></typeparam>
    /// <param name="contract"></param>
    /// <param name="binding"></param>
    /// <returns></returns>
    TBinding Map<TContract, TBinding>(TContract contract, TBinding binding);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="contract"></param>
    /// <param name="contractType"></param>
    /// <param name="bindingType"></param>
    /// <returns></returns>
    object Map(object contract, Type contractType, Type bindingType);

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