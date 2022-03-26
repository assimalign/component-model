using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Mapping;

using Assimalign.ComponentModel.Mapping.Internal.Exceptions;

/// <summary>
/// 
/// </summary>
public sealed class Mapper : IMapper
{
    private readonly MapperOptions options;
    private readonly ConcurrentDictionary<int, IMapperProfile> cache;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    public Mapper(MapperOptions options)
    {
        this.options = options;
        this.cache = new ConcurrentDictionary<int, IMapperProfile>(
            options.Profiles.ToDictionary(key=> key.TargetType.GetHashCode() + key.SourceType.GetHashCode(), value=>value));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTarget"></typeparam>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public TTarget Map<TTarget, TSource>(TSource source)
        where TTarget : new()
    {
        if (source is null)
        {
            throw new ArgumentNullException("source");
        }

        var results = this.Map(source, typeof(TTarget), typeof(TSource));

        if (results is TTarget instance)
        {
            return instance;
        }
        else
        {
            throw new Exception("");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TTarget"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public TTarget Map<TTarget, TSource>(TTarget target, TSource source)
    {
        if (source is null)
        {
            throw new ArgumentNullException("source");
        }

        var results = this.Map(target, source, typeof(TTarget), typeof(TSource));

        if (results is TTarget instance)
        {
            return instance;
        }
        else
        {
            throw new Exception("");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="sourceType"></param>
    /// <param name="targetType"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public object Map(object source, Type targetType, Type sourceType)
    {
        if (source is null)
        {
            throw new ArgumentNullException("source");
        }

        object target;

        try
        {
            target = Activator.CreateInstance(targetType);
        }
        catch (Exception exception)
        {
            throw new MapperInstanceCreationException(targetType, exception);
        }

        return this.Map(target, source, targetType, sourceType);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="sourceType"></param>
    /// <param name="targetType"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public object Map(object target, object source, Type targetType, Type sourceType)
    {
        if (source is null)
        {
            throw new ArgumentNullException("source");
        }

        var context = new MapperContext(target, source);

        foreach (var profile in options.Profiles)
        {
            if (profile.SourceType == sourceType && profile.TargetType == targetType)
            {
                foreach (var action in profile.MapActions)
                {
                    action.Invoke(context);
                }

                break;
            }
        }

        return target;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IMapper Create(Action<MapperOptions> configure)
    {
        var options = new MapperOptions();

        configure.Invoke(options);

        return new Mapper(options);
    }
}
