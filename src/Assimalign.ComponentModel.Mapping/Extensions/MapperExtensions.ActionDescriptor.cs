
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Mapping;

using Assimalign.ComponentModel.Mapping.Internal;
using Assimalign.ComponentModel.Mapping.Internal.Exceptions;

public static class MapperActionDescriptorExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTarget"></typeparam>
    /// <typeparam name="TTargetMember"></typeparam>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TSourceMember"></typeparam>
    /// <param name="descriptor"></param>
    /// <param name="target"></param>
    /// <param name="source"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    /// <exception cref="MapperInvalidMappingException"></exception>
    public static IMapperActionDescriptor<TTarget, TSource> MapProfile<TTarget, TTargetMember, TSource, TSourceMember>(this IMapperActionDescriptor<TTarget, TSource>  descriptor, Expression<Func<TTarget, TTargetMember>> target, Expression<Func<TSource, TSourceMember>> source, Action<IMapperActionDescriptor<TTargetMember, TSourceMember>> configure)
        where TTargetMember : class
        where TSourceMember : class
    {
        if (descriptor is MapperActionDescriptor<TTarget, TSource> ind)
        {
            var profile = new MapperProfileDefault<TTargetMember, TSourceMember>(configure);

            if (ind.Profiles.Contains(profile))
            {
                throw new Exception("");
            }

            IMapperActionDescriptor nd = new MapperActionDescriptor<TTargetMember, TSourceMember>()
            {
                Profiles = ind.Profiles,
                MapActions = profile.MapActions
            };

            profile.Configure(nd);

            ind.Profiles.Add(profile);

            var mapperAction = new MapperActionNestedProfile<TTarget, TTargetMember, TSource, TSourceMember>(target, source)
            {
                Profile = profile
            };

            if (ind.MapActions.Contains(mapperAction))
            {
                throw new MapperInvalidMappingException(target);
            }

            ind.MapAction(mapperAction);
        }
        else
        {
            // TODO: Decide whether to throw NotSupportedException();
        }

        return descriptor;
    }

    public static IMapperActionDescriptor<TTarget, TSource> MapProfile<TTarget, TTargetMember, TSource, TSourceMember>(this IMapperActionDescriptor<TTarget, TSource> descriptor, Expression<Func<TTarget, IEnumerable<TTargetMember>>> target, Expression<Func<TSource, IEnumerable<TSourceMember>>> source, Action<IMapperActionDescriptor<TTargetMember, TSourceMember>> configure)
        where TTargetMember : class
        where TSourceMember : class
    {
        //if (descriptor is MapperActionDescriptor<TTarget, TSource> ind)
        //{
           
        //}
        //else
        //{
        //    // TODO: Decide whether to throw NotSupportedException();
        //}

        //var profile = new MapperProfileDefault<TTargetMember, TSourceMember>(configure);

        //if (this.Profiles.Contains(profile))
        //{
        //    throw new Exception("");
        //}

        //IMapperActionDescriptor descriptor = new MapperActionDescriptor<TTargetMember, TSourceMember>()
        //{
        //    Profiles = this.Profiles
        //};

        //profile.Configure(descriptor);

        //this.Profiles.Add(profile);

        //if (target is Expression<Func<TTarget, List<TTargetMember>>> list)
        //{

        //}

        //var mapperAction = new MapperActionNestedEnumerable<TTarget, TTargetMember, TSource, TSourceMember>(target, source)
        //{
        //    Profile = profile
        //};
        //if (MapActions.Contains(mapperAction))
        //{
        //    throw new MapperInvalidMappingException(target);
        //}
        //return this.MapAction(mapperAction);

        return descriptor;
    }

    /// <summary>
    /// Tries to map all Members of <typeparamref name="TTarget"/> and <typeparamref name="TSource"/> that share the same name.
    /// </summary>
    /// <returns></returns>
    public static IMapperActionDescriptor<TTarget, TSource> MapAllMembers<TTarget, TSource>(this IMapperActionDescriptor<TTarget, TSource> descriptor)
    {
        return descriptor.MapAllProperties().MapAllFields();
    }

    /// <summary>
    /// Tries to map only Field Members of <typeparamref name="TTarget"/> and <typeparamref name="TSource"/> that share the same name.
    /// </summary>
    /// <returns></returns>
    public static IMapperActionDescriptor<TTarget, TSource> MapAllFields<TTarget, TSource>(this IMapperActionDescriptor<TTarget, TSource> descriptor)
    {
        var targetType = typeof(TTarget);
        var sourceType = typeof(TSource);

        var targetParameter = Expression.Parameter(targetType);
        var sourceParameter = Expression.Parameter(sourceType);

        foreach (var targetField in targetType.GetFields().Where(x => x.IsPublic))
        {
            var sourceField = sourceType.GetField(
                targetField.Name,
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

            if (sourceField is not null && sourceField.FieldType == targetField.FieldType)
            {
                var targetParameterMember = Expression.Field(targetParameter, targetField);
                var sourceParameterMember = Expression.Field(sourceParameter, sourceField);
                var targetLambda = Expression.Lambda(targetParameterMember, targetParameter);
                var sourceLambda = Expression.Lambda(sourceParameterMember, sourceParameter);
                var mapperActionType = typeof(MapperActionMember<,,,>).MakeGenericType(
                    typeof(TTarget),
                    targetParameterMember.Type,
                    typeof(TSource),
                    sourceParameterMember.Type);

                var mapperAction = Activator.CreateInstance(mapperActionType, targetLambda, sourceLambda) as IMapperAction;

                if (descriptor.MapActions.Contains(mapperAction))
                {
                    throw new MapperInvalidMappingException(targetLambda);
                }
                descriptor.MapAction(mapperAction);
            }
        }

        return descriptor;
    }

    /// <summary>
    /// Tries to map only Property Members of <typeparamref name="TTarget"/> and <typeparamref name="TSource"/> that share the same name.
    /// </summary>
    /// <returns></returns>
    public static IMapperActionDescriptor<TTarget, TSource> MapAllProperties<TTarget, TSource>(this IMapperActionDescriptor<TTarget, TSource> descriptor)
    {
        var targetType = typeof(TTarget);
        var sourceType = typeof(TSource);

        var targetParameter = Expression.Parameter(targetType);
        var sourceParameter = Expression.Parameter(sourceType);

        foreach (var targetProperty in targetType.GetProperties().Where(x => x.CanRead && x.CanWrite))
        {
            var sourceProperty = sourceType.GetProperty(
                targetProperty.Name,
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

            if (sourceProperty is not null && sourceProperty.CanRead && sourceProperty.CanWrite && sourceProperty.PropertyType == targetProperty.PropertyType)
            {
                var targetParameterMember = Expression.Property(targetParameter, targetProperty);
                var sourceParameterMember = Expression.Property(sourceParameter, sourceProperty);
                var targetLambda = Expression.Lambda(targetParameterMember, targetParameter);
                var sourceLambda = Expression.Lambda(sourceParameterMember, sourceParameter);
                var mapperActionType = typeof(MapperActionMember<,,,>).MakeGenericType(
                    typeof(TTarget),
                    targetParameterMember.Type,
                    typeof(TSource),
                    sourceParameterMember.Type);

                var mapperAction = Activator.CreateInstance(mapperActionType, targetLambda, sourceLambda) as IMapperAction;

                if (descriptor.MapActions.Contains(mapperAction))
                {
                    throw new MapperInvalidMappingException(targetLambda);
                }

                descriptor.MapAction(mapperAction);
            }
        }

        return descriptor;
    }
}
