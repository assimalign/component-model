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


    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    public Mapper(MapperOptions options)
    {
        this.options = options;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TTarget"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public TTarget Map<TSource, TTarget>(TSource source)
        where TTarget : new()
    {
        if (source is null)
        {
            throw new ArgumentNullException("source");
        }

        var results = this.Map(source, typeof(TSource), typeof(TTarget));

        if (results is TTarget binding)
        {
            return binding;
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
    public TTarget Map<TSource, TTarget>(TSource source, TTarget target)
    {
        if (source is null)
        {
            throw new ArgumentNullException("source");
        }

        var results = this.Map(source, target, typeof(TSource), typeof(TTarget));

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
    public object Map(object source, Type sourceType, Type targetType)
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

        return this.Map(source, target, sourceType, targetType);
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
    public object Map(object source, object target, Type sourceType, Type targetType)
    {
        if (source is null)
        {
            throw new ArgumentNullException("source");
        }

        var context = new MapperContext(source, target);

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
