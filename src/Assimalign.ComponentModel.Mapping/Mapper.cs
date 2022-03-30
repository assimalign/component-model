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
    public TTarget Map<TTarget, TSource>(TTarget target, TSource source)
    {
        if (target is null)
        {
            throw new ArgumentNullException("target");
        }
        if (source is null)
        {
            throw new ArgumentNullException("source");
        }
        if (this.Map(target, source, typeof(TTarget), typeof(TSource)) is TTarget instance)
        {
            return instance;
        }
        else
        {
            throw new Exception("");
        }
    }
    public object Map(object source, Type targetType, Type sourceType)
    {
        if (source is null)
        {
            throw new ArgumentNullException("source");
        }
        try
        {
            object target = Activator.CreateInstance(targetType);

            return this.Map(target, source, targetType, sourceType);
        }
        catch (Exception exception)
        {
            throw new MapperInstanceCreationException(targetType, exception);
        }
    }
    public object Map(object target, object source, Type targetType, Type sourceType)
    {
        if (target is null)
        {
            throw new ArgumentNullException("target");
        }
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
