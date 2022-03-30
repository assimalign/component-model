using System;
using System.Reflection;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ComponentModel.Mapping.Internal;

using Assimalign.ComponentModel.Mapping.Internal.Exceptions;


internal class MapperActionDescriptor<TTarget, TSource> : IMapperActionDescriptor<TTarget, TSource>
{
    public MapperActionDescriptor()
    {
        this.PreActions = new List<IMapperAction>();
        this.MapActions = new List<IMapperAction>();
        this.PostActions = new List<IMapperAction>();
    }

    public IList<IMapperAction> PreActions { get; }
    public IList<IMapperAction> MapActions { get; }
    public IList<IMapperAction> PostActions { get; }
    public IList<IMapperProfile> Profiles { get; set; } // Passing all added profiles from options as reference to be able to register nested profiles


    IMapperActionDescriptor IMapperActionDescriptor.MapAction(IMapperAction action) => MapAction(action);
    public IMapperActionDescriptor<TTarget, TSource> MapAction(IMapperAction action)
    {
        MapActions.Add(action);
        return this;
    }
    public IMapperActionDescriptor<TTarget, TSource> MapProfile<TTargetMember, TSourceMember>(Expression<Func<TTarget, IEnumerable<TTargetMember>>> target, Expression<Func<TSource, IEnumerable<TSourceMember>>> source, Action<IMapperActionDescriptor<TTargetMember, TSourceMember>> configure)
    {
        var profile = new MapperProfileDefault<TTargetMember, TSourceMember>(configure);

        if (this.Profiles.Contains(profile))
        {
            throw new Exception("");
        }

        IMapperActionDescriptor descriptor = new MapperActionDescriptor<TTargetMember, TSourceMember>()
        {
            Profiles = this.Profiles
        };

        profile.Configure(descriptor);

        this.Profiles.Add(profile);

        if (target is Expression<Func<TTarget, List<TTargetMember>>> list)
        {

        }

        var mapperAction = new MapperActionNestedEnumerable<TTarget, TTargetMember, TSource, TSourceMember>(target, source)
        {
            Profile = profile
        };
        if (MapActions.Contains(mapperAction))
        {
            throw new MapperInvalidMappingException(target);
        }
        return this.MapAction(mapperAction);
    }
    public IMapperActionDescriptor<TTarget, TSource> MapProfile<TTargetMember, TSourceMember>(Expression<Func<TTarget, TTargetMember>> target, Expression<Func<TSource, TSourceMember>> source, Action<IMapperActionDescriptor<TTargetMember, TSourceMember>> configure)
    {
        var profile = new MapperProfileDefault<TTargetMember, TSourceMember>(configure);

        if (this.Profiles.Contains(profile))
        {
            throw new Exception("");
        }

        IMapperActionDescriptor descriptor = new MapperActionDescriptor<TTargetMember, TSourceMember>()
        {
            Profiles = this.Profiles
        };

        profile.Configure(descriptor);

        this.Profiles.Add(profile);

        var mapperAction = new MapperActionNestedObject<TTarget, TTargetMember, TSource, TSourceMember>(target, source)
        {
            Profile = profile
        };
        if (MapActions.Contains(mapperAction))
        {
            throw new MapperInvalidMappingException(target);
        }

        this.MapAction(mapperAction);

        return this;
    }
    public IMapperActionDescriptor<TTarget, TSource> OnMapComplete(Action<TTarget, TSource> action)
    {
        this.PostActions.Add(new MapperAction<TTarget, TSource>(action));
        return this;
    }
    public IMapperActionDescriptor<TTarget, TSource> OnMapBegin(Action<TTarget, TSource> action)
    {
        this.PreActions.Add(new MapperAction<TTarget, TSource>(action));
        return this;
    }
    public IMapperActionDescriptor<TTarget, TSource> MapAllProperties()
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

                if (MapActions.Contains(mapperAction))
                {
                    throw new MapperInvalidMappingException(targetLambda);
                }
                this.MapAction(mapperAction);
            }
        }

        return this;
    }
    public IMapperActionDescriptor<TTarget, TSource> MapAllFields()
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
                
                if (MapActions.Contains(mapperAction))
                {
                    throw new MapperInvalidMappingException(targetLambda);
                }
                this.MapAction(mapperAction);
            }
        }

        return this;
    }
    public IMapperActionDescriptor<TTarget, TSource> MapAllMembers()
    {
        return MapAllFields().MapAllProperties();
    }
    public IMapperActionDescriptor<TTarget, TSource> MapMember(string target, string source)
    {
        var targetParameter = Expression.Parameter(typeof(TTarget));
        var sourceParameter = Expression.Parameter(typeof(TSource));
        var targetParameterMember = targetParameter.GetMemberExpression(target);
        var sourceParameterMember = sourceParameter.GetMemberExpression(source);
        var targetLambda = Expression.Lambda(targetParameterMember, targetParameter);
        var sourceLambda = Expression.Lambda(sourceParameterMember, sourceParameter);
        var mapperActionType = typeof(MapperActionMember<,,,>).MakeGenericType(
            typeof(TTarget),
            targetParameterMember.Type,
            typeof(TSource),
            sourceParameterMember.Type);

        var mapperAction = Activator.CreateInstance(mapperActionType, targetLambda, sourceLambda) as IMapperAction;
        if (MapActions.Contains(mapperAction))
        {
            throw new MapperInvalidMappingException(targetLambda);
        }
        return this.MapAction(mapperAction);
    }
    public IMapperActionDescriptor<TTarget, TSource> MapMember<TTargetMember, TSourceMember>(Expression<Func<TTarget, TTargetMember>> target, Expression<Func<TSource, TSourceMember>> source)
    {
        var mapperAction = new MapperActionMember<TTarget, TTargetMember, TSource, TSourceMember>(target, source);

        if (MapActions.Contains(mapperAction))
        {
            throw new MapperInvalidMappingException(target);
        }

        return this.MapAction(mapperAction);
    }

    
    private IMapperAction GetMapperAction<TTargetMember, TSourceMember>(Expression target, Expression source)
    {
        return target switch
        {
            Expression<Func<TTarget, IList<TTargetMember>>> ilt => new MapperAction
        };
    }
}