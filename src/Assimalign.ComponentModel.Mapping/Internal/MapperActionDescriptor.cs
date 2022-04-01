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
    public MapperOptions Options { get; set; }
    public IMapperActionStack MapActions { get; set; }
    public IList<IMapperProfile> Profiles { get; set; } // Passing all added profiles from options as reference to be able to register nested profiles

    IMapperActionDescriptor IMapperActionDescriptor.MapAction(IMapperAction action) => MapAction(action);
    IMapperActionDescriptor IMapperActionDescriptor.MapAction(Action<IMapperContext> configure)
    {
        return this.MapAction(new MapperActionDefault(configure));
    }

    public IMapperActionDescriptor<TTarget, TSource> MapAction(IMapperAction action) 
    {
        MapActions.Push(action);
        return this;
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

        // Let's ensure we are not adding an already mapped action 
        if (MapActions.Contains(mapperAction))
        {
            throw new MapperInvalidMappingException(targetLambda);
        }
        
        return this.MapAction(mapperAction);
    }
    public IMapperActionDescriptor<TTarget, TSource> MapMember<TTargetMember, TSourceMember>(Expression<Func<TTarget, TTargetMember>> target, Expression<Func<TSource, TSourceMember>> source)
    {
        var mapperAction = new MapperActionMember<TTarget, TTargetMember, TSource, TSourceMember>(target, source);

        // Let's ensure we are not adding an already mapped action 
        if (MapActions.Contains(mapperAction))
        {
            throw new MapperInvalidMappingException(target);
        }

        return this.MapAction(mapperAction);
    }
}